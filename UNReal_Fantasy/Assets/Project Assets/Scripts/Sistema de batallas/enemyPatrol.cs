using UnityEngine;
using System.Collections;

public class enemyPatrol : MonoBehaviour {

	public Transform[] patrolPoints;
	public float moveSpeed = 10;
	private int currentTarget = 0;
	public float maxRandStopTime= 5;
	public float minRandStopTime= 2;

	public bool disableLocalMovement=false;			//para activar/desactivar el control del movimiento desde scripts externos
	private bool isIdle=false;

	private float maxCurrentIdleTime=0f;
	private float idleTimeCounter=0f;

	private Animation anim;
	
	void Start () {
		transform.position = patrolPoints[0].position;
		currentTarget = 0;
		anim = GetComponentInChildren<Animation> ();
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
			if (isIdle && !anim.IsPlaying("attack")) {//TODO: Cambiar por la animacion "idle"
				anim.Play("attack");		
			} else {
				if(!anim.IsPlaying("walk") && !isIdle) anim.Play("walk");
			}
			
			if (!isIdle) {
				transform.position = Vector3.MoveTowards (transform.position,
				                                          patrolPoints [currentTarget].position,
				                                          moveSpeed * Time.deltaTime);
			}
		}
	}
}
