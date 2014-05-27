using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {

    public DoorController[] nextDoors;
    public DoorController[] previousDoors;
    public GameObject[] nextStageEnemies;
    public GameObject[] previousStageEnemies;
    public Elavator elavator;

    private bool triggered = false;
    public bool testTrigger = false;

    private void HandleEnemies()
    {
        foreach (GameObject enemy in previousStageEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        foreach (GameObject enemy in nextStageEnemies)
        {
            enemy.SetActive(true);
        }
    }
    public void SpawnPointEntered()
    {
        if (testTrigger && !triggered)
        {
            TestResultManager.instance.RoomFinished();
            triggered = true;
        }

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
