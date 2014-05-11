using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;


public class TTManager : MonoBehaviour
{

    private bool allowMovement = false;

    public GameObject rotater;
    public GameObject soundDisplay;
    public MouseLook cameraRotater;

    private float targetRotation;

    private TTState currentState;

    private float minAngDelta = 0.5f;

    public static TTManager instance;

    private float soundMovementAmount;

    private TTSGU ttsgu;
    private List<TTSGU> ttsguContainer;

    private bool showSource;

    private float minTransitionTime = 2.0f;
    private float currentTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        ttsguContainer = new List<TTSGU>();
        SetState(TTState.findSound);
    }



    void Update()
    {
        //Debug.Log(Random.Range(0, 12));
        switch (currentState)
        {
            case TTState.findSound:
                HandleSoundSearch();
                break;
            case TTState.askFocus:
                HandleFocusState();
                break;
            case TTState.soundMooving:
                MoveSound();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
        {
            SaveResults();
            Application.LoadLevel(0);
        }
    }

    void SetState(TTState state)
    {
        TTGUIControler.instance.SetState(state);
        currentState = state;
        switch (state)
        {
            case TTState.findSound:
                RandomNewPosition();
                soundDisplay.SetActive(true);
                allowMovement = true;
                break;
            case TTState.askFocus:
                soundDisplay.SetActive(true);
                allowMovement = true;
                break;
            case TTState.soundMooving:
                currentTime = 0.0f;
                allowMovement = false;
                soundDisplay.SetActive(false);
                StartMoovingSound();
                break;
            case TTState.showGuesses:
                soundDisplay.SetActive(false);
                break;
        }
        cameraRotater.enabled = allowMovement;
    }

    void HandleFocusState()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, 100.0f, 1 << 12))
            {

                SetState(TTState.soundMooving);
            }
        }
    }

    void HandleSoundSearch()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, 100.0f, 1 << 13))
            {
                SetState(TTState.askFocus);
            }
        }
    }

    void StartMoovingSound()
    {
        SelectTarget();
    }

    void SelectTarget()
    {
        ttsgu.movementAmount = Random.Range(-2, 3) * 20.0f;
        targetRotation = ttsgu.movementAmount + ttsgu.originLocation;
    }

    public void RandomNewPosition()
    {
        ttsgu = new TTSGU();
        ttsgu.originLocation = Random.Range(0, 12) * 30.0f;
        Debug.Log(ttsgu.originLocation);
        rotater.transform.rotation = Quaternion.Euler(0.0f, ttsgu.originLocation, 0.0f);
    }



    void MoveSound()
    {
        currentTime += Time.deltaTime;

        Quaternion targetRot = Quaternion.Euler(0.0f, targetRotation, 0.0f);
        rotater.transform.rotation = Quaternion.RotateTowards(rotater.transform.rotation, targetRot, Time.deltaTime * 20.0f);
        if (Vector3.Angle(rotater.transform.rotation * Vector3.forward, targetRot * Vector3.forward) < minAngDelta && currentTime > minTransitionTime)
        {
            SetState(TTState.showGuesses);
        }
    }

    private void SaveResults()
    {
        TTSGUContainer rs = new TTSGUContainer();
        rs.guesses = ttsguContainer;


        System.IO.Directory.CreateDirectory(Application.dataPath + "/../Test3Results/");
        var serializer = new XmlSerializer(typeof(TTSGUContainer));

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

        var stream = new FileStream(Application.dataPath + "/../Test3Results" + "/Result" + time + ".xml", FileMode.Create);
        serializer.Serialize(stream, rs);
        stream.Close();
    }


    public void GuessMade(float degrees)
    {
        ttsgu.guessAmount = degrees;
        ttsguContainer.Add(ttsgu);
        SetState(TTState.findSound);
    }
}
