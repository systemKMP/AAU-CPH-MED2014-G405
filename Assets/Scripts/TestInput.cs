using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public enum SoundPosition { front, back, left, right, up, down };

public class TestInput : MonoBehaviour
{

    int totalGuessCount = 0;

    public Transform[] spawnPositions; //ENTER ACCORDING TO ENUM

    int[] occuredGuessCount;
    int[] correctGuesses;

    private SoundPosition currentSoundPos;

    Vector3 originalPos;
    Vector3 randOffsetTarget;
    float offsetResetDist = 0.05f;
    public float maxOffsetDist;
    public float offsetMoveSpeed;
    float currentTime;

    // Use this for initialization
    void Start()
    {
        occuredGuessCount = new int[6];
        correctGuesses = new int[6];

        RandomizeSoundPosition();
        NewOffsetTarget();

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, randOffsetTarget, Time.deltaTime * offsetMoveSpeed);
        if ((transform.position - randOffsetTarget).magnitude < offsetResetDist)
        {
            randOffsetTarget = originalPos + new Vector3(Random.Range(-maxOffsetDist, maxOffsetDist), Random.Range(-maxOffsetDist, maxOffsetDist), Random.Range(-maxOffsetDist, maxOffsetDist));
        }
    }

    void NewOffsetTarget()
    {
        originalPos = transform.position;
        randOffsetTarget = originalPos + new Vector3(Random.Range(-maxOffsetDist, maxOffsetDist), Random.Range(-maxOffsetDist, maxOffsetDist), Random.Range(-maxOffsetDist, maxOffsetDist));
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
        NewOffsetTarget();
    }

    private void RandomizeSoundPosition()
    {
        currentSoundPos = (SoundPosition)Random.Range(0, 6);
        transform.position = spawnPositions[(int)currentSoundPos].position;
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
}
