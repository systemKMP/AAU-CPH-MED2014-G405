using UnityEngine;
using System.Collections;

public class testResult 
{
    public float currRoomTimer;
    public int deathCounter;

    public testResult()
    {
        currRoomTimer = 0.0f;
        deathCounter = 0;
    }

    public testResult(float roomTimer, int deaths)
    {
        currRoomTimer = roomTimer;
        deathCounter = deaths;
    }

}
