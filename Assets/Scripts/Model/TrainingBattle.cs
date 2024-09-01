using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingBattle : MonoBehaviour
{
    [SerializeField] private Game game = null;
    [SerializeField] private int maxTurn = 2;
    [SerializeField, Header("myAvatar")] private Battler battler = null;
    [SerializeField, Header("cpuAvatar")] private CpuBattler cpu = null;
    [SerializeField, Header("初期HP")] private int initHP = 1000;

    
    [SerializeField] private TMP_InputField inputField = null; // battler用のinputField

    // 生成したスキル
    private (Skill, Skill) generatedSkills = (null, null); //(battler, cpu)
    public bool isGenerated = false; // スキルが生成し終わったか -- 実行して良いかどうか
    public bool isGenerating = false; // スキルが生成中かどうか


    // マウスのクリックを検知するbool値
    public bool isMouseButtonDown = false;


    private void Start()
    {
        InitTrainingBattle(); // game init is in this method   

        StartCoroutine(Battle());
    }


    // 初期化メソッド
    public void InitTrainingBattle()
    {
        // initialize game
        game.InitGame(maxTurn);

        // スキル生成中を表すテキストを消す
        //isGeneratingText.text = "";


        generatedSkills = (null, null);
        isGenerated = false;
        isGenerating = false;
        isMouseButtonDown = false;


        // battlerとcpuのhpを初期化する
        battler.hp = initHP;
        cpu.hp = initHP;
    }


    private IEnumerator Battle()
    {
        while (!game.isFinished)
        {
            switch (game.currentPhase)
            {

                // GameStartフェーズ
                case Game.GamePhase.GameStart:
                    GameStart();
                    yield return new WaitUntil(() => isMouseButtonDown);
                    isMouseButtonDown = false;
                    game.currentPhase = Game.GamePhase.TurnStart; //状態遷移
                    break;

                // TurnStartフェーズ
                case Game.GamePhase.TurnStart:
                    TurnStart();
                    yield return new WaitUntil(() => isMouseButtonDown);
                    isMouseButtonDown = false;
                    game.currentPhase = Game.GamePhase.Generate; //状態遷移
                    break;

                // Generateフェーズ
                case Game.GamePhase.Generate:
                    //Generate(); -- ボタンクリックで呼び出す
                    yield return new WaitUntil(() => isGenerated && isMouseButtonDown);
                    isGenerated = false;
                    isMouseButtonDown = false;
                    game.currentPhase = Game.GamePhase.Execute; //状態遷移
                    break;

                // Executeフェーズ
                case Game.GamePhase.Execute:
                    Execute();
                    yield return new WaitUntil(() => isMouseButtonDown);
                    isMouseButtonDown = false;
                    game.currentPhase = Game.GamePhase.Result; //状態遷移
                    break;

                // Resultフェーズ
                case Game.GamePhase.Result:
                    Result();
                    yield return new WaitUntil(() => isMouseButtonDown);
                    isMouseButtonDown = false;
                    TurnStartOrGameEnd();
                    break;

                // GameEndフェーズ
                case Game.GamePhase.GameEnd:
                    GameEnd();
                    //yield return new WaitUntil(() => isMouseButtonDown);
                    //isMouseButtonDown = false;
                    
                    break;

            }
        }
        Debug.Log("all process is finished");
    }


    // ** 各GamePhaseメソッド ***********************************************
    private void GameStart()
    {
        Debug.Log("GameStart");
    }

    private void TurnStart()
    {
        Debug.Log("TurnStart");
        game.turn += 1;
        ClearGeneratedSkills(); // {game, battler, cpu}インスタンスのgeneratedSkillをクリア
        inputField.text = ""; // inputFieldをクリア

    }

    public void Generate()
    {
        // inputFieldに入力がない場合は、実行しない
        if (inputField.text == "" || inputField.text == null)
        {
            Debug.Log("スキル名を入力してください");
        }
        else
        {
            //Debug.Log("Generate");
            isGenerating = true;
            isGenerated = !isGenerating; // isGeneratingとは常に反対
            isMouseButtonDown = false; // スキル生成前にクリックしてしまった状態をリセット

            battler.GenerateSkill();
            cpu.GenerateRandomSkill();

        }
        
    }

    


    private void Execute()
    {
        Debug.Log("Execute");
        // 0. スキルが生成完了しているので、game.AddSkillを呼び出してスキルを追加する
        // 1. battlerとcpuのskillPointを計算
        // 2. 計算した結果から、それぞれのHPを更新

        // 0. 
        game.AddSkill(generatedSkills.Item1);

        // 1
        var battlerSkillPoint = game.ComputeSkillPoint(generatedSkills.Item1);
        var cpuSkillPoint = game.ComputeSkillPoint(generatedSkills.Item2);

        // 2
        // battlerのHPを更新
        battler.hp = (int)Mathf.Max(0, battler.hp - cpuSkillPoint);
        cpu.hp = (int)Mathf.Max(0, cpu.hp - battlerSkillPoint);
    }

    private void Result()
    {
        Debug.Log("Result");
        // resultで表示するテキストの更新などを行う
        // 
        
    }

    // ゲーム終了か継続かを判断し、状態遷移を行う
    private void TurnStartOrGameEnd()
    {
        // Executeで更新されたHPをもとに、GameEnd or TurnStartのどちらかにゲーム状態を更新
        if (battler.hp <= 0 || cpu.hp <= 0 || game.turn > maxTurn) game.currentPhase = Game.GamePhase.GameEnd;
        else game.currentPhase = Game.GamePhase.TurnStart;
    }

    private void GameEnd()
    {
        Debug.Log("GameEnd");
        game.isFinished = true;
    }

    // ***********************************************************************
    // Generated Skill 関連
    //
    public void ClearGeneratedSkills()
    {
        // battler, cpuのgeneratedSkillをクリア
        battler.ClearGeneratedSkill();
        cpu.ClearGeneratedSkill();

        // TrainingBattleの方をクリア
        generatedSkills.Item1 = null;
        generatedSkills.Item2 = null;
    }

    // 生成したスキルのGetメソッド
    public Skill GetGeneratedSkill_Item1()
    {
        return generatedSkills.Item1;
    }
    public Skill GetGeneratedSkill_Item2()
    {
        return generatedSkills.Item2;
    }

    //
    // ***********************************************************************

    private void Update()
    {
        // マウスのクリックを検知するbool値を更新 (スキル生成中は呼び出さない)
        if (Input.GetMouseButtonDown(0) && !isGenerating)
        {
            isMouseButtonDown = true;
        }

        // Generateフェーズの時
        if (game.currentPhase == Game.GamePhase.Generate)
        {
            generatedSkills.Item1 = battler.GetGeneratedSkill();
            generatedSkills.Item2 = cpu.GetGeneratedSkill();
            if (generatedSkills.Item1 != null && generatedSkills.Item2 != null)
            {

                // 各flagを更新
                isGenerating = false;
                isGenerated = true;

                

            }
        }
    }
}


