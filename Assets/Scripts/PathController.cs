using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour
{

    public Transform[] patrolPoints;
    private int currentPoint;
    public float moveSpeed;
    public bool moveInCircle = true;
    public bool atEnd = false;

    private float targetChangeDistance = 0.5f;

    Vector3 movementDirection;

    // Use this for initialization
    void Start()
    {
        if (patrolPoints.Length > 0 && patrolPoints != null){
            transform.position = patrolPoints[0].position;
        }

        currentPoint = 0;
    }

    void Update()
    {
        if (patrolPoints.Length > 0 && patrolPoints != null)
        {
            if (moveInCircle)
            {
                if ((transform.position - patrolPoints[currentPoint].position).magnitude < targetChangeDistance)
                {
                    currentPoint++;
                }
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
                Vector3 target = patrolPoints[currentPoint].position - transform.position;

                movementDirection = Vector3.MoveTowards(movementDirection, target.normalized, Time.deltaTime * 4.0f);

                transform.position += movementDirection * Time.deltaTime * moveSpeed;

                transform.LookAt(movementDirection + transform.position);
            }
            else
            {
                if (patrolPoints.Length == 0 || patrolPoints[currentPoint] == null)
                {
                    transform.position = transform.position;
                }
                else
                {
                    if ((transform.position - patrolPoints[currentPoint].position).magnitude < targetChangeDistance && currentPoint != null)
                    {
                        if (!atEnd) /*This bool check if the drone has reached the last element in the array of waypoints*/
                        {
                            currentPoint++;
                            if (currentPoint >= patrolPoints.Length)
                                atEnd = true; /*Once it reaches the last element tell the drone that has reached the end*/
                        }
                        if (atEnd)
                        {
                            currentPoint--; /*Makes the drone go backwards, the same path as it walked before, just opposite*/
                            if (currentPoint <= 0)
                                atEnd = false; 
                        }
                    }

                    Vector3 target = patrolPoints[currentPoint].position - transform.position;

                    movementDirection = Quaternion.RotateTowards(Quaternion.LookRotation(movementDirection), Quaternion.LookRotation(target), Time.deltaTime * 400.0f) * Vector3.forward;

                    transform.position += movementDirection * Time.deltaTime * moveSpeed;

                    transform.LookAt(movementDirection + transform.position);
                }
            }
        }
    }
}
