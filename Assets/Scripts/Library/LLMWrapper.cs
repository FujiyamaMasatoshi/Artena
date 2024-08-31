using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LlamaCppUnity;

public class LLMWrapper : MonoBehaviour
{
    //public string modelPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Model/Llama-3-ELYZA-JP-8B-Q3_K_L.gguf");

    private Llama llm = null;

    public void LoadLLM(string modelPath)
    {
        llm = new Llama(modelPath);
    }

    public string Run(string userPrompt, uint maxTokens, float temperature)
    {
        string result = llm.Run(userPrompt, maxTokens: maxTokens, temperature: temperature);
        return result;
    }


}
