using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TB_GenerateButton : MonoBehaviour
{
    [SerializeField] private TrainingBattle battle;
    [SerializeField] private Game game;
    [SerializeField] private float maxEstimateTime = 15f;

    private Button button;
    private float timer = 0f;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        RepushButton();
    }

    private void Init()
    {
        button = GetComponent<Button>();
    }

    private void RepushButton()
    {
        if (game.currentPhase == Game.GamePhase.TurnStart)
        {
            timer = 0f;
        }
        if (game.currentPhase == Game.GamePhase.Generate)
        {
            timer += Time.deltaTime;

            if (timer > maxEstimateTime)
            {
                battle.Generate();
                Debug.Log("exe re generate");
                timer = 0f;
            }
        }
    }
}
