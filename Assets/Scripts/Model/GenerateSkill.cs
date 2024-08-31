using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class GenerateSkill : MonoBehaviour
{
    [SerializeField] private string modelPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Model/Llama-3-ELYZA-JP-8B-Q3_K_L.gguf");
    [SerializeField] private string systemPrompt = "あなたは忠実なアシスタントです。あなたには、これからスキル名としてユーザーが考えた単語が与えられます。その単語を可愛いらしさ、かっこよさ、ユニークさの3つの視点から0から100の間で点数をつけてください。\n";
    [SerializeField] TextMeshPro testText = null;

    // prompts
    private string fewshotPrompt = "";
    private string userPrompt = "";
    private string generatedSkillParams = "";

    // llm instance
    [SerializeField] private LLMWrapper llm = null;
    [SerializeField] private uint maxTokens = 32;
    [SerializeField] private float temperature = 0.8f;

    [SerializeField] private bool isGenerating = false;

    private void Start()
    {
        llm.LoadLLM(modelPath);
    }


    public void SetFewShotPrompt()
    {
        string ex1 = "(入力1)\n[雷鳴のレゾナンス]\n(出力1)\n{\"cute\": 20, \"cool\": 89, \"unique\": 35}\n";
        string ex2 = "(入力2)\n[フローラル花嵐]\n(出力2)\n{\"cute\": 85, \"cool\": 17, \"unique\": 45}\n";
        string ex3 = "(入力3)\n[幻影の茶碗スマッシュ]\n(出力3)\n{\"cute\": 15, \"cool\": 7, \"unique\": 95}\n";
        
        fewshotPrompt = ex1 + ex2 + ex3;
    }

    public void CreatePrompt(string skillName="波風スプラッシュ")
    {
        var input = $"(入力4)\n[{skillName}]\n(出力4)\n";
        userPrompt = systemPrompt + fewshotPrompt + input;
    }


    async void GenerateSkillParameter()
    {
        isGenerating = true;
        SetFewShotPrompt();
        CreatePrompt();
        generatedSkillParams = await Task.Run(() => llm.Run(userPrompt, maxTokens, temperature));


        // testTExtに反映
        testText.text = generatedSkillParams;

        isGenerating = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isGenerating) GenerateSkillParameter();
        }
    }
}
