using UnityEngine;
using System.Collections;

public class enemyStatusGUI : MonoBehaviour {

	
	// texturas
	public Texture2D icon;
	public Texture2D statusLayoutBg; // fondo del layout para estatus
	//public Texture2D closeBtn;

	public Texture2D healthBackground; 	// fondo blanco (no el quee stan pensando)

	public Texture2D healthForeground; // barra roja
	public Texture2D healthDamage; // barra amarilla de daño
	
	//public Texture2D staminaForeground; // barra roja
	//public Texture2D staminaDamage; // barra amarilla de daño

	//public GUISkin HUDSkin; // skin alternativo (no esta en uso)
	public GUIStyle style;

	//interfaz
	public int layoutWidth=282;
	public int layoutHeight=77;

	public string enemyName;
	public GameObject gizmo_npc;
	public bool showStatus=true;

	//Desde donde empiean los elementos 
	private int anchorX = 0;			
	private int anchorY = 0;
	int barH = 20;

	//posiciones relativas al jugador
	public GameObject player;
	public int yGUIDistance=120;
	public int selectableDistance=300;		

	//referencia de posicion a pasar al jugador atacante (para que el jugador sepa a donde dirigirse)
	private Transform thisTransform;
	
	//valores para la barra de vida   
	private float previousHealth; 
	private float healthBar; 
	private float myFloat; 

	public static float maxHP=1000; 
	public float curHP=maxHP; 

	//Variables para manejo de doble clic
	private float lastClickTime=0f;
	private float catchTime=0.4f;

	void Start () {
		curHP=maxHP;
		previousHealth = maxHP; 
		healthBar = layoutWidth*0.8f; 
		anchorX = (int)(layoutWidth * 0.1f);
		anchorY = (int)(layoutWidth * 0.05);
		myFloat = (maxHP / 100) * 10; 

		//Ocultamos el gizmo (solo se muestra en mouseenter)
		gizmo_npc.GetComponent<MeshRenderer>().enabled = false;

		thisTransform=GetComponent<Transform> ();
	}
	
	void Update(){
		adjustCurrentHealth();

		//Deteccion de doble click para ataque de pesonaje
		if(Input.GetButtonDown("Fire1")){
			if(Time.time-lastClickTime<catchTime){
				player.GetComponent<playerAttack>().setAttackingEnemy(this);
			}else{

			}
			lastClickTime=Time.time;
		}
	}
	
	public void adjustCurrentHealth(){

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

		//Vector2 targetPos=new Vector2(test1,test2);
		//Vector2 targetPos = player.GetComponentInChildren<Camera> ().WorldToScreenPoint (transform.position);			//para ubicar la interfaz sobre el objeto seleccionado

		if (showStatus) {								
			float previousAdjustValue = (previousHealth * healthBar) / maxHP;
			float percentage = healthBar * (curHP / maxHP);

			//GUI.skin = HUDSkin;
			GUILayout.BeginArea (new Rect((Screen.width-layoutWidth)/2 , 10, layoutWidth, layoutHeight),"");		//Interfaz al centro de la pantalla
			//Fondo del layout
			GUI.DrawTexture (new Rect (0, 0,layoutWidth, layoutHeight), statusLayoutBg);  

			//Nombre
			GUI.Label (new Rect (16,16,245,12),enemyName,style);

			//Icono de estado
			//GUI.DrawTexture (new Rect (anchorX, anchorY, healthBar , barH), icon);  

			//Barra de salud
			GUI.DrawTexture (new Rect (anchorX, 3*anchorY, healthBar, 20), healthBackground);       
			GUI.DrawTexture (new Rect (anchorX, 3*anchorY, previousAdjustValue, 20), healthDamage);
			GUI.DrawTexture (new Rect (anchorX, 3*anchorY, percentage, 20), healthForeground);
			//Cantidad de salud
			GUI.Label (new Rect (anchorX, 3*anchorY, healthBar, 20),(int)(previousHealth) + "/" + maxHP.ToString (),style);

			//Boton de cerrado
			/*if(GUI.Button(new Rect(layoutWidth-anchorX, anchorY, 15, 15),closeBtn)){
				hideStatus();
			}*/
			GUILayout.EndArea (); 
		} else {
			gizmo_npc.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	void OnMouseDown(){
		ShowStatus ();
		player.GetComponent<playerAttack>().setIsTargetSelected(true);
	}

	//Muestra el estado, se autoselecciona para el jugador como enemigo activo y oculta las demas interfaces
	public void ShowStatus(){
		//deselecciona los demas enemigos marcados con el tag selectable (usado para seleccionables con interfaz emergente)
		if (!showStatus) {
			GameObject[] others=GameObject.FindGameObjectsWithTag ("selectable");
			enemyStatusGUI cur_gui = null;
			foreach (GameObject cur_object in others) {
				if(cur_gui=cur_object.GetComponent<enemyStatusGUI>())
					cur_gui.hideStatus();
			}
		}
		showStatus = true;
	}
	//Oculta el estado y se deselecciona como enemigo activo
	public void hideStatus(){
		showStatus = false;
		player.GetComponent<playerAttack> ().setAttackingEnemy (null);
	}


	void OnMouseEnter(){
		gizmo_npc.GetComponent<MeshRenderer>().enabled = true;
	}
	void OnMouseOut(){
		gizmo_npc.GetComponent<MeshRenderer>().enabled = false;
	}


	//setters y getters (evitan el exceso de variables publicas)
	public Transform getTransform(){
		return thisTransform;
	}

}
