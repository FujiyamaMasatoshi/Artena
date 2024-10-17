using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


public class SkillGenerator : MonoBehaviour
{


    // prompts
    //private string systemPrompt = "あなたは忠実なアシスタントです。これからユーザーが考えた「スキル名」が与えられます。そのスキル名を、可愛いらしさ(cute)、かっこよさ(cool)、ユニークさ(unique)について考えなさい。\n";
    private string fewshotPrompt = "";
    private string userPrompt = "";
    private string skillName = ""; //入力されたスキル名


    // llm instance
    [SerializeField] private LLMWrapper llm = null;
    [SerializeField] private uint maxTokens = 32;
    [SerializeField] private float temperature = 0.8f;


    // 生成されたSkill
    private Skill generatedSkill = null;

    public bool isGenerating = false;

    //private void Start()
    //{
    //    llm.LoadLLM();
    //}

    public void InitSkillGenerator()
    {
        // LLMWrapperの初期化メソッドを呼び出してロードを行う
        //llm.LoadLLM();
        llm.InitLLM();
    }


    public void SetSkillName(string name)
    {
        skillName = name;
    }

    public string GetSkillName()
    {
        return skillName;
    }

    // PlayerDataManagerからfew shotを引っ張る
    public void SetFewShotPrompt()
    {
        // fewShotデータを呼び出す
        PlayerDataManager.instance.LoadPlayerData();
        Skill cuteSkill = PlayerDataManager.instance.skillLibrary.fewShot[0];
        Skill coolSkill = PlayerDataManager.instance.skillLibrary.fewShot[1];
        Skill uniqueSkill = PlayerDataManager.instance.skillLibrary.fewShot[2];

        //string ex1 = $"(入力1)\n[{cuteSkill.skillName}]\n(出力1)\n {{\"cute\": {cuteSkill.parameters.cute}, \"cool\": {cuteSkill.parameters.cool}, \"unique\": {cuteSkill.parameters.unique}}}\n";
        //string ex2 = $"(入力2)\n[{coolSkill.skillName}]\n(出力2)\n{{\"cute\": {coolSkill.parameters.cute}, \"cool\": {coolSkill.parameters.cool}, \"unique\": {coolSkill.parameters.unique}}}\n";
        //string ex3 = $"(入力3)\n[{uniqueSkill.skillName}]\n(出力3)\n {{\"cute\": {uniqueSkill.parameters.cute}, \"cool\": {uniqueSkill.parameters.cool}, \"unique\": {uniqueSkill.parameters.unique}}}\n";

        string ex1 = $"(入力1)\n[{cuteSkill.skillDetails}]\n(出力1)\n {{\"cute\": {cuteSkill.parameters.cute}, \"cool\": {cuteSkill.parameters.cool}, \"unique\": {cuteSkill.parameters.unique}}}\n";
        string ex2 = $"(入力2)\n[{coolSkill.skillDetails}]\n(出力2)\n{{\"cute\": {coolSkill.parameters.cute}, \"cool\": {coolSkill.parameters.cool}, \"unique\": {coolSkill.parameters.unique}}}\n";
        string ex3 = $"(入力3)\n[{uniqueSkill.skillDetails}]\n(出力3)\n {{\"cute\": {uniqueSkill.parameters.cute}, \"cool\": {uniqueSkill.parameters.cool}, \"unique\": {uniqueSkill.parameters.unique}}}\n";

        fewshotPrompt = ex1 + ex2 + ex3;
    }

    // スキルの詳細ストーリーを生成させるためのプロンプト
    public void CreatePromptForSkillDetails(string skillName)
    {
        string sys = $"\nこれから与えられる[スキル名]から、そのスキルに関するストーリーを2~3文で出力しなさい。\n";
        Skill skill0 = PlayerDataManager.instance.skillLibrary.fewShot[0];
        Skill skill1 = PlayerDataManager.instance.skillLibrary.fewShot[1];
        Skill skill2 = PlayerDataManager.instance.skillLibrary.fewShot[2];
        string fewShots = $"(入力1)\n[スキル名]: {skill0.skillName}\n(出力1)\n[stroy]{skill0.skillDetails}[/story]\n";
        fewShots += $"(入力2)\n[スキル名]: {skill1.skillName}\n(出力2)\n[story]{skill1.skillDetails}[/story]\n";
        fewShots += $"(入力3)\n[スキル名]: {skill2.skillName}\n(出力3)\n[story]{skill2.skillDetails}[/story]\n";

        userPrompt = sys + fewShots + $"(入力4)\n[スキル名]: {skillName}\n(出力4)\n[story]";
    }

