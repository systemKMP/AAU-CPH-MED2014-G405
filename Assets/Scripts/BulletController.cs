using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    private Vector3 direction = Vector3.zero;
    public float speed;

    void Start()
    {
        Destroy(gameObject, 4.0f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}
