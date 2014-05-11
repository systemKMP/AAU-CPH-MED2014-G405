using UnityEngine;
using System.Collections;

public enum TTState { empty, showGuesses, askFocus, findSound, soundMooving }

public class TTGUIControler : MonoBehaviour
{
    public static TTGUIControler instance;

    public TTState currentState;
    bool showSingleMessage = false;
    string message = "";

    int buttonW = 200;
    int buttonH = 80;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Screen.lockCursor = true;
        currentState = TTState.findSound;
    }


    void OnGUI()
    {
        switch (currentState)
        {
            case TTState.askFocus:
                showSingleMessage = true;
                message = "Please refocus and press mouse button";
                


                break;
            case TTState.findSound:
                showSingleMessage = true;
                message = "Find the sound source and press mouse button";
                break;
            case TTState.soundMooving:
                showSingleMessage = true;
                message = "Sound is mooving";
                break;
            case TTState.showGuesses:
                if (GUI.Button(new Rect(Screen.width / 8 * 2 - buttonW / 2, Screen.height / 2+50, buttonW, buttonH), "1: Counter clockwise 40°") || Input.GetKeyDown(KeyCode.Alpha1))
                {
                    GuessAttempt(-40);
                }
                if (GUI.Button(new Rect(Screen.width / 8 * 3 - buttonW / 2, Screen.height / 2 + 50, buttonW, buttonH), "2: Counter clockwise 20°") || Input.GetKeyDown(KeyCode.Alpha2))
                {
                    GuessAttempt(-20);
                }
                if (GUI.Button(new Rect(Screen.width / 8 * 4 - buttonW / 2, Screen.height / 2 + 50, buttonW, buttonH), "3: Stayed still") || Input.GetKeyDown(KeyCode.Alpha3))
                {
                    GuessAttempt(0);
                }
                if (GUI.Button(new Rect(Screen.width / 8 * 5 - buttonW / 2, Screen.height / 2 + 50, buttonW, buttonH), "4: Clockwise 20°") || Input.GetKeyDown(KeyCode.Alpha4))
                {
                    GuessAttempt(20);
                }
                if (GUI.Button(new Rect(Screen.width / 8 * 6 - buttonW / 2, Screen.height / 2 + 50, buttonW, buttonH), "5: Clockwise 40°") || Input.GetKeyDown(KeyCode.Alpha5))
                {
                    GuessAttempt(40);
                }
                showSingleMessage = false;
                break;

        }

        if (showSingleMessage)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2, 500, 100), message);
        }

    }

    public void SetState(TTState state)
    {
        currentState = state;
    }

    private void GuessAttempt(float degrees)
    {
        TTManager.instance.GuessMade(degrees);
    }
}
