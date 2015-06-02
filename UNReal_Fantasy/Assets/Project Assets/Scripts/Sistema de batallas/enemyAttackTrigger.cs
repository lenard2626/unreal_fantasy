using UnityEngine;
using System.Collections;

[RequireComponent(typeof (enemyStatusGUI))]

public class enemyAttackTrigger : MonoBehaviour {
	

	private GameObject player;						//Para acceder a componentes de player

	/*Variables publicas para configurar el ataque*/
	public float meleeDamage=50;
	public float meleeAttackRange=2;				//El rango de ataque del enemigo cuerp a cuerpo 
	public float viewRange=15;						//El rango en el cual debe entrar el jugador para ser atacado por el enemigo
	public float moveSpeed=5;
	public float attackCoolDown=3;					//0-10: rango de tiempo entre ataques

	private Transform playerTrans;
	private playerStatusGUI playerStatusBar;
	private playerAttack playerAtkScript;
	private enemyPatrol enemyPatrolScript;
	private enemyStatusGUI enemyStatusScript;

	private Animator pAnimtr;
	private SFX sfx;

	private bool isApproaching=false;			//Dice si en determinado instante, el enemigo acecha al jugador
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador

	private float attackCDTimer=0;

	void Start () {
		//parent=(GetComponent<Transform>().root).GetComponent<GameObject>();			///Lo que hay que hacer para sacarle el parent a un script...
		player=GameObject.Find("PersonajePrincipal");
		playerTrans = player.GetComponent<Transform>();
		pAnimtr = GetComponentInParent<Animator> ();

		//Configuramos el SphereCollider en base al radio de ataque
		SphereCollider attackTriggerScript = GetComponent<SphereCollider>();
		attackTriggerScript.radius = viewRange;
		sfx = GetComponent<SFX> ();
 
		enemyPatrolScript = GetComponent<enemyPatrol>();
		enemyStatusScript = GetComponent<enemyStatusGUI> ();
		playerStatusBar = player.GetComponent<playerStatusGUI>();
		playerAtkScript = player.GetComponent<playerAttack>();

		pAnimtr.speed =1.0f /attackCoolDown;						//Escala de velocidad de ataque

		meleeAttackRange *= meleeAttackRange;						//Usamos distancia al cuadrado para ahorrarnos la raiz cuadrada
	}
	void Update(){
		pAnimtr.SetBool ("Walking",isApproaching);
		pAnimtr.SetBool ("Attacking",isAttacking);

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
			//Debug.Log("Ataca!!!!");
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
		//if(!meshAnim.IsPlaying ("run"))meshAnim.Play("run");
		pAnimtr.SetBool ("Walking",true);
		transform.position = Vector3.MoveTowards (transform.position,
		                                          new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z),
		                                            moveSpeed * Time.deltaTime);
		transform.LookAt(player.transform.position);
		transform.LookAt (player.transform.position);

	}

	void attack(){
		if (enemyStatusScript.curHP <= 0) {
			die ();
		} else {
			if(Time.time - attackCDTimer > attackCoolDown) {  // espera entre ataques 
				//ataca
				playerStatusBar.setCurrentHP(playerStatusBar.getCurrentHP() - meleeDamage);
				//Reproduce un sonido de ataque asociado
				sfx.PlayRandomAttack();
				attackCDTimer = Time.time;
			}
		}
	}


	public void die(){
		Debug.Log ("enemigo: me muero....");
		Destroy (transform.parent.gameObject);
		playerAtkScript.win ();						//Le decimos al jugador que gano
	}
}
