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

	public float sceneTransitionTimeout=10;
	
	private float attackCDTimer=0;
	private bool isTargetSelected=false;		//Dice si en determinado instante, el jugador tiene un objetivo señalado
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador

	float sceneTransitionCounter=0;

	public KeyCombo[] combos;
	public Skill[] skills=new Skill[]{new Skill("Punch1",1.1f),new Skill("Punch2",1.4f)};
	private int curSkillIndex=0;

	private CapsuleCollider ccollider;			//Necesario para evitar colisiones

	private Animator animtr;
	private SFX sfx;
	private ThirdPersonUserControl tpc;
	
	private enemyStatusGUI attackedEnemyScript=null;		//acceso al script de estado del enemigo
	private playerStatusGUI playerStatusScript=null;

	private screenFade screenfade;
	
	void Start () {
		combos = new KeyCombo[]{new KeyCombo (new string[] {"Punch1", "Punch1","Punch2"}, animtr)};
		animtr = GetComponent<Animator> ();
		sfx = GetComponentInChildren<SFX> ();
		ccollider = GetComponent<CapsuleCollider> ();
		tpc = GetComponent<ThirdPersonUserControl> ();	//Para usar las funciones de movimiento de ethan
		playerStatusScript = GetComponent<playerStatusGUI>();
		meleeAttackRange *= meleeAttackRange;			//Usamos distancia al cuadrado para ahorrarnos la raiz cuadrada
	}
	// Update is called once per frame
	void Update () {
		if (playerStatusScript.getCurrentHP() <= 0) {
			die ();
		}

		//Evalua si esta atacando un enemigo
		if (attackedEnemyScript != null && isTargetSelected) {			//Si ha seleccionado a un enemigo, este valor no debe ser nulo
			var distanceToEnemy = Vector3.SqrMagnitude (attackedEnemyScript.getTransform().position - transform.position);
			//Debug.Log("Distancia al enemigo "+distanceToEnemy);
			if ((meleeAttackRange+(ccollider.radius*ccollider.radius)>= distanceToEnemy)) {
				isAttacking=true;
				tpc.setFollow(Vector3.zero);
			}else{
				approachToTarget();
			}
			if (isAttacking) {
				//Evalua la skill a emplear
				evaluateSkill();
				evaluateCombo ();
				attack();
			}
		}
	}

	public void setAttackingEnemy(enemyStatusGUI aesIn){
		attackedEnemyScript=aesIn;						//Funcion que recibe el componente "enemyStatusScript", para que el personaje principal pueda atacar al enemigo
		isTargetSelected = true;
	}
	
	public void approachToTarget(){
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
		animtr.SetBool ("Attacking",isAttacking);

		animtr.speed = (1.0f /getAttackCoolDown());			//Escala de velocidad de ataque
		if(Time.time - attackCDTimer > attackCoolDown) {  	// espera entre ataques 
			useSkill (curSkillIndex);
			//Reproduce un sonido de ataque aleatorio
			sfx.PlayRandomAttack();
			attackCDTimer = Time.time;
		}
	}

	//Calcula el daño y anima la habilidad usada correspondiente
	private void useSkill(int skillsIndex){
		Debug.Log("usada skill "+skills[skillsIndex].SkillName);
		animtr.SetTrigger (skills[skillsIndex].AnimParamName);
		attackedEnemyScript.curHP -=calculateAttackDamage(skills[skillsIndex].DamageModifier);
	}

	private void evaluateSkill(){
		int i=0;
		foreach(Skill cur_skill in skills){
			Debug.Log("examinando skill "+cur_skill.SkillName);
			if(Input.GetButtonDown(cur_skill.SkillName)){
				curSkillIndex=i;
				return;
			}
			i++;
		}
	}

	private void evaluateCombo(){
		foreach(KeyCombo cur_combo in combos){
			if(cur_combo.Check()){
				Debug.Log("Combo aceptado");
			}
		}
	}
	
	private int calculateAttackDamage(float damageModifier){
		return (int)((damageModifier*meleeDamage)*Random.Range(0.8f,1.2f));
	}
	
	
	public void die(){
		Debug.Log ("jugador: me muero....");
		isAttacking = false;
		attackedEnemyScript = null;
		animtr.SetBool("Lose",true);
		animtr.SetBool("Attacking",isAttacking);
		sessionData.saveLastMisionStateBeforeBattle = Mision.SININICIAR;
		sessionData.inBattle = 2;
		StartCoroutine (afterBattleCoroutine ());
	}
	public void win(){
		Debug.Log ("jugador: ganeeeeee...");
		isAttacking = false;
		attackedEnemyScript = null;
		animtr.SetBool("VictoryDance",true);
		animtr.SetBool("Attacking",isAttacking);
		sessionData.saveLastMisionStateBeforeBattle = Mision.FINALIZADA;
		sessionData.inBattle = 2;
		StartCoroutine (afterBattleCoroutine ());
	}

	IEnumerator afterBattleCoroutine(){
		yield return new WaitForSeconds(sceneTransitionTimeout);
		Debug.Log ("sceneTransitionCounter "+sceneTransitionCounter);
		Application.LoadLevel ("MainWorld");
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
