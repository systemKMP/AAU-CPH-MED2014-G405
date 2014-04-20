using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public enum SoundPosition { front, back, left, right, up, down };

public class TestInput : MonoBehaviour
{

    public Transform rotationTrans;

    private Quaternion rotationTarget;

    int totalGuessCount = 0;

    int[] occuredGuessCount;
    int[] correctGuesses;

    private SoundPosition currentSoundPos;

    // Use this for initialization
    void Start()
    {
        occuredGuessCount = new int[6];
        correctGuesses = new int[6];

        RandomizeSoundPosition();
    }

    void Update()
    {
        RotateTowardsTarget();
    }

    // Update is called once per frame
    void OnGUI()
    {
        //according to enum
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 250, 100, 100), "front"))
        {
            GuessPosition((int)SoundPosition.front);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 150, 100, 100), "back"))
        {
            GuessPosition((int)SoundPosition.back);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 50, 100, 100), "left"))
        {
            GuessPosition((int)SoundPosition.left);
        }
        if (GUI.Button(new Rect(Screen.width / 2 + 200, Screen.height / 2 - 50, 100, 100), "right"))
        {
            GuessPosition((int)SoundPosition.right);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 120, 100, 100), "top"))
        {
            GuessPosition((int)SoundPosition.up);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 20, 100, 100), "botton"))
        {
            GuessPosition((int)SoundPosition.down);
        }
        if (totalGuessCount > 20)
        {
            if (GUI.Button(new Rect(0, 0, 100, 100), "Finish"))
            {
                SaveResults();
                Application.LoadLevel(0);
            }
        }
    }

    private void GuessPosition(int guessIndex)
    {
        totalGuessCount++;
        occuredGuessCount[(int)currentSoundPos]++;
        if ((int)currentSoundPos == guessIndex)
        {
            correctGuesses[(int)currentSoundPos]++;
        }
        RandomizeSoundPosition();
    }

    private void RandomizeSoundPosition()
    {
        int newPos = (int)currentSoundPos;
        while (newPos == (int)currentSoundPos)
        {
            newPos = Random.Range(0, 6);
        }

        currentSoundPos = (SoundPosition)newPos;

        switch ((int)currentSoundPos)
        {
            case 0:
                rotationTarget = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                rotationTarget = Quaternion.Euler(0, 180, 0);
                break;
            case 2:
                rotationTarget = Quaternion.Euler(0, -90, 0);
                break;
            case 3:
                rotationTarget = Quaternion.Euler(0, 90, 0);
                break;
            case 4:
                rotationTarget = Quaternion.Euler(-90, 0, 0);
                break;
            case 5:
                rotationTarget = Quaternion.Euler(90, 0, 0);
                break;

        }
        //transform.position = spawnPositions[(int)currentSoundPos].position;
    }

    private void SaveResults()
    {
        ResultData rs = new ResultData();
        rs.guessAttempts = occuredGuessCount;
        rs.correctGuesses = correctGuesses;
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

        var stream = new FileStream(Application.dataPath + "/../TestResults" + "/Result" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + ".xml", FileMode.Create);
        serializer.Serialize(stream, rs);
        stream.Close();
    }

    private void RotateTowardsTarget()
    {
        rotationTrans.rotation = Quaternion.RotateTowards(rotationTrans.rotation, rotationTarget, Time.deltaTime*70);
        transform.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
    }
}
