using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBufferForCPU
{
    private string[] skillBuffer;
    public SkillBufferForCPU()
    {
        skillBuffer = new string[10];
        SetSkillBuffer();
    }

    private void SetSkillBuffer()
    {

        skillBuffer[0] = "パステルポイズン";
        skillBuffer[1] = "もふもふデストロイ";
        skillBuffer[2] = "ときめきサンシャイン";
        skillBuffer[3] = "ニコニコスモッグ";
        skillBuffer[4] = "ピクシーキャンディー";
        skillBuffer[5] = "ツンデレトキシン";
        skillBuffer[6] = "メルティーハニーショット";
        skillBuffer[7] = "ゆるふわヴェノム";
        skillBuffer[8] = "ときめきバキューム";
        skillBuffer[9] = "ケチャップスラッシャー";
    }

    public string GetRandomSkillName()
    {
        int rand = Random.Range(0, skillBuffer.Length - 1);
        return skillBuffer[rand];
    }
}