    // スキルパラメータを生成させるためのプロンプト
    public void CreatePromptForSkillParameter(string skillDetails)
    {

        string sys = "これからあるストーリーが与えられます。これらのストーリーの内容から、可愛らしさ(cute)、かっこよさ(cool)、ユニークさ(unique)の3つの観点で点数をつけなさい。";

        var input = $"(入力4)\n[{skillDetails}]\n(出力4)\n";
        userPrompt = sys + fewshotPrompt + input;
    }

    // 入力されたスキル名と生成されたパラメーターを抽出して返す
    private Skill ExtractSkill(string skillName, string allText, string skillDetails, string pattern = @"\{.*?\}")
    {
        // patternに従って文字列を抽出
        MatchCollection matches = Regex.Matches(allText, pattern);

        if (matches.Count <= 0)
        {
            // から文字列を返す -> もう一度推論させる合図
            return null;
        }
        else
        {
            // 最初に一致したもの
            string matchedText = matches[0].Value;

            //改行コードを削除
            matchedText = matchedText.Replace("\n", "");
            Debug.Log($"matched text: {matchedText}");


            // 抽出したパラメーターをJson形式に変換し、SkillParamsに変換
            SkillParams param = null;

            try
            {
                param = JsonUtility.FromJson<SkillParams>(matchedText);
            }
            // if parse is failed ... rule base
            catch (System.Exception e)
            {
                Debug.Log("failed to parse in json");
                var cute = Random.Range(0, 101);
                var cool = Random.Range(0, 101);
                var unique = Random.Range(0, 101);
                param = new SkillParams(cute, cool, unique);
            }
            

            // SkillParamsとskillNameからSkillを生成
            Skill skill = new Skill(skillName, param, skillDetails);

            return skill; // {cute, cool, unique} json string
        }
    }

    private string ExtractSkillDetails(string skillDetails, string pattern = "[/story]"/*@"\[(.*?)\]"*/)
    {
        //// patternに従って文字列を抽出
        //MatchCollection matches = Regex.Matches(skillDetails, pattern);

        //if (matches.Count <= 0)
        //{
        //    // から文字列を返す -> もう一度推論させる合図
        //    return null;
        //}
        //else
        //{
        //    // 最初に一致したもの
        //    string matchedText = matches[0].Value;

        //    //改行コードを削除
        //    matchedText = matchedText.Replace("\n", "");
        //    //Debug.Log($"matched text: {matchedText}");

        //    return matchedText;
        //}

        int index = skillDetails.IndexOf(pattern);

        if (index != -1) // [/story]が見つかった場合
        {
            // [/story]までの文字列を取得
            string result = skillDetails.Substring(0, index);

            return result;
        }
        else
        {
            // [/story]が見つからなかった場合の処理
            Debug.Log("タグが見つかりませんでした。");
            return null;
        }
    }



    // skillNameが""出ない限り実行する
    public async void GenerateSkill()
    {
        if (this.skillName != "")
        {
            generatedSkill = null; // 前回生成したスキルをリセット

            isGenerating = true; // 推論開始

            // ####################
            // SkillDetails生成
            // ####################
            CreatePromptForSkillDetails(this.skillName);
            Debug.Log($"skillDetail prompt: {userPrompt}");
            var details = await Task.Run(() => llm.Run(userPrompt, 96, temperature));
            var extractDetails = ExtractSkillDetails(details);
            Debug.Log($"details: {details}");
            Debug.Log($"extract details: {extractDetails}");

            // #############
            // スキル生成
            // #############
            SetFewShotPrompt();
            CreatePromptForSkillParameter(details);
            var llmOut = await Task.Run(() => llm.Run(userPrompt, maxTokens, temperature));

            isGenerating = false; //推論終了

            // 生成したSkillをセット
            generatedSkill = ExtractSkill(this.skillName, llmOut, extractDetails);
        }
        else
        {
            Debug.Log("skillNameが空です。");
        }
        
        
    }

