using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルクラス
public class Skill
{
    public string skillName;
    public int cute;
    public int cool;
    public int unique;

    public Skill(string skillName, int cute, int cool, int unique)
    {
        this.skillName = skillName;
        this.cute = cute;
        this.cool = cool;
        this.unique = unique;
    }

    // Skillの属性を返す
    public string Attribute()
    {
        var p = Random.Range(0f, 1f);
        if (cute > cool && cute > unique) return nameof(cute);
        else if (cool > cute && cool > unique) return nameof(cool);
        else if (unique > cute && unique > cool) return nameof(unique);
        else if (cute == cool && cute == unique)
        {
            if (p < 0.33f) return nameof(cute);
            else if (p < 0.66f) return nameof(cool);
            else return nameof(unique);
        }
        else if(cute == cool && cute > unique)
        {
            if (p < 0.5f) return nameof(cute);
            else return nameof(cool);
        }
        else if (cool == unique && cool > cute)
        {
            if (p < 0.5f) return nameof(cool);
            else return nameof(unique);
        }
        else if (cute == unique && unique > cool)
        {
            if (p < 0.5f) return nameof(unique);
            else return nameof(cute);
        }

        // どれでもない場合は、ランダムで返す
        string[] attributes = { nameof(cute), nameof(cool), nameof(unique) };
        return attributes[Random.Range(0, attributes.Length)];

    }

}

public class Game : MonoBehaviour
{
    // ************************************
    // Game System
    // 1. スキル生成フェーズ
    // 2. スキル実行フェーズ
    // 3. 終了のフラグでゲームの終了を判断
    // ************************************

    [SerializeField, Header("パフォーマンスポイントの倍率(>1)")] private float ppRate = 1.1f;

    // ゲームで生成したSkillを保持するリスト
    public List<Skill> generatedSkills = new List<Skill>();

    
    public bool isFinished = false; // ゲーム終了かどうかの判断
    public int turn = 0; // ターン
    public int performancePoint = 0;
    public (string, int) continuousAttribute = ("", 0);

    // ゲームの初期化
    public void InitGame()
    {
        generatedSkills.Clear();
        isFinished = false;
        turn = 0;
        performancePoint = 0;
        continuousAttribute = ("", 0);
    }

    // ゲームフェーズ
    public enum GamePhase
    {
        Generate, // 生成フェーズ
        Execute, // 実行フェーズ
        Result, // 結果フェーズ (ターンエンド)
        End // ゲーム終了フェーズ
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
    // +- continuousAttributeの更新ができていないため
    public void ComputePerformancePoint(Skill nowGeneratedSkill)
    {

    }


}
