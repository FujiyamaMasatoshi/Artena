using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBattle : MonoBehaviour
{
    [SerializeField] private Game game = null;
    [SerializeField] private int maxTurn = 10;
    [SerializeField, Header("myAvatar")] Battler battler = null;
    [SerializeField, Header("cpuAvatar")] CpuBattler cpu = null;

    // 生成したスキル
    private (Skill, Skill) generatedSkills = (null, null);
    // 実行して良いかどうか
    public bool isGenerated = false;

    // 初期化メソッド
    public void InitTrainingBattle()
    {
        generatedSkills = (null, null);
        isGenerated = false;
    }

    private void Start()
    {
        game.InitGame(maxTurn);

        StartCoroutine(Battle());
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
                    yield return new WaitForSeconds(1.0f);
                    game.NextGamePhase();
                    break;

                // TurnStartフェーズ
                case Game.GamePhase.TurnStart:
                    TurnStart();
                    yield return new WaitForSeconds(1.0f);
                    game.NextGamePhase();
                    break;

                // Generateフェーズ
                case Game.GamePhase.Generate:
                    //Generate(); -- ボタンクリックで呼び出す
                    yield return new WaitUntil(() => isGenerated);
                    isGenerated = false;
                    game.NextGamePhase();
                    break;

                // Executeフェーズ
                case Game.GamePhase.Execute:
                    Execute();
                    yield return new WaitForSeconds(1.0f);
                    game.NextGamePhase();
                    break;

                // Resultフェーズ
                case Game.GamePhase.Result:
                    Result();
                    yield return new WaitForSeconds(1.0f);
                    game.NextGamePhase();
                    break;

                // GameEndフェーズ
                case Game.GamePhase.GameEnd:
                    GameEnd();
                    yield return new WaitForSeconds(1.0f);
                    game.NextGamePhase();
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
        ClearGeneratedSkills();
    }

    public void Generate()
    {
        Debug.Log("Generate");
        battler.GenerateSkill();
        cpu.GenerateRandomSkill();

        
    }


    private void Execute()
    {
        Debug.Log("Execute");
        
    }

    private void Result()
    {
        Debug.Log("Result");
    }

    private void GameEnd()
    {
        Debug.Log("GameEnd");
    }

    // **********************************************************
    public void ClearGeneratedSkills()
    {
        generatedSkills.Item1 = null;
        generatedSkills.Item2 = null;
    }

    private void Update()
    {
        // Generateフェーズの時
        if (game.currentPhase == Game.GamePhase.Generate)
        {
            generatedSkills.Item1 = battler.GetGeneratedSkill();
            generatedSkills.Item2 = cpu.GetGeneratedSkill();
            if (generatedSkills.Item1 != null && generatedSkills.Item2 != null)
            {
                isGenerated = true;
            }
        }
    }
}


