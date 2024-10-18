using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 個別のプレイヤーデータを管理する
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance = null;
    public string playerName = "";
    public string attribute = "";
    public SkillLibrary skillLibrary = null;
    
    //public List<Skill> skillLibrary = new List<Skill>();

    // startの前に呼び出される
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void LoadPlayerData()
    {
        // プレイヤー名設定
        playerName = PlayerPrefs.GetString("PlayerName");

        // プレイヤーの属性
        attribute = PlayerPrefs.GetString("Attribute");

        // SkillLibraryからデータをロード
        LoadSkillLibrary();
    }

    // これまでのデータを全て消去し新しくデータを構築する
    public void DeletePlayerData()
    {
        // 全てのデータを消去
        PlayerPrefs.DeleteAll();

        LoadPlayerData();
        playerName = "";
        skillLibrary.library.Clear();
        for (int i = 0; i < skillLibrary.fewShot.Length; i++)
        {
            skillLibrary.fewShot[i] = null;
        }
    }


    // プレイヤー名を保存
    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save(); //ディスクに保存
    }

    // SkillLibraryからデータをロード
    public void LoadSkillLibrary()
    {
        // PlayerPrefsからデータを読み込む
        string json = PlayerPrefs.GetString("SkillLibrary");
        
        // jsonをJsonUtilityを使用してdeserializeする
        skillLibrary = JsonUtility.FromJson<SkillLibrary>(json);

        if (skillLibrary == null)
        {
            skillLibrary = new SkillLibrary();
        }
        Debug.Log("skillLibrary: " + skillLibrary);
    }


    

    // スキルをSkillLibraryに追加する
    public void SaveSkillInSkillLibrary(Skill skill)
    {

        // これまでのスキルをロード
        LoadSkillLibrary();

        // スキルを追加
        skillLibrary.library.Add(skill);
        Debug.Log($"skillLibrary.Count: {skillLibrary.library.Count}");

        // json変換
        string json = JsonUtility.ToJson(skillLibrary, true);
        Debug.Log("json\n" + json);

        // 保存
        PlayerPrefs.SetString("SkillLibrary", json);
        PlayerPrefs.Save();
    }

    // 添え字を受け取りskillLibrary.libraryのスキルを削除
    public void DeleteSkillInSkillLibrary(int index)
    {
        // 決して保存する
        skillLibrary.library.RemoveAt(index);

        // json変換
        string json = JsonUtility.ToJson(skillLibrary, true);
        Debug.Log("json\n" + json);

        // 保存
        PlayerPrefs.SetString("SkillLibrary", json);
        PlayerPrefs.Save();
    }

    // NewGame時に最初にスキルをセットさせる
    public void InitSkillLibrary()
    {
        LoadSkillLibrary();

        // 初期化するためのスキルはこちらで用意する
        Skill cuteSkill = new Skill("キャピギャルアピール", new SkillParams(65, 10, 10), "ギャルのアピールは、一周回って可愛くて仕方ないギャルのアピールには心を打たれる。");
        Skill coolSkill = new Skill("氷刃絶影", new SkillParams(11, 84, 5), "氷の刃によって影を絶つほどの剣技にはきっとメロメロにされてしまうだろう。");
        Skill uniqueSkill = new Skill("ミラージュ乱舞", new SkillParams(12, 14, 74), "ミラージュ曲線を体現したユニークなスキルだ。");

        // libraryにセット
        skillLibrary.fewShot[0] = cuteSkill;
        skillLibrary.fewShot[1] = coolSkill;
        skillLibrary.fewShot[2] = uniqueSkill;

        // これまでのデータを削除して初期化
        skillLibrary.library.Clear();
        skillLibrary.library.Add(cuteSkill);
        skillLibrary.library.Add(coolSkill);
        skillLibrary.library.Add(uniqueSkill);

        // json変換
        string json = JsonUtility.ToJson(skillLibrary, true);
        Debug.Log("json\n" + json);

        // 保存
        PlayerPrefs.SetString("SkillLibrary", json);
        PlayerPrefs.Save();

    }

    public void InitAttribute()
    {
        string[] attributes = { "cute", "cool", "unique"};

        int index = Random.Range(0, 3);
        this.attribute = attributes[index];

        // 保存
        SaveAttribute();
        //PlayerPrefs.SetString("Attribute", this.attribute);
        //PlayerPrefs.Save();
    }

    public void SaveAttribute()
    {
        if (this.attribute == "cute" || this.attribute == "cool" || this.attribute == "unique")
        {
            // 保存
            PlayerPrefs.SetString("Attribute", this.attribute);
            PlayerPrefs.Save();
        }
    }

    public void SetFewShotSkill(Skill skill)
    {
        // スキルライブラリをロード
        LoadSkillLibrary();

        // FewShotを参照して、skillと同じ属性のスキルがなければセット
        // 同じ属性がすでにセットされている場合は、上書き
        for(int i=0; i<skillLibrary.fewShot.Length; i++)
        {
            Skill s = skillLibrary.fewShot[i];
            if (s.Attribute() == skill.Attribute())
            {
                skillLibrary.fewShot[i] = skill;

                // json変換
                string json = JsonUtility.ToJson(skillLibrary, true);
                Debug.Log("json\n" + json);

                // 保存
                PlayerPrefs.SetString("SkillLibrary", json);
                PlayerPrefs.Save();

                break;
            }
        }
    }



    // テスト
    public void PrintSkillLibrary()
    {
        // スキルライブラリをロード
        LoadSkillLibrary();

        // 表示
        string skills = "";
        if (skillLibrary != null && skillLibrary.library.Count > 0)
        {
            foreach (Skill skill in skillLibrary.library)
            {
                skills += $"skillName: {skill.skillName} - (cute:{skill.parameters.cute}, cool:{skill.parameters.cool},unique:{skill.parameters.unique})\n";
            }
            Debug.Log("skills\n" + skills);
        }
        else
        {
            Debug.Log("library is empty");
        }

    }
}

