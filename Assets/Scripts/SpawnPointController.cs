using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {

    public DoorController[] nextDoors;
    public DoorController[] previousDoors;

    public GameObject[] nextStageEnemies;
    public GameObject[] previousStageEnemies;

    public Elavator elavator;

    private void HandleEnemies()
    {
        foreach (GameObject enemy in previousStageEnemies)
        {
            Destroy(enemy);
            //enemy.SetActive(false);
        }
        foreach (GameObject enemy in nextStageEnemies)
        {
            enemy.SetActive(true);
        }
    }
    public void SpawnPointEntered()
    {
        foreach (DoorController door in previousDoors)
        {
            door.PermanentClose();
        }
        HandleEnemies();

        if (elavator != null)
        {
            elavator.ActivateLift();
        }
    }

    public void RefreshDoorState()
    {
        foreach (DoorController door in nextDoors)
        {
            door.InstantClose();
        }
    }
}
