using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
public class mostrardialogo : MonoBehaviour {
	private Canvas dialogo;
	private Text texto;
	private Texture2D imagen;//Image
	private Button aceptarMision;
	private Button cancelarMision;
	public Texture2D sprite;//Sprite
	registroJugador player;
	mostrardialogo scriptDialog;
	public Font fuente;
	bool isOpened=false;
	bool isOnTrigger=false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("PersonajePrincipal").GetComponent<registroJugador>();
		/*dialogo = GameObject.FindGameObjectWithTag ("Dialog").GetComponent<Canvas>();
		texto=GameObject.FindGameObjectWithTag ("Dialog").GetComponentInChildren<Text> ();
		imagen=GameObject.FindGameObjectWithTag ("DgImage").GetComponent<Image> ();
		dialogo.enabled=false;
		*/
	}

	void dialogFenster() {
		//layout start
		GUI.BeginGroup(new Rect(0, Screen.height*5/6, Screen.width, Screen.height/6));
		
		//the menu background box
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height/6), "");
		
		//logo picture
		GUI.Label(new Rect(0, 0, Screen.width/6, Screen.height/6), sprite);
		
		///////pause menu buttons
		//game resume button
		GUI.skin.textArea.normal.background = null;
		GUI.skin.textArea.active.background = null;
		GUI.skin.textArea.onHover.background = null;
		GUI.skin.textArea.hover.background = null;
		GUI.skin.textArea.onFocused.background = null;
		GUI.skin.textArea.focused.background = null;
		GUI.skin.textArea.normal.textColor = Color.white;
		GUI.skin.font = fuente;

		Rect rectText = new Rect (Screen.width / 6, Screen.width / 100, Screen.width / 6, Screen.height / 7);
		Rect rectBtn1 = new Rect (Screen.width * 4 / 6, Screen.width / 80, Screen.width / 7, Screen.height / 11);
		Rect rectBtn2 = new Rect (Screen.width * 5 / 6, Screen.width / 80, Screen.width / 7, Screen.height / 11);


		if (this.transform.parent.name == "Jimmy") {
			if(player.state==0){
				GUI.TextArea (rectText, "Haga la mision 1");
				if(GUI.Button(rectBtn1, "Aceptar")) {
					player.addMission("Mision 1");
					player.listMissions();
					GUI.TextArea (rectText, "Mision 1 Aceptada");
				}else if (GUI.Button(rectBtn2, "Cancelar")){
					GUI.TextArea (rectText, "Vuelve Pronto");
					isOpened=false;
				}
			}else{
				GUI.TextArea (rectText, "Hola, como estas, en que puedo ayudarte!");
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Jimmy");
		} else if(this.transform.parent.name == "Nigga"){
			if(player.state==1){
				GUI.TextArea (rectText, "Haga la mision 2");
				if(GUI.Button(rectBtn1, "Aceptar")) {
					player.addMission("Mision 2");
					player.listMissions();
					GUI.TextArea (rectText, "Mision 2 Aceptada");
				}else if (GUI.Button(rectBtn2, "Cancelar")){
					GUI.TextArea (rectText, "Vuelva Pronto, oyo?");
					isOpened=false;
				}
			}else{
				GUI.TextArea (rectText, "Que necesita el 'eñor!!");
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "ProfesorCuchilla"){
			if(player.state==2){
				GUI.TextArea (rectText, "Haga la mision 3");
				if(GUI.Button(rectBtn1, "Aceptar")) {
					player.addMission("Mision 3");
					player.listMissions();
					GUI.TextArea (rectText, "Mision 3 Aceptada");
				}else if (GUI.Button(rectBtn2, "Cancelar")){
					GUI.TextArea (rectText, "Apurele!!");
					isOpened=false;
				}
			}else{
				GUI.TextArea (rectText, "Estudie Vago!!!");
			}			
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "Celador"){
			if(player.state==3){
				GUI.TextArea (rectText, "Haga la mision 4");
				if(GUI.Button(rectBtn1, "Aceptar")) {
					player.addMission("Mision 4");
					player.listMissions();
					GUI.TextArea (rectText, "Mision 4 Aceptada");
				}else if (GUI.Button(rectBtn2, "Cancelar")){
					GUI.TextArea (rectText, "Lo espero dotor");
					isOpened=false;
				}
			}else{
				GUI.TextArea (rectText, "El carnet por favor");
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "Capucho"){
			if(player.state==4){
				GUI.TextArea (rectText, "Haga la mision 5");
				if(GUI.Button(rectBtn1, "Aceptar")) {
					player.addMission("Mision 5");
					player.listMissions();
					GUI.TextArea (rectText, "Mision 5 Aceptada");
				}else if (GUI.Button(rectBtn2, "Cancelar")){
					GUI.TextArea (rectText, "Lo espero compita");
					isOpened=false;
				}
			}else{
				GUI.TextArea (rectText, "Camilo VIVE!!!");
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		}
		//layout end
		GUI.EndGroup(); 
	}
	void OnGUI(){
		if (isOpened) {
			dialogFenster ();
		}
	}
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(){
		isOpened = true;
		isOnTrigger = true;
	}

	void OnTriggerExit(){
		isOpened = false;
		isOnTrigger = false;
	}

	void OnMouseUp(){
		if (isOnTrigger) {
			isOpened = true;
		}
	}
}
