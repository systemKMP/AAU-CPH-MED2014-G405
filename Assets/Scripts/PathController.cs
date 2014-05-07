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

	Vector3 velocity;

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
			if((transform.position - patrolPoints[currentPoint].position).magnitude < targetChangeDistance)
			{
				currentPoint++;
			}
			if(currentPoint >= patrolPoints.Length)
			{
					currentPoint = 0;
			}
			Vector3 target = patrolPoints[currentPoint].position - transform.position;

			velocity = Vector3.MoveTowards(velocity, target.normalized,Time.deltaTime*4.0f);

			transform.position += velocity * Time.deltaTime * moveSpeed;

			transform.LookAt(velocity+transform.position);
		}
		else 
		{
			if((transform.position - patrolPoints[currentPoint].position).magnitude < targetChangeDistance)
			{
				if(!atEnd)
				{
					currentPoint++;
					if(currentPoint >= patrolPoints.Length)
						atEnd = true;
				}
				if(atEnd)
					currentPoint--;
			}
			Vector3 target = patrolPoints[currentPoint].position - transform.position;
			
			velocity = Vector3.MoveTowards(velocity, target.normalized,Time.deltaTime*4.0f);
			
			transform.position += velocity * Time.deltaTime * moveSpeed;
			
			transform.LookAt(velocity+transform.position);
		}
	}
}