    // 生成したスキルをゲット
    public Skill GetGeneratedSkill()
    {
        return generatedSkill;
    }

    // 生成したスキルをクリア
    public void ClearGeneratedSkill()
    {
        generatedSkill = null;
    }

    // **************************************************
    // CPU (or 自動戦闘向けに)
    // 追加で、ランダムでスキル名を生成するメソッドを持つ
    // ***************************************************

    private string generatedRandomSkillName = "";
    public void SetGeneratedRandomSkillName(string name)
    {
        generatedRandomSkillName = name;
    }
    public string GetGeneratedRandomSkillName()
    {
        return generatedRandomSkillName;
    }

    // スキル名をランダムで生成
    public async void GenerateRandomSkill()
    {
        // *****************************
        // 1. ランダムなスキル名を生成
        // *****************************
        // システムプロンプト
        var sysPrmpt = "あなたは忠実なアシスタントです。これからcute,cool,uniqueの中から1つお題を与えるので、それにあったオリジナル必殺技を10文字以内で考えてください。\n";
        // fewshot
        var fewshot = "(入力1)\n[cute]\n(出力1)\n{フラッフィー・ハートブレイク}\n";
        fewshot += "(入力2)\n[cool]\n(出力2)\n{氷刃絶影}\n";
        fewshot += "(入力3)\n[unique]\n(出力3)\n{ミラージュ乱舞}\n";
        // 選択させる属性
        string[] attributes = { "cute", "cool", "unique" };
        var input = $"(入力4)\n[{attributes[Random.Range(0, attributes.Length)]}]\n(出力4)\n";

        // userプロンプト
        var usrPrmpt = sysPrmpt + fewshot + input;

        isGenerating = true; // 推論開始

        var skillNameLlmOut = await Task.Run(() => llm.Run(usrPrmpt, 16, temperature));

        //isGenerating = false; // 終了

        generatedRandomSkillName = ExtractSkillName(skillNameLlmOut);

        // 空白の場合はもう一度呼び出して再生成を行う
        if (string.IsNullOrEmpty(generatedRandomSkillName) || generatedRandomSkillName.Trim().Length == 0)
        {
            Debug.Log("生成されたスキルが空白です。再生成を実行します");
            GenerateRandomSkill();
        }
        else
        {
            // ****************************
            // 2. Skillを生成
            // ****************************
            //generatedSkill = null; // 前回生成したスキルをリセット

            ////isGenerating = true; // 推論開始
            //SetFewShotPrompt();
            //CreatePromptForSkillParameter(this.generatedRandomSkillName); //ランダム生成したスキル名を使用
            //var skillLlmOut = await Task.Run(() => llm.Run(userPrompt, maxTokens, temperature));

            //isGenerating = false; //推論終了

            // 生成したSkillをセット
            //generatedSkill = ExtractSkill(this.generatedRandomSkillName, skillLlmOut);

            this.skillName = generatedRandomSkillName;
            GenerateSkill();
        }



    }

    //ランダムで生成されたスキル名を抽出する
    private string ExtractSkillName(string allText, string pattern = @"\{(.*?)\}")
    {
        // patternに従って文字列を抽出
        MatchCollection matches = Regex.Matches(allText, pattern);

        if (matches.Count <= 0)
        {
            // から文字列を返す -> もう一度推論させる合図
            return "";
        }
        else
        {
            // 最初に一致したもの
            string matchedText = matches[0].Value;

            //改行コードを削除
            matchedText = matchedText.Replace("\n", "");
            matchedText = matchedText.Substring(startIndex: 1, length: matchedText.Length - 2);
            Debug.Log($"matched text: {matchedText}");

            return matchedText;
        }
    }

    

}
