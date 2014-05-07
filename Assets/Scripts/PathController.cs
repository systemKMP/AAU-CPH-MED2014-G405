using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour {

	public Transform[] patrolPoints;
	private int currentPoint;
	public float moveSpeed;
	public bool moveInCircle = true;

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
			if(transform.position == patrolPoints[currentPoint].position)
			{
				currentPoint++;
			}
			if(currentPoint >= patrolPoints.Length)
			{
					currentPoint = 0;
			}
			Quaternion target = Quaternion.LookRotation(patrolPoints[currentPoint].position - transform.position);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 5.0f);
			
			transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed* Time.deltaTime);
		}
	}
}
