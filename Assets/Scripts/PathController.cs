using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour {

	public Transform[] patrolPoints;
	private int currentPoint;
	public float moveSpeed;
	public bool moveInCircle = true;
	public bool atEnd = false;

	private float targetChangeDistance = 0.5f;

	Vector3 movementDirection;

	// Use this for initialization
	void Start () 
	{
		transform.position = patrolPoints[0].position;
		currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(moveInCircle)
		{
            if (patrolPoints[currentPoint] == null)
            {
                transform.position = transform.position;
            }
            else
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
		}
		else 
		{
            if (patrolPoints[currentPoint] == null)
            {
                transform.position = transform.position;
            }
            else
            {
                if ((transform.position - patrolPoints[currentPoint].position).magnitude < targetChangeDistance && currentPoint != null)
                {
                    if (!atEnd)
                    {
                        currentPoint++;
                        if (currentPoint >= patrolPoints.Length)
                            atEnd = true;
                    }
                    if (atEnd)
                    {
                        currentPoint--;
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
