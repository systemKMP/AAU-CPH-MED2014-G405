using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject spawnObject;
    private SpawnPointController currentSpawnPoint;

    public CharacterController characterController;
    public MouseLook mouseLook;
    public CharacterMotor characterMotor;
    public FPSInputController fpsInputController;

    public CapsuleCollider capsuleCollider;

    public bool isDead;

    private float deathTimer;
    private float respawnTime = 4.0f;

	// Use this for initialization
	void Start () {
        rigidbody.isKinematic = true;
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0.0f)
            {
                Respawn();
            }
        }

        if (Input.GetKeyDown(KeyCode.O)){
            Respawn();
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Checkpoint")
        {
            SpawnPointController spc = col.GetComponent<SpawnPointController>();
            if (currentSpawnPoint != spc)
            {
                currentSpawnPoint = spc;
                currentSpawnPoint.SpawnPointEntered();
            }
        }
    }

    public void Kill(Vector3 pos)
    {
        TestResultManager.instance.FailedCurrentRoom();

        isDead = true;
        deathTimer = respawnTime;

        fpsInputController.enabled = false;
        characterMotor.enabled = false;
        mouseLook.enabled = false;
        characterController.enabled = false;

        rigidbody.isKinematic = false;
        capsuleCollider.enabled = true;
        rigidbody.constraints = RigidbodyConstraints.None;

        rigidbody.AddForce((transform.position - pos).normalized * 20.0f);
    }

    private void Respawn()
    {
        isDead = false;
        
        characterController.enabled = true;
        fpsInputController.enabled = true;
        characterMotor.enabled = true;
        mouseLook.enabled = true;

        if (!rigidbody.isKinematic)
        {
            rigidbody.velocity = Vector3.zero;
        }
        
        rigidbody.isKinematic = true;
        capsuleCollider.enabled = false;

        transform.position = currentSpawnPoint.transform.position;
        transform.rotation = currentSpawnPoint.transform.rotation;
        currentSpawnPoint.RefreshDoorState();
    }
}
