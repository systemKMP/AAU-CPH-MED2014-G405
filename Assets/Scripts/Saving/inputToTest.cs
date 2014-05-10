using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.IO;

public class inputToTest : MonoBehaviour {

    LinkedList<testResult> roomTimeUsed;
    LinkedList<testResult> death;

	void Start () 
    {
	    roomTimeUsed = new LinkedList<testResult>();
        death = new LinkedList<testResult>();
	}

	void Update () 
    {
	
	}

    void saveResult()
    {
        testResultData timeUsed = new testResultData();
        timeUsed.timeForNextRoom = roomTimeUsed.ToList();
        testResultData roomDeath = new testResultData();
        roomDeath.deathCount = death.ToList();

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
        serializer.Serialize(stream, timeUsed);
        serializer.Serialize(stream, roomDeath);
        stream.Close();
    }

    void failedCurrentRoom()
    { 
        
    }

    void roomFinished()
    { 
        
    }

    void testStart()
    { 
        
    }

    void testFinished()
    { 
        
    }
}
