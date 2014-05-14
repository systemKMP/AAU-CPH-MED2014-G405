using UnityEngine;
using System.Collections;

public class GameEnder : MonoBehaviour {

    private bool gameEnding;

    private float gameEndTime = 3.0f;
    private float currentTimer;

    public PathController pController;

    void Start()
    {
        gameEnding = false;
    }

    void Update()
    {
        if (gameEnding)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer < 0.0f)
            {
                Application.LoadLevel(0);
            }
        }
    }

    void OnTriggerEnter()
    {
        if (!gameEnding)
        {
            TestResultManager.instance.FinishTest();
            currentTimer = gameEndTime;
            gameEnding = true;
            pController.enabled = true;
        }
    }
}
