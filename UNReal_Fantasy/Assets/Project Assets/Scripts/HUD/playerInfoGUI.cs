using UnityEngine;
using System.Collections;


public class playerInfoGUI : MonoBehaviour {
	
	
	// texturas
	public Texture2D closeBtn;
	public Texture2D statusLayoutBg; // fondo del layout para estatus
	
	//interfaz

	private int layoutX = 0;
	private int layoutY = 0;
	public int layoutWidth=268;
	public int layoutHeight=164;

	private int columnWidth=0;
	public string playerName="test";
	
	public GUIStyle style;
	
	public int anchorX = 0;
	public int anchorY = 0;
	
	public bool showStatus=true;
	
	//valores de estado  IMPROTANTE: CAMBIAR DE PUBLICO A PRIVADO AL TERMINAR DEBUG
	public int level=0;
	public int strength=0;
	public int intelligence=0;
	public int agility=0;
	public int spirit=0;

	private sessionData sd;
	/*
	public int testx=0;
	public int testy=0;
*/
	//public Texture2D lineTexture;


	void Start () {
		layoutX=10;
		layoutY=GetComponent<playerStatusGUI>().layoutHeight+10;

		anchorX = (int)(layoutWidth * 0.1);
		anchorY = (int)(layoutHeight * 0.05);

		columnWidth = (int)(layoutWidth * 0.5f);

		level=0;
		strength=0;
		intelligence=0;
		agility=0;
		spirit=0;
	}
	
	void Update(){

	}
	
	void OnGUI () {
		
		if (showStatus) {

			//Area de HUD para estado del personaje
			GUILayout.BeginArea (new Rect(layoutX, layoutY, layoutWidth, layoutHeight),"");
			//Fondo del layout
			GUI.DrawTexture (new Rect (0, 0,layoutWidth, layoutHeight), statusLayoutBg);  
			//Boton de cerrado
			/*if(GUI.Button(new Rect(235,30, 15, 15),closeBtn)){
				hideStatus();
			}*/
			//Boton de cerrado
			if(GUI.Button(new Rect(layoutWidth-anchorX,25, 15, 15),closeBtn)){
				hideStatus();
				
			}

			//Etiquetas de estadisticas
			GUI.Label (new Rect (70,25,9,layoutHeight-anchorY),"Estadisticas",style);

			GUI.Label (new Rect (anchorX,60,columnWidth, 20),"Fuerza: ",style);
			GUI.Label (new Rect (3*columnWidth/2,60,columnWidth, 20),strength+"",style);

			GUI.Label (new Rect (anchorX,80,columnWidth, 20),"Inteligencia: ",style);
			GUI.Label (new Rect (3*columnWidth/2,80,columnWidth, 20),intelligence+"",style);

			GUI.Label (new Rect (anchorX,100,columnWidth, 20),"Agilidad: ",style);
			GUI.Label (new Rect (3*columnWidth/2,100,columnWidth, 20),agility+"",style);

			GUI.Label (new Rect (anchorX,120,columnWidth, 20),"Espiritu: ",style);
			GUI.Label (new Rect (3*columnWidth/2,120,columnWidth, 20),spirit+"",style);

			//Metodo alternativo: pentagono con triangulos

			Color c=new Color(0,255,0,1.0f);
			//DrawStatusPlygon(new Vector2(50,50),4,50);

			GUILayout.EndArea ();
		} 
	}
	//Oculta el estado del jugador
	public void hideStatus(){
		showStatus = false;
	}

}
