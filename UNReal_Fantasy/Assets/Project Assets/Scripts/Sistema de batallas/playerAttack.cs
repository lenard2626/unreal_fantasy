using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	/*Variables publicas para configurar el ataque*/
	public float meleeDamage=70;
	public float attackCoolDown=3;
	public float meleeAttackRange=5;				//El rango de ataque del enemigo cuerp a cuerpo 			

	private float attackCDTimer=0;
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador
	private Animation anim;

	private enemyStatusGUI attackedEnemyScript=null;		//acceso al script de estado del enemigo
	
	void Start () {
		anim = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Evalua si esta atacando un enemigo
		if (attackedEnemyScript != null) {			//Si ha seleccionado a un enemigo, este valor no debe ser nulo
			if (meleeAttackRange > Vector3.SqrMagnitude (attackedEnemyScript.getTransform().position - transform.position)) {
				isAttacking=true;
			}else{
				approachToTarget();
			}
			if (isAttacking) {
				attack();
			}
		}
	}

	public void setAttackedEnemy(enemyStatusGUI aesIn){
		attackedEnemyScript=aesIn;						//Funcion que recibe el componente "enemyStatusScript", para que el personaje principal pueda atacar al enemigo
		if (aesIn != null) {
			isAttacking=false;
		}
	}

	public void approachToTarget(){
		//Acerca automaticamente el jugador al enemigo
		if(!anim.IsPlaying ("run"))anim.Play("run");
		transform.position = Vector3.MoveTowards (transform.position,
		                                          new Vector3(attackedEnemyScript.getTransform().position.x,transform.position.y,attackedEnemyScript.getTransform().position.z),
		                                          10 * Time.deltaTime);
	}

	void attack(){
		if(Time.time - attackCDTimer > attackCoolDown) {  // espera entre ataques 
			//ataca
			attackedEnemyScript.curHP -= meleeDamage;
			if(!anim.IsPlaying ("attack"))anim.Play("attack");
			attackCDTimer = Time.time;
		}
	}

	
	public void die(){
		Debug.Log ("jugador: me muero....");
		if(!anim.IsPlaying ("die"))anim.Play("die");
	}
}
