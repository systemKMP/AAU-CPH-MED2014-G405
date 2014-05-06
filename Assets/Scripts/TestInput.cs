using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class TestInput : MonoBehaviour
{

    private bool showOptions = false;
    private float showOptionsAngle = 1.0f;
    public Transform rotationTrans;

    private Quaternion rotationTarget;

    public Transform cameraRotation;

    int totalGuessCount = 0;

    LinkedList<Result> angleOffests;

    public int requiredGuessCount = 10;

    // Use this for initialization
    void Start()
    {
        angleOffests = new LinkedList<Result>();

        RandomizeSoundPosition();
    }

    void Update()
    {
        RotateTowardsTarget();
        if (Input.GetMouseButtonDown(0) && showOptions)
        {
            GuessPosition();
        }

        if (totalGuessCount > requiredGuessCount && Input.GetKeyDown(KeyCode.Escape))
        {

            SaveResults();
            Application.LoadLevel(0);
        }

    }

    // Update is called once per frame
    void OnGUI()
    {
        if (showOptions)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 1.5f, 160, 80), "PRESS LEFT MOUSE KEY TO GUESS THE POSITION");
        }
        if (totalGuessCount > requiredGuessCount)
        {
            GUI.Label(new Rect(0, 0, 100, 100), "Press Esc to finish");

        }
    }

    private void GuessPosition()
    {
        totalGuessCount++;

        Vector3 cameraTViewAngle = cameraRotation.rotation * Vector3.forward;
        cameraTViewAngle.Set(cameraTViewAngle.x, 0.0f, cameraTViewAngle.z);


        Vector3 soundTViewAngle = rotationTrans.rotation * Vector3.forward;
        soundTViewAngle.Set(soundTViewAngle.x, 0.0f, soundTViewAngle.z);

        float sAng = Vector3.Angle(cameraTViewAngle, soundTViewAngle);

        float tAng = Vector3.Angle(cameraRotation.rotation * Vector3.forward, rotationTrans.rotation * Vector3.forward);

        float hAng = Vector3.Angle(cameraRotation.rotation * Vector3.forward, Vector3.up) - Vector3.Angle(rotationTrans.rotation * Vector3.forward, Vector3.up);

        angleOffests.AddLast(new Result(sAng, hAng, tAng));

        RandomizeSoundPosition();
    }

    private void RandomizeSoundPosition()
    {
        showOptions = false;

        rotationTarget = Quaternion.Euler(Random.Range(-75.0f, 75.0f), Random.Range(-180.0f, 180.0f), 0.0f);
    }

    private void SaveResults()
    {
        ResultData rs = new ResultData();
        rs.angleOffsets = angleOffests.ToList();


        System.IO.Directory.CreateDirectory(Application.dataPath + "/../TestResults/");
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
        serializer.Serialize(stream, rs);
        stream.Close();
    }

    private void RotateTowardsTarget()
    {
        rotationTrans.rotation = Quaternion.RotateTowards(rotationTrans.rotation, rotationTarget, Time.deltaTime * 70);
        transform.localPosition = new Vector3(0.0f, 0.0f, 1.5f);
        if (Vector3.Angle(rotationTrans.rotation * Vector3.forward, rotationTarget * Vector3.forward) < showOptionsAngle)
        {
            showOptions = true;
        }

    }
}
