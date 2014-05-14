using UnityEngine;
using System.Collections;

public class RayCasting : MonoBehaviour {

	private RaycastHit hit = new RaycastHit();
	public GameObject character; //raycasted from

    public GameObject bullet;

    public AudioClip killSound;

    public AudioSource secondarySource;

	float rayCastInterval = 0.2f;
	float rayCastTimer;

	int raycastTargets = (1<<8) | (1<<9 | 1<<10); //layer nr 8 = door, layer nr 9 = wall, 10 = player

	// Use this for initialization
	void Start () {
        if(character == null)
            Debug.LogWarning("No player attached");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = character.transform.position - transform.position;

        if (character != null)
        {
            if (rayCastTimer >= 0.0f)
                rayCastTimer -= Time.deltaTime;

            if (rayCastTimer <= 0.0f)
            {
                rayCastTimer = rayCastInterval;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, 200, raycastTargets))
                {
                    if (hit.transform.gameObject.layer == 10)
                    {
                        PlayerController pc = hit.transform.gameObject.GetComponent<PlayerController>() as PlayerController;
                        if (!pc.isDead)
                        {
                            GameObject blt = Instantiate(bullet, transform.position + transform.rotation * new Vector3(-0.07588383f, 0.490696f, 1.050918f) , Quaternion.identity) as GameObject;
                            blt.GetComponent<BulletController>().SetDirection((pc.transform.position - transform.position - transform.rotation * new Vector3(-0.07588383f, 0.490696f, 1.050918f)).normalized);
                            pc.Kill(transform.position);

                            if (GameManager.instance.hrtfMode)
                            {
                                gameObject.GetComponent<TBE_3DCore.TBE_Source>().PlayOneShot(killSound);
                            }
                            else
                            {
                                secondarySource.Play();
                            }
                        }
                    }
                    Debug.DrawRay(transform.position, direction, Color.red); //show the raycasting at full length between the player and the object(enemy)
                }
            }
        }
	}
}
