using UnityEngine;
using System.Collections;

public class SingleSoundPlayer : MonoBehaviour {

    public AudioClip audio;

    public float playInterval;
    private float currentTime;

    void Start()
    {
        currentTime = playInterval;
    }

	// Update is called once per frame
	void Update () {
        //currentTime -= Time.deltaTime;
        //if (currentTime < 0.0f)
        //{
        //    gameObject.GetComponent<TBE_3DCore.TBE_Source>().PlayOneShot(audio);
        //    currentTime += playInterval;
        //}
	}
}
