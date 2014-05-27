using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the core functionalities for the players character
/// </summary>
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

	void Start () {
        rigidbody.isKinematic = true;
        isDead = false;
	}
	
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

    /// <summary>
    /// Takes care of approprate actions for killing the player
    /// </summary>
    /// <param name="pos">position of the enemy that killed the player</param>
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

    /// <summary>
    /// Respawn the players character
    /// </summary>
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
