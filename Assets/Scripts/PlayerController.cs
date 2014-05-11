using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject spawnObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnterTrigger(Collider col)
    {

        spawnObject = col.gameObject;
    }

    public void Kill()
    {

    }
}
