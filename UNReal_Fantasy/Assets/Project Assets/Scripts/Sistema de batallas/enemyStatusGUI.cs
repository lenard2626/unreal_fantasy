using UnityEngine;
using System.Collections;

public class enemyStatusGUI : MonoBehaviour {

	
	// texturas
	public Texture2D healthBackground; // back segment
	public Texture2D healthForeground; // front segment
	public Texture2D healthDamage; // draining segment
	public GUISkin HUDSkin; // Styles up the health integer

	//interfaz
	public int layoutWidth=300;
	public int layoutHeight=150;
	public int layoutBorderX=10;
	public int layoutBorderY=10;
	public string enemyName;
	public GameObject gizmo_npc;
	public bool showStatus=true;

	int healthBarPosX = 0;
	int healthBarPosY = 15;
	int healthBarHeight = 20;

	//posiciones relativas al jugador
	public GameObject player;
	public int yGUIDistance=120;
	public int selectableDistance=300;		//A que distancia puede el jugador seleccionar este enemigo?
	
	//values   
	private float previousHealth; //a value for reducing previous current health through attacks
	private float healthBar; //a value for creating the health bar size
	private float myFloat; // an empty float value to affect drainage speed
	public static float maxHP=1000; // maximum HP
	public static float curHP=maxHP; // current HP


	void Start () {
		curHP=maxHP;
		previousHealth = maxHP; // assign the empty value to store the value of max health
		healthBar = /*layoutWidth-2*layoutBorderX*/100f; // create the health bar width
		myFloat = (maxHP / 100) * 10; // affects the health drainage

		//Ocultamos el gizmo (solo se muestra en mouseenter)
		gizmo_npc.GetComponent<MeshRenderer>().enabled = false;
	}
	
	void Update(){
		adjustCurrentHealth();
	}
	
	public void adjustCurrentHealth(){
		
		/**Deduct the current health value from its damage**/  
		if(previousHealth > curHP){
			previousHealth -= ((maxHP / curHP) * (myFloat)) * Time.deltaTime; // deducts health damage
		} else {
			previousHealth = curHP;
		}
		
		if(previousHealth < 0){
			previousHealth = 0;
		}
		
		if(curHP > maxHP){
			curHP = maxHP;
			previousHealth = maxHP;
		}
		
		if(curHP < 0){
			curHP = 0;
		}
	}
	
	void OnGUI () {

		Vector2 targetPos;
		targetPos = player.GetComponentInChildren<Camera> ().WorldToScreenPoint (transform.position);

		if (showStatus &&
		    (targetPos.x>0 && targetPos.y>0 )) {								//Para evitar mostrar la interfaz cuando el enemigo esta detras de la camara
			float previousAdjustValue = (previousHealth * healthBar) / maxHP;
			float percentage = healthBar * (curHP / maxHP);



			//GUI.skin = HUDSkin;
			GUILayout.BeginArea (new Rect (targetPos.x, yGUIDistance, layoutWidth, layoutHeight), /*curHP + "/" + maxHP*/enemyName);

			GUI.DrawTexture (new Rect (healthBarPosX, healthBarPosY, (healthBar * 2), healthBarHeight), healthBackground);       
			GUI.DrawTexture (new Rect (healthBarPosX, healthBarPosY, (previousAdjustValue * 2), healthBarHeight), healthDamage);
			GUI.DrawTexture (new Rect (healthBarPosX, healthBarPosY, (percentage * 2), healthBarHeight), healthForeground);

			GUI.Label (new Rect (healthBarPosX,healthBarPosY, (healthBar * 2), healthBarHeight),(int)(previousHealth) + "/" + maxHP.ToString ());
			GUILayout.EndArea ();
		} else {
			gizmo_npc.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	void OnMouseDown(){
		ShowStatus ();
	}

	public void ShowStatus(){
		//deselecciona los demas enemigos marcados con el tag selectable (usado para seleccionables con interfaz emergente)
		GameObject[] others=GameObject.FindGameObjectsWithTag ("selectable");
		enemyStatusGUI cur_gui = null;
		foreach (GameObject cur_object in others) {
			if(cur_gui=cur_object.GetComponent<enemyStatusGUI>())
				cur_gui.showStatus=false;
		}
		showStatus = true;
	}
	public void hideStatus(){
		showStatus = false;
	}


	void OnMouseEnter(){
		gizmo_npc.GetComponent<MeshRenderer>().enabled = true;
	}
	void OnMouseOut(){
		gizmo_npc.GetComponent<MeshRenderer>().enabled = false;
	}
}
