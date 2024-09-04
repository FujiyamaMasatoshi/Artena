using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LlamaCppUnity;
using System;

public class LLMWrapper : MonoBehaviour
{

    private Llama llm = null;
    private string modelPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Model/Llama-3-ELYZA-JP-8B-Q3_K_L.gguf");
    //private string modelPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Model/stabilityai-japanese-stablelm-3b-4e1t-instruct-Q4_0.gguf");
    //private string modelPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Model/alpaca-guanaco-japanese-gpt-1b.Q8_0.gguf");


    // bool型にして成功したらtrue, 失敗したらfalseを返す
    public void LoadLLM()
    {
        Debug.Log("modelPath: " + modelPath);
        llm = new Llama(modelPath);
        //if (llm == null)
        //{
        //    try
        //    {
        //        llm = new Llama(modelPath);

        //    }
        //    catch (ArgumentException e)
        //    {
        //        llm = null;
        //        Debug.Log("モデルのロードに失敗しました。");
                
        //    }
        //}
        
    }

    public string Run(string userPrompt, uint maxTokens, float temperature)
    {
        string result = llm.Run(userPrompt, maxTokens: maxTokens, temperature: temperature);
        return result;
    }


}
