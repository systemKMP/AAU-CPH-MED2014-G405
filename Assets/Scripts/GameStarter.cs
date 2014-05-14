using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {

    public bool playHRTF;

    void OnTriggerEnter()
    {
        GameManager.instance.hrtfMode = playHRTF;
        Application.LoadLevel(1);

    }
}
