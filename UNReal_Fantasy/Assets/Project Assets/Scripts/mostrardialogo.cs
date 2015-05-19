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
	public registroJugador player;
	public mostrardialogo scriptDialog;
	public Font fuente;
	bool isOpened=false;
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

		if (this.transform.parent.name == "Jimmy") {
			if(player.state==0){
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Haga la mision 1");
				if(GUI.Button(new Rect(Screen.width*4/6, Screen.width/80, Screen.width/6, Screen.height/10), "Aceptar")) {
					player.addMission("Mision 1");
					player.listMissions();
					GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Mision 1 Aceptada");
				}else if (GUI.Button(new Rect(Screen.width*5/6, Screen.width/80, Screen.width/6, Screen.height/10), "Cancelar")){
					GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Vuelve Pronto");
				}
			}else{
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width-Screen.width/6, Screen.height/7), "Hola, como estas, en que puedo ayudarte!");
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Jimmy");
		} else if(this.transform.parent.name == "Nigga"){
			if(player.state==1){
				player.completeMission("Mision 1");
				player.addMission("Mision 2");
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Haga la mision 2");
				player.listMissions();
			}else{
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Que anda buscando el 'eñor");
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "ProfesorCuchilla"){
			if(player.state==2){
				player.completeMission("Mision 2");
				player.addMission("Mision 3");
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Haga la mision 3");
				player.listMissions();
			}else{
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Vaya a estudiar vago!!");
			}
			
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "Celador"){
			if(player.state==3){
				player.completeMission("Mision 3");
				player.addMission("Mision 4");
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Haga la mision 4");
				player.listMissions();
			}else{
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "El carnet por favor!");
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "Capucho"){
			if(player.state==4){
				player.completeMission("Mision 4");
				player.addMission("Mision 5");
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "Haga la mision 5");
				player.listMissions();
			}else{
				GUI.TextArea (new Rect(Screen.width/6, Screen.width/100, Screen.width/6, Screen.height/7), "CAMILO VIVE!!!");
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
		/*dialogo.enabled=true;
		if (this.transform.parent.name == "Jimmy") {
			imagen.sprite = this.sprite;
			if(player.state==0){
				player.addMission("Mision 1");
				texto.text = "Haga la mision 1";
				player.listMissions();
			}else{
				texto.text = "Hola, como estas, en que puedo ayudarte";
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Jimmy");
		} else if(this.transform.parent.name == "Nigga"){
			imagen.sprite = this.sprite;
			if(player.state==1){
				player.completeMission("Mision 1");
				player.addMission("Mision 2");
				texto.text = "Haga la mision 2";
				player.listMissions();
			}else{
				texto.text = "Que anda buscando el 'eñor";
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "ProfesorCuchilla"){
			imagen.sprite = this.sprite;
			if(player.state==2){
				player.completeMission("Mision 2");
				player.addMission("Mision 3");
				texto.text = "Haga la mision 3";
				player.listMissions();
			}else{
				texto.text = "Vaya a estudiar vago!!";
			}
			
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "Celador"){
			imagen.sprite = this.sprite;
			if(player.state==3){
				player.completeMission("Mision 3");
				player.addMission("Mision 4");
				texto.text = "Haga la mision 4";
				player.listMissions();
			}else{
				texto.text = "El carnet por favor!";
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		} else if(this.transform.parent.name == "Capucho"){
			imagen.sprite = this.sprite;
			if(player.state==4){
				player.completeMission("Mision 4");
				player.addMission("Mision 5");
				texto.text = "Haga la mision 5";
				player.listMissions();
			}else{
				texto.text = "CAMILO VIVE!!!";
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		}*/

	}

	void OnTriggerExit(){
		isOpened = false;
		//dialogo.enabled=false;
	}
}
