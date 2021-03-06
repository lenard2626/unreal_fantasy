﻿using UnityEngine;
using System.Collections;

public class enemyPatrol : MonoBehaviour {

	public Transform[] patrolPoints;
	public float moveSpeed = 10;

	public float maxRandStopTime= 5;
	public float minRandStopTime= 2;

	private int currentTarget = 0;
	public bool disableLocalMovement=false;			//para activar/desactivar el control del movimiento desde scripts externos
	private bool isIdle=false;

	private float maxCurrentIdleTime=0f;
	private float idleTimeCounter=0f;

	private Animator animtr;

	private Transform thisTransform;
	private NavMeshAgent nma;
	
	void Start () {
		transform.position = patrolPoints[0].position;
		currentTarget = 0;
		animtr = GetComponentInParent<Animator> ();
		thisTransform = GetComponent<Transform> ();
		if (!(nma = GetComponent<NavMeshAgent> ())) {
			nma = GetComponentInParent<NavMeshAgent> ();
		}
		if(nma!=null)
			nma.speed = moveSpeed;
	}	

	void Update () {
		if (!disableLocalMovement) {
			//Si ya llego al siguiente patrolpoint
			if(transform.position == patrolPoints[currentTarget].position){
				currentTarget++;
				isIdle=true;
				//Calcula el tiempo de espera en ese punto
				maxCurrentIdleTime = Random.Range(minRandStopTime,maxRandStopTime);
			}
			//Control de idle

			if (idleTimeCounter >= maxCurrentIdleTime) {
				isIdle = false;
				idleTimeCounter = 0;
			} else {
				idleTimeCounter+=Time.deltaTime;
			}
			if (currentTarget >= patrolPoints.Length) {
				currentTarget=0;
			}
			
			//Control de animacion
			if (isIdle) {
				animtr.SetBool("Idle",true);
				animtr.SetBool("Walking",false);
			} else {
				animtr.SetBool("Idle",false);
				animtr.SetBool("Walking",true);
			}


			if (!isIdle) {

				//Sin (farmaton) navMeshAgent

				thisTransform.position = Vector3.MoveTowards (transform.position,
				                                          patrolPoints [currentTarget].position,
				                                          moveSpeed * Time.deltaTime);
				thisTransform.LookAt(patrolPoints [currentTarget].position);

				//Con (farmaton) navMeshAgent
				if(nma!=null)
					nma.destination = patrolPoints [currentTarget].position;

			}
		}
	}
}
