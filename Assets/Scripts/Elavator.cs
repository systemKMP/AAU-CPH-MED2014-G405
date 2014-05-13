using UnityEngine;
using System.Collections;

public class Elavator : MonoBehaviour {

    public bool finished = false;
    public bool isRunning = false;

    public Transform targetPos;

    private float stateTransitionDist = 0.05f;

    void Start()
    {

    }

    void Update()
    {
        if (isRunning)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * 0.7f);
            if ((transform.position - targetPos.position).magnitude < stateTransitionDist)
            {
                isRunning = false;
                finished = true;
            }
        }

    }

    public void ActivateLift()
    {
        if (!finished)
        {
            isRunning = true;
        }
    }
}
