using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// model
public class SkillPanelDataManager : MonoBehaviour
{
    // インスペクタで確認するために[SerializeField]にする
    [SerializeField] public int currentPageIndex = 0;
    [SerializeField] public int maxPageIndex = 0;
    [SerializeField] private int numSkillPanel = 10;

    private List<Skill> displayedSkills = new List<Skill>();

    // 初期化メソッド
    public void InitSkillPanelData()
    {
        currentPageIndex = 0;
        SetMaxPageIndex();
        displayedSkills = new List<Skill>();

        // 表示させるスキル
        ReflectSkillPanelData();
    }

    // 最大ページ数をセットする
    public void SetMaxPageIndex()
    {
        PlayerDataManager.instance.LoadSkillLibrary();
        maxPageIndex = PlayerDataManager.instance.skillLibrary.library.Count / numSkillPanel;
    }

    public void NextPage()
    {
        if (currentPageIndex < maxPageIndex)
        {
            currentPageIndex++;

            // スキルパネルに反映
            ReflectSkillPanelData();
        }
    }

    public void BackPage()
    {
        if (0 < currentPageIndex)
        {
            currentPageIndex--;

            // スキルパネルに反映
            ReflectSkillPanelData();
        }
    }

    public List<Skill> GetDisplayedSkills()
    {
        return displayedSkills;
    }

    // 表示させるスキルをセットする
    public void ReflectSkillPanelData()
    {
        // スキルライブラリをロード
        PlayerDataManager.instance.LoadSkillLibrary();

        // 表示させるスキルリストをクリア
        displayedSkills.Clear();

        // 現在のページインデックスを参照して、スキルをdisplayedSkillsにセットする
        // 探索開始地点、終了地点を取得
        int startIndex = currentPageIndex*10;
        int iter = Mathf.Min(numSkillPanel, PlayerDataManager.instance.skillLibrary.library.Count - currentPageIndex*10);
        
        Debug.Log($"startIndex: {startIndex}, iter: {iter}");
        // セット
        if (iter > 0)
        {
            for (int i = 0; i < iter; i++)
            {
                displayedSkills.Add(PlayerDataManager.instance.skillLibrary.library[i + startIndex]);
            }
        }
        
    }

    
}
