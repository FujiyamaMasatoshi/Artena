using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingBattle : MonoBehaviour
{
    [SerializeField] private Game game = null;
    [SerializeField] private int maxTurn = 2;
    [SerializeField, Header("myAvatar")] private Battler battler = null;
    [SerializeField, Header("cpuAvatar")] private Battler cpu = null;
    [SerializeField, Header("初期HP")] private int initHP = 1000;

    [SerializeField] private GameObject InGameCanvas = null;
    [SerializeField] private GameObject OutGameCanvas = null;
    [SerializeField] private OutGameEvent outGameEvent = null;
    [SerializeField] private GameObject mainCamera = null;
    [SerializeField] private Transform defaultCameraTransform = null;
    
    [SerializeField] private TMP_InputField inputField = null; // battler用のinputField

    public bool isGenerated = false; // スキルが生成し終わったか -- 実行して良いかどうか
    public bool isGenerating = false; // スキルが生成中かどうか
    public bool isExecuted = false; // スキル実行終了したかどうか -> エフェクトも終了済み


    // マウスのクリックを検知するbool値
    public bool isMouseButtonDown = false;


    private void Start()
    {
        NewGame();
    }

    // Initialize
    public void NewGame()
    {
        InitTrainingBattle(); // game init is in this method   

        StartCoroutine(Battle());
    }


    // 初期化メソッド
    public void InitTrainingBattle()
    {
        // initialize game
        game.InitGame(maxTurn);
        TB_GameManager.instance.InitGameData();

        // 各種メンバ変数を初期化
        isGenerated = false;
        isGenerating = false;
        isMouseButtonDown = false;
        isExecuted = false;

        // InGameCanvasをactive true, OutGameCanvasをactive falseにする
        InGameCanvas.SetActive(true);
        OutGameCanvas.SetActive(false);

        // カメラをデフォルトの位置に移動させる
        mainCamera.transform.position = defaultCameraTransform.position;
        mainCamera.transform.rotation = defaultCameraTransform.rotation;
        mainCamera.transform.localScale = defaultCameraTransform.localScale;

        // battlerとcpuのhpを初期化する
        battler.InitBattler(initHP);
        cpu.InitBattler(initHP);
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
                    yield return new WaitUntil(() => isGenerated/* && isMouseButtonDown*/);
                    isGenerated = false;
                    isMouseButtonDown = false;
                    game.currentPhase = Game.GamePhase.Execute; //状態遷移
                    break;

                // Executeフェーズ
                case Game.GamePhase.Execute:
                    Execute();
                    yield return new WaitUntil(() => isExecuted);
                    isMouseButtonDown = false;
                    game.currentPhase = Game.GamePhase.Result; //状態遷移
                    break;

                // Resultフェーズ
                case Game.GamePhase.Result:
                    Result();
                    // yield return new WaitUntil(() => isMouseButtonDown);
                    yield return new WaitForSeconds(1.0f);
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

        // Out game event start
        StartOutGameEvent();
    }


    // ** 各GamePhaseメソッド ***********************************************
    private void GameStart()
    {
        Debug.Log("GameStart");
        ClearGeneratedSkills();
        inputField.text = "";
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
        // Generateフェーズでしか動作させない
        if (game.currentPhase == Game.GamePhase.Generate)
        {
            // inputFieldに入力がない場合は、実行しない
            if (inputField.text == "" || inputField.text == null)
            {
                Debug.Log("スキル名を入力してください");
            }
            else
            {
                //Debug.Log("Generate");
                isGenerating = true; // 生成中
                isGenerated = false; // まだ生成完了していない

                // battlerとcpuのflagを更新
                battler.isGenerating = true;
                cpu.isGenerating = true;

                // マウスの状態をリセット
                isMouseButtonDown = false; // スキル生成前にクリックしてしまった状態をリセット

                // スキル生成
                if (game.generatedSkillInGeneratePhase.Item1 == null)
                {
                    battler.GenerateSkill();
                }
                if (game.generatedSkillInGeneratePhase.Item2 == null)
                {
                    cpu.GenerateRandomSkill();
                }
            }
        }
        
        
    }

    


    private void Execute()
    {
        isExecuted = false;   

        Debug.Log("Execute");
        // 1. battlerとcpuのアニメーションを更新
        // 2. スキルが生成完了しているので、game.AddSkillを呼び出してスキルを追加する

        // 1. battlerとcpuのアニメーションフラグを更新
        battler.StartExecute(cpu.transform);
        cpu.StartExecute(battler.transform);
        battler.isExecute = true;
        cpu.isExecute = true;

        // 2. 
        game.AddSkill(game.generatedSkillInGeneratePhase.Item1);
        
        
    }

    private void Result()
    {
        Debug.Log("Result");

        // 1. batterとcpuのskillPointを計算 -> TB_GameManagerに書き込む
        // 2. 計算した結果から、それぞれのHPを更新

        // 1
        var battlerSkillPoint = game.ComputeSkillPoint(game.generatedSkillInGeneratePhase.Item1);
        var cpuSkillPoint = game.ComputeSkillPoint(game.generatedSkillInGeneratePhase.Item2);

        TB_GameManager.instance.battlerSkillPoints += battlerSkillPoint;
        TB_GameManager.instance.cpuSkillPoints += cpuSkillPoint;

        // ダメージエフェクトを生成
        // battlerのダメージ生成
        Debug.Log($"cpuSkillPoint: {cpuSkillPoint}");
        Debug.Log($"battlerSkillPoint: {battlerSkillPoint}");
        battler.DisplayDamageEffect(cpuSkillPoint);
        // cpuのダメージ生成
        cpu.DisplayDamageEffect(battlerSkillPoint);


        // 2
        // battlerのHPを更新
        battler.hp = (int)Mathf.Max(0, battler.hp - cpuSkillPoint);
        cpu.hp = (int)Mathf.Max(0, cpu.hp - battlerSkillPoint);

        // battlerとcpuのアニメーションフラグを更新
        battler.isExecute = false;
        cpu.isExecute = false;

        
    }

    // ゲーム終了か継続かを判断し、状態遷移を行う
    private void TurnStartOrGameEnd()
    {
        // ダメージ表記を消す
        battler.ClearDamageEffect();
        cpu.ClearDamageEffect();

        // Executeで更新されたHPをもとに、GameEnd or TurnStartのどちらかにゲーム状態を更新
        if (battler.hp <= 0 || cpu.hp <= 0 || game.turn >= maxTurn) game.currentPhase = Game.GamePhase.GameEnd;
        else game.currentPhase = Game.GamePhase.TurnStart;
    }

    private void GameEnd()
    {
        Debug.Log("GameEnd");
        game.isFinished = true;
    }

    // GameEnd後の処理後に呼び出される
    private void StartOutGameEvent()
    {
        outGameEvent.StartEvent();

        // 表示させるキャンバスを変更する
        InGameCanvas.SetActive(false);
        OutGameCanvas.SetActive(true);
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
        game.generatedSkillInGeneratePhase = (null, null);
    }

    // 生成したスキルのGetメソッド
    public Skill GetGeneratedSkill_Item1()
    {
        return game.generatedSkillInGeneratePhase.Item1;
    }
    public Skill GetGeneratedSkill_Item2()
    {
        return game.generatedSkillInGeneratePhase.Item2;
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
            game.generatedSkillInGeneratePhase.Item1 = battler.GetGeneratedSkill();
            game.generatedSkillInGeneratePhase.Item2 = cpu.GetGeneratedSkill();

            if (game.generatedSkillInGeneratePhase.Item1 != null && game.generatedSkillInGeneratePhase.Item2 != null)
                {
                Debug.Log($"generated skill: {game.generatedSkillInGeneratePhase.Item1}, {game.generatedSkillInGeneratePhase.Item2}");
                // 各flagを更新
                isGenerating = false;
                isGenerated = true;

                // battlerとcpuのflagを更新
                battler.isGenerating = false;
                cpu.isGenerating = false;

            }
        }

        // Executeフェーズの時
        if (game.currentPhase == Game.GamePhase.Execute)
        {
            if ((!battler.IsEffecting() && battler.isExecute) && (!cpu.IsEffecting() && cpu.isExecute))
            {
                isExecuted = true;
            }
            else
            {
                isExecuted = false;
            }
        }
    }
}


