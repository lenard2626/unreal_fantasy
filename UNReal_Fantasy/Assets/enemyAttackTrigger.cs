using UnityEngine;
using System.Collections;

public class enemyAttackTrigger : MonoBehaviour {

	public int runningSpeed = 2;
	public GameObject player;

	private bool isApproaching = false;
	private Vector3 playerPos;

	// Use this for initialization
	void Start () {
		
	}
	void Update(){
		if(isApproaching)
			Vector3.Lerp(transform.position,playerPos,runningSpeed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider playerCollider){
		if (playerCollider.tag == "Player") {
			attack(playerCollider);
			Debug.Log("entra en la zona roja");
		}
	}

	void attack(Collider playerCollider){
		//Primero, se acerca al jugador
		playerPos = playerCollider.transform.position;
		isApproaching = true;
		//
	}
}
