using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {

    public DoorController[] nextDoors;
    public DoorController[] previousDoors;

    public GameObject[] nextStageEnemies;
    public GameObject[] previousStageEnemies;

    private void HandleEnemies()
    {
        foreach (GameObject enemy in nextStageEnemies)
        {
            enemy.SetActive(true);
        }

        foreach (GameObject enemy in previousStageEnemies)
        {
            enemy.SetActive(false);
        }
    }
    public void SpawnPointEntered()
    {
        foreach (DoorController door in previousDoors)
        {
            door.PermanentClose();
        }
        HandleEnemies();
    }

    public void RefreshDoorState()
    {
        foreach (DoorController door in nextDoors)
        {
            door.InstantClose();
        }
    }
}
