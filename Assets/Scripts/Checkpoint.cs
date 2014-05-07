using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpawnPoint") //looks for objects tagged as "Player"
        {
            spawnPoint = other.transform;
        }
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

}
