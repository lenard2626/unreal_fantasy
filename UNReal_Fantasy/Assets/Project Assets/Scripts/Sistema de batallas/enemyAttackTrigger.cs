﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (enemyStatusGUI))]

public class enemyAttackTrigger : MonoBehaviour {

	private GameObject player;						//Para acceder a componentes de player

	/*Variables publicas para configurar el ataque*/
	public float meleeDamage=50;
	public float meleeAttackRange=1;				//El rango de ataque del enemigo cuerp a cuerpo 
	public float viewRange;						//El rango en el cual debe entrar el jugador para ser atacado por el enemigo
	public float moveSpeed;
	public float attackCoolDown=3;					//0-10: rango de tiempo entre ataques

	private Transform playerTrans;
	private playerStatusGUI playerStatusBar;
	private playerAttack playerAtkScript;
	//private enemyPatrol enemyPatrolScript;
	private enemyStatusGUI enemyStatusScript;

	private Animator pAnimtr;
	private SFX sfx;

	private bool isApproaching=false;			//Dice si en determinado instante, el enemigo acecha al jugador
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador

	private float attackCDTimer=0;

	private NavMeshAgent nma;
	private float ptd_distance;


	void Start () {
		//parent=(GetComponent<Transform>().root).GetComponent<GameObject>();			///Lo que hay que hacer para sacarle el parent a un script...
		player=GameObject.Find("PersonajePrincipal");
		playerTrans = player.GetComponent<Transform>();
		pAnimtr = GetComponentInParent<Animator> ();

		//Configuramos el SphereCollider en base al radio de ataque
		SphereCollider attackTriggerScript = GetComponent<SphereCollider>();
		attackTriggerScript.radius = viewRange;
		sfx = GetComponent<SFX> ();
 
		//enemyPatrolScript = GetComponent<enemyPatrol>();
		enemyStatusScript = GetComponent<enemyStatusGUI> ();
		playerStatusBar = player.GetComponent<playerStatusGUI>();
		playerAtkScript = player.GetComponent<playerAttack>();

		nma = GetComponentInParent<NavMeshAgent>();

		pAnimtr.speed =1.0f /attackCoolDown;						//Escala de velocidad de ataque

		meleeAttackRange *= meleeAttackRange;						//Usamos distancia al cuadrado para ahorrarnos la raiz cuadrada
	}
	void Update(){
		pAnimtr.SetBool ("Walking",isApproaching);
		pAnimtr.SetBool ("Attacking",isAttacking);

		ptd_distance = Vector3.SqrMagnitude (transform.position - playerTrans.transform.position);

		if (ptd_distance <= meleeAttackRange + nma.radius * nma.radius) {
			isAttacking = true;
			isApproaching = false;
		} else {
			if (ptd_distance <= viewRange * viewRange) {
				isApproaching = true;
			} else {
				isApproaching = false;
			}
			isAttacking=false;
		} 
		if (isApproaching) {
			approach ();
		}
		if(isAttacking){
			attack ();
		}

	}

	void OnTriggerEnter(Collider playerCollider){
		if (playerCollider.tag == "Player") {
			//enemyPatrolScript.disableLocalMovement=true;
			isApproaching=true;
			enemyStatusScript.ShowStatus();
			Debug.Log("Ataca!!!!");
		}
	}
	void OnTriggerExit(Collider playerCollider){
		if (playerCollider.tag == "Player") {
			//enemyPatrolScript.disableLocalMovement=false;
			isApproaching=false;
			isAttacking=false;
		}
	}

	void approach(){
		nma.destination = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z);
		pAnimtr.SetBool ("Walking",true);
		Debug.Log("Acecha!!!!");
		/*
		transform.position = Vector3.MoveTowards (transform.position,
		                                          new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z),
		                                          moveSpeed * Time.deltaTime);

		transform.LookAt (player.transform.position);
		*/
	}

	void attack(){
		//Desactivamos caminata
		pAnimtr.SetBool ("Walking",false);
		//Nos aseguramos de que este pelmazo siempre mire al jugador
		transform.LookAt(player.transform.position);

		if(playerStatusBar.getCurrentHP()<=0){
			win();
		}
		if (enemyStatusScript.curHP <= 0) {
			die ();
		} else {
			//Forza el ataque en el jugador
			//playerAtkScript.setAttackingEnemy(enemyStatusScript);

			if(Time.time - attackCDTimer > attackCoolDown) {  // espera entre ataques 
				//ataca
				playerStatusBar.setCurrentHP(playerStatusBar.getCurrentHP() - calculateAttackDamage());
				//Reproduce un sonido de ataque asociado
				sfx.PlayRandomAttack();
				attackCDTimer = Time.time;
			}
		}
	}
	private int calculateAttackDamage(){
		return (int)((1.0f*meleeDamage)*Random.Range(0.8f,1.2f));
	}

	public void die(){
		Debug.Log ("enemigo: me muero....");
		playerAtkScript.win ();						//Le decimos al jugador que gano
		pAnimtr.SetBool("Lose",true);
	}
	public void win(){
		Debug.Log ("enemigo: gane....muahahaha");
		playerAtkScript.die ();						//Le decimos al jugador que gano
		pAnimtr.SetBool("VictoryDance",true);
	}
}
