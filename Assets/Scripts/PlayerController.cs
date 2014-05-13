using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject spawnObject;
    private SpawnPointController currentSpawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Checkpoint")
        {
            currentSpawnPoint = col.GetComponent<SpawnPointController>();
            currentSpawnPoint.SpawnPointEntered();
        }
        //spawnObject = col.gameObject;
    }

    public void Kill()
    {
        transform.position = currentSpawnPoint.transform.position;
        currentSpawnPoint.RefreshDoorState();
    }
}
