﻿using UnityEngine;
using System.Collections;

public class RayCasting : MonoBehaviour {

	private RaycastHit hit = new RaycastHit();
	public GameObject character; //raycasted from

	float rayCastInterval = 0.2f;
	float rayCastTimer;

	int raycastTargets = (1<<8) | (1<<9); //layer nr 8 = enemy, layer nr 9 = wall

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = character.transform.position - transform.position;
		if(rayCastTimer >= 0.0f)
			rayCastTimer -= Time.deltaTime;

		if(rayCastTimer <= 0.0f)
		{
			rayCastTimer = rayCastInterval;
			if(Physics.Raycast(transform.position, direction.normalized, out hit, 200, raycastTargets)) 
			{
				if(hit.transform.gameObject.layer == 8)
				{
					Destroy(hit.transform.gameObject);
				}
				Debug.DrawRay(transform.position, direction, Color.red); //show the raycasting at full length between the player and the object(enemy)
				Debug.Log(hit.transform.gameObject.layer);
			}
		}
	}
}
