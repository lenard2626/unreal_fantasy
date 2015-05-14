using UnityEngine;
using System.Collections;

public class enemyAttackTrigger : MonoBehaviour {
	

	public Transform player;

	private NavMeshAgent nma;
	private Animation childrenAnimation;
	private bool isApproaching=false;			//Dice si en determinado instante, el enemigo acecha al jugador


	void Start () {
		nma = gameObject.GetComponent<NavMeshAgent>();
		childrenAnimation = gameObject.GetComponentInChildren<Animation>();

	}
	void Update(){
		if (isApproaching && !childrenAnimation.IsPlaying ("Cube|walk")) {
			childrenAnimation.Play("Cube|walk");
		}
	}

	void OnTriggerEnter(Collider playerCollider){
		if (playerCollider.tag == "Player") {
			attack(playerCollider);
		}
	}

	void attack(Collider playerCollider){
		//Configuramos el navmeshagent para perseguir al jugador
		nma=GetComponent<NavMeshAgent>();
		nma.SetDestination(player.position);
	}
}
