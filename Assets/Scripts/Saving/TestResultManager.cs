using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.IO;

public class TestResultManager : MonoBehaviour {

    public static TestResultManager instance;

    List<TestResult> savingResult;

    private int currentRoomDeathCount;
    private float currentRoomTime;
    private int currentRoomNr;

    private bool testRunning;

    void Awake(){
        instance = this;
    }

	void Start () 
    {
        savingResult = new List<TestResult>();
        StartTest();
	}

	void Update () 
    {
        currentRoomTime += Time.deltaTime;
	}

    void SaveResult()
    {

        TestResultData trd = new TestResultData();
        trd.hrtfTest = GameManager.instance.hrtfMode;

        trd.testDataList = savingResult;

        System.IO.Directory.CreateDirectory(Application.dataPath + "/../FinalTestData/");
        var serializer = new XmlSerializer(typeof(TestResultData));

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

        var stream = new FileStream(Application.dataPath + "/../FinalTestData" + "/Result" + time + ".xml", FileMode.Create);
        serializer.Serialize(stream, trd);
        stream.Close();
    }

    public void FailedCurrentRoom()
    {
        currentRoomDeathCount++;
    }

    public void RoomFinished()
    {
        TestResult currentRoomData = new TestResult();

        currentRoomData.currentRoomTimer = currentRoomTime;
        currentRoomData.deathCounter = currentRoomDeathCount;
        currentRoomData.roomNumber = currentRoomNr;

        currentRoomTime = 0.0f;
        currentRoomDeathCount = 0;
        currentRoomNr++;

        savingResult.Add(currentRoomData);
    }

    public void StartTest()
    {
        currentRoomNr = 0;
        testRunning = true;
        currentRoomTime = 0.0f;
        currentRoomDeathCount = 0;
    }

    public void FinishTest()
    {
        RoomFinished();
        testRunning = false;
        SaveResult();
    }
}
