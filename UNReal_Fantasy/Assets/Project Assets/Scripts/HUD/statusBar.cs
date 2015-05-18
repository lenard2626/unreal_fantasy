using UnityEngine;
using System.Collections;

public class statusBar : MonoBehaviour {

	
	// texturas
	public Texture2D healthBackground; // back segment
	public Texture2D healthForeground; // front segment
	public Texture2D healthDamage; // draining segment
	public GUISkin HUDSkin; // Styles up the health integer
	
	//interfaz
	public int layoutWidth=500;
	public int layoutHeight=100;
	public int layoutBorderX=10;
	public int layoutBorderY=10;
	public string playerName;
	
	int anchorX = 0;
	int anchorY = 15;
	int healthBarHeight = 20;

	public int test=0;
	public int test2=0;

	
	bool showStatus=true;
	
	//values   
	private float previousHealth; //a value for reducing previous current health through attacks
	private float healthBar; //a value for creating the health bar size
	private float myFloat; // an empty float value to affect drainage speed
	public static float maxHP=5000; // maximum HP
	public float curHP=0; // current HP
	
	
	void Start () {
		curHP=maxHP;
		previousHealth = maxHP; // assign the empty value to store the value of max health
		healthBar = /*layoutWidth-2*layoutBorderX*/100f; // create the health bar width
		myFloat = (maxHP / 100) * 10; // affects the health drainage

	}
	
	void Update(){
		adjustCurrentHealth();
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
		
		if (showStatus) {
			float previousAdjustValue = (previousHealth * healthBar) / maxHP;
			float percentage = healthBar * (curHP / maxHP);
	
			//GUI.skin = HUDSkin;
			//Area de HUD para estado del personaje
			GUILayout.BeginArea (new Rect((Screen.width-layoutWidth)/2 , anchorY, layoutWidth, layoutHeight), /*curHP + "/" + maxHP*/"Informacion del personaje");
			//Nombre del personaje
			GUI.Label (new Rect (test2,test, (healthBar * 2), healthBarHeight),playerName);
			//Barra de vida
			GUI.DrawTexture (new Rect ( anchorX, 3*anchorY, (healthBar * 2), healthBarHeight), healthBackground);       
			GUI.DrawTexture (new Rect (anchorX, 3*anchorY, (previousAdjustValue * 2), healthBarHeight), healthDamage);
			GUI.DrawTexture (new Rect (anchorX, 3*anchorY, (percentage * 2), healthBarHeight), healthForeground);
			//Cantidad de salud
			GUI.Label (new Rect (anchorX,3*anchorY, (healthBar * 2), healthBarHeight),(int)(previousHealth) + "/" + maxHP.ToString ());

			GUILayout.EndArea ();
		} 
	}

}
