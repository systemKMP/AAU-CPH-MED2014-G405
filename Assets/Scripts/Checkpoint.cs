using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //looks for objects tagged as "Player"
        {
            spawnPoint.position = new Vector3(transform.position.x, spawnPoint.position.y, transform.position.z); //Sets the new spawn point to whichever checkpoint object last tagged by the player
            //Destroy(gameObject);
        }
    }
}
