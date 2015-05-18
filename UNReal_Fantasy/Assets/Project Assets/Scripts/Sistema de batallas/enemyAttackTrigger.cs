using UnityEngine;
using System.Collections;

public class enemyAttackTrigger : MonoBehaviour {
	

	public GameObject player;						//Para acceder a componentes de player
	public GameObject parent;						//Para acceder a componentes del parent

	/*Variables publicas para configurar el ataque*/
	public float meleeDamage=50;
	public float meleeAttackRange=2;				//El rango de ataque del enemigo cuerp a cuerpo 
	public float moveSpeed=5;
	public float attackCoolDown=3;					//Segundos entre ataques

	private Transform parentTrans;
	private Animation meshAnim;

	private Transform playerTrans;
	private statusBar playerStatusBar;
	private enemyPatrol enemyPatrolScript;
	private enemyStatusGUI enemyStatusScript;

	private bool isApproaching=false;			//Dice si en determinado instante, el enemigo acecha al jugador
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador

	private float attackCDTimer=0;

	void Start () {

		playerTrans = player.GetComponent<Transform>();
		meshAnim = parent.GetComponent<Animation> ();

		parentTrans = parent.GetComponent<Transform> ();
		enemyPatrolScript = GetComponentInParent<enemyPatrol>();
		playerStatusBar = player.GetComponent<statusBar>();
		enemyStatusScript = GetComponent<enemyStatusGUI> ();

		meleeAttackRange *= meleeAttackRange;						//Usamos distancia al cuadrado para ahorrarnos la raiz cuadrada
	}
	void Update(){
		var distance = Vector3.SqrMagnitude (transform.position - playerTrans.transform.position);

		if (distance < meleeAttackRange) {
			isAttacking = true;
			isApproaching = false;
		} else if ((distance > meleeAttackRange)&&isAttacking) {
			isAttacking=false;
			isApproaching = true;
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
			enemyPatrolScript.disableLocalMovement=true;
			isApproaching=true;
			enemyStatusScript.ShowStatus();
		}
	}
	void OnTriggerExit(Collider playerCollider){
		if (playerCollider.tag == "Player") {
			enemyPatrolScript.disableLocalMovement=false;
			isApproaching=false;
			isAttacking=false;
		}
	}

	void approach(){
		if(!meshAnim.IsPlaying ("run"))meshAnim.Play("run");
		transform.position = Vector3.MoveTowards (transform.position,
		                                          new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z),
		                                            moveSpeed * Time.deltaTime);

	}

	void attack(){
		if(Time.time - attackCDTimer > attackCoolDown) {  // espera entre ataques 
			//ataca
			playerStatusBar.curHP -= meleeDamage;
			attackCDTimer = Time.time;
		}
	}
}
