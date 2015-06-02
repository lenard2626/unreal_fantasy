using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(playerInfoGUI))]

public class playerAttack : MonoBehaviour {
	
	/*Variables publicas para configurar el ataque*/
	public float meleeDamage=70;
	public float attackCoolDown=5;
	public float meleeAttackRange=5;				//El rango de ataque del enemigo cuerp a cuerpo 			
	public float moveSpeed=10;
	
	private float attackCDTimer=0;
	private bool isTargetSelected=false;		//Dice si en determinado instante, el jugador tiene un objetivo señalado
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador

	private CapsuleCollider ccollider;			//Necesario para evitar colisiones

	private Animator animtr;
	private SFX sfx;

	private ThirdPersonUserControl tpc;
	
	private enemyStatusGUI attackedEnemyScript=null;		//acceso al script de estado del enemigo
	
	void Start () {
		animtr = GetComponent<Animator> ();
		sfx = GetComponentInChildren<SFX> ();
		ccollider = GetComponent<CapsuleCollider> ();
		tpc = GetComponent<ThirdPersonUserControl> ();	//Para usar las funciones de movimiento de ethan
		meleeAttackRange *= meleeAttackRange;			//Usamos distancia al cuadrado para ahorrarnos la raiz cuadrada
	}
	// Update is called once per frame
	void Update () {
		//Evalua si esta atacando un enemigo
		if (attackedEnemyScript != null && isTargetSelected) {			//Si ha seleccionado a un enemigo, este valor no debe ser nulo
			var distanceToEnemy = Vector3.SqrMagnitude (attackedEnemyScript.getTransform().position - transform.position);
			//Debug.Log("Distancia al enemigo "+distanceToEnemy);
			if (meleeAttackRange+(ccollider.radius*ccollider.radius)>= distanceToEnemy) {
				isAttacking=true;
				tpc.setFollow(Vector3.zero);
			}else{
				approachToTarget();
			}
			if (isAttacking) {
				attack();
			}
		}
	}

	public void setAttackingEnemy(enemyStatusGUI aesIn){
		attackedEnemyScript=aesIn;						//Funcion que recibe el componente "enemyStatusScript", para que el personaje principal pueda atacar al enemigo
		isTargetSelected = true;
	}
	
	public void approachToTarget(){
		//Debug.Log("acercandose al enemigo...");

		//Mueve el personaje
		if (attackedEnemyScript != null) {
			Vector3 des = (new Vector3 (attackedEnemyScript.getTransform ().position.x,transform.position.y,attackedEnemyScript.getTransform ().position.z))-transform.position;
			//Moviendo al personaje junto al anim state
			tpc.setFollow (des);
		}
	}
	public void stop(){
		Debug.Log("alto!!!");
		//Decimos que no siga nada
		tpc.setFollow (Vector3.zero);
		setAttacking (false);
		setIsTargetSelected (false);
	}
	
	public void setIsTargetSelected(bool itsIn){
		isTargetSelected = itsIn;
	}
	
	void attack(){
		//Debug.Log("atacando al enemigo!!!");
		animtr.SetBool ("Attacking",true);
		animtr.speed = (1.0f /getAttackCoolDown());		//Escala de velocidad de ataque
		if(Time.time - attackCDTimer > attackCoolDown) {  	// espera entre ataques 

			//calcula el daño
			attackedEnemyScript.curHP -= meleeDamage;
			//Reproduce un sonido de ataque asociado
			sfx.PlayRandomAttack();
			attackCDTimer = Time.time;
		}
	}
	
	private int calculateAttackDamage(){
		return (int)((2*meleeDamage+10)*Random.Range(0.1f,1f));
	}
	
	
	public void die(){
		Debug.Log ("jugador: me muero....");
		Application.LoadLevel("MainWorld");
	}
	public void win(){
		Debug.Log ("jugador: ganeeeeee...");
		animtr.SetBool("VictoryDance",true);
	}
	
	public enemyStatusGUI getAttackedEnemyScript(){
		return attackedEnemyScript;
	}

	public float getAttackCoolDown(){
		return attackCoolDown;
	}
	public void setAttacking(bool attack){
		isAttacking = attack;
	}
	public Vector3 getDestination(){
		return attackedEnemyScript.getTransform().position;
	}
}
