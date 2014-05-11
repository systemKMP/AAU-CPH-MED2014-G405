using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.IO;

public class inputToTest : MonoBehaviour {

    LinkedList<testResult> roomInformation;
    testResult resultInformation = new testResult();
    testResultData savingResult = new testResultData();
    public float roomTimeUsed = 0;

	void Start () 
    {
	    roomInformation = new LinkedList<testResult>();
	}

	void Update () 
    {
	
	}

    void SaveResult()
    {
        savingResult.timeForNextRoom = roomInformation.ToList();

        System.IO.Directory.CreateDirectory(Application.dataPath + "/../RealTestResults/");
        var serializer = new XmlSerializer(typeof(ResultData));

        string time = "";
        if (System.DateTime.Now.Hour < 10)
        {
            time += "0";
        }
        time += System.DateTime.Now.Hour.ToString();

        if (System.DateTime.Now.Minute < 10)
        {
            time += "0";
        }
        time += System.DateTime.Now.Minute.ToString();

        if (System.DateTime.Now.Second < 10)
        {
            time += "0";
        }
        time += System.DateTime.Now.Second.ToString();

        var stream = new FileStream(Application.dataPath + "/../TestResults" + "/Result" + time + ".xml", FileMode.Create);
        serializer.Serialize(stream, savingResult);
        stream.Close();
    }

    void FailedCurrentRoom()
    {
        resultInformation.deathCounter++;
    }

    void RoomFinished()
    {
        roomTimeUsed = resultInformation.currRoomTimer;
        savingResult.timeForNextRoom = roomInformation.ToList();
        resultInformation.currRoomTimer = 0.0f;
        resultInformation.deathCounter = 0;
    }

    void TestStart()
    {
        resultInformation.currRoomTimer += Time.deltaTime;
        resultInformation.deathCounter = 0;
    }

    void TestFinished()
    {
        SaveResult();
    }
}
