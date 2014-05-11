using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {

    public DoorController[] nextDoors;
    public DoorController[] previousDoors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnPointEntered()
    {
        foreach (DoorController door in previousDoors)
        {
            door.PermanentClose();
        }

        foreach (DoorController door in nextDoors)
        {
            door.InstantClose();
        }
    }
}
