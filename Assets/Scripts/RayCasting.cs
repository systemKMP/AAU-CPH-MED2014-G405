using UnityEngine;
using System.Collections;

public class RayCasting : MonoBehaviour {

	private RaycastHit hit = new RaycastHit();
	public GameObject character; //raycasted from
    public GameObject bullet;
    public AudioClip killSound;
    public AudioSource secondarySource;

	float rayCastInterval = 0.2f; // how often the cast should be done
	float rayCastTimer;
	int raycastTargets = (1<<8) | (1<<9 | 1<<10); //layer nr 8 = door, layer nr 9 = wall, 10 = player

	void Start () {
        if(character == null)
            Debug.LogWarning("No player attached"); //this is to warn the programmer if there is a drone that has no target
	}
	
	void Update () {
		Vector3 direction = character.transform.position - transform.position; //a vector from the enemy to the player

        if (character != null) //as long as there is a target it should run all of the code
        {
            if (rayCastTimer >= 0.0f)
                rayCastTimer -= Time.deltaTime; //as long as the raycast's timer is above 0 then it should subtract in seconds due to Time.deltatime

            if (rayCastTimer <= 0.0f)
            {
                rayCastTimer = rayCastInterval; //once the raycast timer hits 0 it should go back to its timer, this is done to ease the games performance
                if (Physics.Raycast(transform.position, direction.normalized, out hit, 200, raycastTargets))
                {
                    if (hit.transform.gameObject.layer == 10)
                    {
                        PlayerController pc = hit.transform.gameObject.GetComponent<PlayerController>() as PlayerController;
                        if (!pc.isDead)
                        {
                            GameObject blt = Instantiate(bullet, transform.position + transform.rotation * new Vector3(-0.07588383f, 0.490696f, 1.050918f) , Quaternion.identity) as GameObject; //initiates a bullet towards the player 
                            blt.GetComponent<BulletController>().SetDirection((pc.transform.position - transform.position - transform.rotation * new Vector3(-0.07588383f, 0.490696f, 1.050918f)).normalized);
                            pc.Kill(transform.position); //kill the player

                            if (GameManager.instance.hrtfMode)//checks if it is running in hrtf mode
                            {
                                TBE_3DCore.TBE_Source tbes = gameObject.GetComponent<TBE_3DCore.TBE_Source>() as TBE_3DCore.TBE_Source; //simply plays the sound with the hrtf settigns
                                tbes.PlayOneShot(killSound);
                            }
                            else
                            {
                                secondarySource.Play(); //plays the sound with unity standard sound
                            }
                        }
                    }
                    Debug.DrawRay(transform.position, direction, Color.red); //show the raycasting at full length between the player and the object(enemy)
                }
            }
        }
	}
}
