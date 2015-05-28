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
 
	private Animation meshAnim;

	private Transform playerTrans;
	private playerStatusGUI playerStatusBar;
	private enemyPatrol enemyPatrolScript;
	private enemyStatusGUI enemyStatusScript;

	private bool isApproaching=false;			//Dice si en determinado instante, el enemigo acecha al jugador
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador

	private float attackCDTimer=0;

	void Start () {
		//parent=(GetComponent<Transform>().root).GetComponent<GameObject>();			///Lo que hay que hacer para sacarle el parent a un script...
		player=GameObject.Find("PersonajePrincipal");
		playerTrans = player.GetComponent<Transform>();
		meshAnim = GetComponentInChildren<Animation> ();

		//Configuramos el SphereCollider en base al radio de ataque
		SphereCollider attackTriggerScript = GetComponent<SphereCollider>();
		attackTriggerScript.radius = viewRange;
 
		enemyPatrolScript = GetComponent<enemyPatrol>();
		enemyStatusScript = GetComponent<enemyStatusGUI> ();
		playerStatusBar = player.GetComponent<playerStatusGUI>();

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
			Debug.Log("Ataca!!!!");
		}
	}
	void OnTriggerExit(Collider playerCollider){
		if (playerCollider.tag == "Player") {
			enemyPatrolScript.disableLocalMovement=false;
			isApproaching=false;
			isAttacking=false;
			Debug.Log("Ataca!!!!");
		}
	}

	void approach(){
		//if(!meshAnim.IsPlaying ("run"))meshAnim.Play("run");
		transform.position = Vector3.MoveTowards (transform.position,
		                                          new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z),
		                                            moveSpeed * Time.deltaTime);
		transform.LookAt(player.transform.position);
		//transform.LookAt (player.transform.position);

	}

	void attack(){
		if (enemyStatusScript.curHP <= 0) {
			die ();
		} else {
			if(Time.time - attackCDTimer > attackCoolDown) {  // espera entre ataques 
				//ataca
				playerStatusBar.setCurrentHP(playerStatusBar.getCurrentHP() - meleeDamage);
				//if(!meshAnim.IsPlaying ("attack"))meshAnim.Play("attack");
				attackCDTimer = Time.time;
			}
		}
	}


	public void die(){
		Debug.Log ("enemigo: me muero....");

		if (!meshAnim.IsPlaying ("die")) {
			//fadeObject(3.0f,0.0f);
			//Destroy(this);
			Application.LoadLevel("MainWorld");
		}
	}
	/*
	public void fadeObject(float time,float targetAlpha)
	{
		float t = 0.0f;

		Renderer[] thisRenderers = parent.GetComponents<Renderer> ();
		if(thisRenderers==null)												//En caso que el renderer no este en el padre...
			thisRenderers =this.GetComponents<Renderer> ();
		if(thisRenderers==null)												//En caso que el renderer no este en el objeto actual...
			thisRenderers =this.GetComponentsInChildren<Renderer> ();
		foreach(Renderer cur_render in thisRenderers){						
			
			var currentAlpha = cur_render.material.color.a;
			while(t <= 1)
			{
				cur_render.material.color = new Color(cur_render.material.color.r,
				                                      cur_render.material.color.g,
				                                      cur_render.material.color.b,
				                                         Mathf.Lerp(currentAlpha, targetAlpha, t));
				
				t += Time.deltaTime/time;
				
			}
			cur_render.material.color = new Color(cur_render.material.color.r,
			                                      cur_render.material.color.g,
			                                      cur_render.material.color.b,
			                                         targetAlpha);
		}
	}
	*/
}
