using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Game : MonoBehaviour
{
    // ************************************
    // Game System
    // 1. スキル生成フェーズ
    // 2. スキル実行フェーズ
    // 3. 終了のフラグでゲームの終了を判断
    // ************************************

    [SerializeField, Header("パフォーマンスポイントの倍率(>1)")] private float skillPointRate = 1.1f;
    [SerializeField, Header("最大ターン数")] private int maxTurn = 10;

    // ゲームで生成したSkillを保持するリスト
    public List<Skill> generatedSkills = new List<Skill>();

    // Generateフェーズで生成されたスキル | (battler, cpu)
    public (Skill, Skill) generatedSkillInGeneratePhase = (null, null);

    
    public bool isFinished = false; // ゲーム終了かどうかの判断
    public int turn = 0; // ターン
    
    public GamePhase currentPhase = GamePhase.GameStart;
    public int performancePoint = 0;
    public (string, int) continuousAttribute = ("", 0);


    // ゲームの初期化
    public void InitGame(int maxTurn)
    {
        generatedSkills.Clear();
        generatedSkillInGeneratePhase = (null, null);
        isFinished = false;
        turn = 0;
        this.maxTurn = maxTurn;
        currentPhase = GamePhase.GameStart;
        performancePoint = 0;
        continuousAttribute = ("", 0);
    }


    // ゲームフェーズ
    public enum GamePhase
    {
        GameStart, //ゲームスタート
        TurnStart, //ターンスタート
        Generate, // 生成フェーズ
        Execute, // 実行フェーズ
        Result, // 結果フェーズ (ターンエンド)
        GameEnd // ゲーム終了フェーズ
    }

    // 各フェーズで実行すべきプログラム
    public void GameStart() // gamephase == GameStart時に呼び出す
    {
        // ゲームスタート時にすることがあれば行う
    }

    public void TurnStart() // gamephase == TurnStart時に呼び出す
    {
        // ターンスタート時に行う
    }

    public void Generate() // gamephase == Generate時に呼び出す
    {
        //
    }


    public void Execute() // gamephase == Execute時に呼び出す
    {
        //
    }

    public void Result() // gamephase == result時に呼び出す
    {
        //
    }


    public void GameEnd() // gamephase == GameEnd時に呼び出す
    {
        // 
    }


    // 生成したスキルを`generatedSkills`に追加
    public void AddSkill(Skill skill)
    {
        if (generatedSkills.Count == 0)
        {
            // 連続属性の設定
            continuousAttribute = (skill.Attribute(), 1);

            // スキルの追加
            generatedSkills.Add(skill);
        }
        else
        {
            // 追加するskillの属性
            var currentAtt = skill.Attribute();

            // 追加するskillの属性と連続している属性が一致しているなら
            if (continuousAttribute.Item1 == currentAtt) continuousAttribute.Item2++;
            // 異なるならリセット
            else continuousAttribute = (skill.Attribute(), 1);
        }

    }


    // 生成されたSkillからパフォーマンスポイントを計算する
    // スキルの属性が連続して同じ場合、ポイントを加算していく
    // このメソッドはAddSkillを呼び出した後に呼び出すこと
    //  - continuousAttributeの更新ができていないため
    public int ComputeSkillPoint(Skill skill)
    {
        int skillPoint = 0;
        // skillの属性は等倍、それ以外は0.5倍加算してポイントにする
        // continuousAttributeを考えて、属性の倍率計算する
        string skillAttribute = skill.Attribute();

        if (skillAttribute == "cute") skillPoint += (int)(skill.parameters.cute + (skill.parameters.cool + skill.parameters.unique) * 0.5f);
        else if (skillAttribute == "cool") skillPoint += (int)(skill.parameters.cool + (skill.parameters.cute + skill.parameters.unique) * 0.5f);
        else skillPoint += (int)(skill.parameters.unique + (skill.parameters.cute + skill.parameters.cool) * 0.5f);

        float ratio = Mathf.Pow(skillPointRate, continuousAttribute.Item2);

        skillPoint = (int)(skillPoint * ratio); //int型にキャスト

        return skillPoint;
    }


    // 生成したスキルを表示させる
    public string SkillDetails(Skill skill)
    {
        string text = $"skillName: {skill.skillName}\n";
        text += $"cute: {skill.parameters.cute}\n";
        text += $"cool: {skill.parameters.cool}\n";
        text += $"unique: {skill.parameters.unique}\n";
        text += $"att: {skill.Attribute()}\n";
        text += $"skillPoint: {ComputeSkillPoint(skill)}";
        return text;
    }

}
