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
		playerTrans = player.GetComponent<Transform>();
		meshAnim = GetComponentInChildren<Animation> ();
 
		enemyPatrolScript = GetComponentInParent<enemyPatrol>();
		playerStatusBar = player.GetComponent<playerStatusGUI>();
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
		if (enemyStatusScript.curHP <= 0) {
			die ();
		} else {
			if(Time.time - attackCDTimer > attackCoolDown) {  // espera entre ataques 
				//ataca
				playerStatusBar.setCurrentHP(playerStatusBar.getCurrentHP() - meleeDamage);
				if(!meshAnim.IsPlaying ("attack"))meshAnim.Play("attack");
				attackCDTimer = Time.time;
			}
		}
	}


	public void die(){
		Debug.Log ("enemigo: me muero....");
		if (!meshAnim.IsPlaying ("die")) {
			fadeObject(3.0f,0.0f);
			Destroy(parent);
		}
	}

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
}
