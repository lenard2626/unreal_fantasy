﻿using UnityEngine;
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
		player.loadMissions ();
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

		Rect rectText = new Rect (Screen.width / 6, Screen.width / 100, Screen.width/2, Screen.height / 7);
		Rect rectBtn1 = new Rect (Screen.width * 4 / 6, Screen.width / 80, Screen.width / 7, Screen.height / 11);
		Rect rectBtn2 = new Rect (Screen.width * 5 / 6, Screen.width / 80, Screen.width / 7, Screen.height / 11);
		if (this.transform.parent.name == player.misionActual.dueño) {
			if(player.misionActual.idMision==2){
				mostrardialogo biblioTrigger = GameObject.Find ("TriggerBiblioteca").GetComponentInChildren<mostrardialogo>();
				biblioTrigger.enabled=true;
			}
			if(player.misionActual.idMision==3){
				mostrardialogo avisoTrigger = GameObject.Find ("TriggerAviso").GetComponentInChildren<mostrardialogo>();
				avisoTrigger.enabled=true;
			}
			if(player.misionActual.estado==Mision.SININICIAR){
				GUI.TextArea (rectText, player.misionActual.detalles);
				if(GUI.Button(rectBtn1, "Aceptar")) {
					isOpened=false;
					GUI.TextArea (rectText, player.misionActual.nombre + " aceptada!!!");
					player.startMission(player.misionActual);
					player.showMission(player.misionActual);
				}else if (GUI.Button(rectBtn2, "Cancelar")){
					GUI.TextArea (rectText, "Vuelve Pronto!!");
					isOpened=false;
				}
			}else if(player.misionActual.estado==Mision.FINALIZADA){
				GUI.TextArea (rectText, "Hola, como estas, en que puedo ayudarte!");
					isOpened=false;
			}else if(player.misionActual.estado==Mision.ENPROGRESO){

				if(this.transform.parent.name == "Jimmy"){
					GUI.TextArea (rectText, "Ve y busca al VENDEDOR DE LA PLAZA CENTRAL  lo mas pronto posible, te necesita con urgencia");
				}

				if(this.transform.parent.name == "Nigga"){
					GUI.TextArea (rectText, "Vamos! Enseñale una lección a ese sujeto");
				}

				if(this.transform.parent.name == "ProfesorCuchilla"){
					GUI.TextArea (rectText, "Vaya y estudie antes de que cambie de opinion");
				}

				if(this.transform.parent.name == "Celador"){
					GUI.TextArea (rectText, "Como carajos???. Me has derrotado!.... Emmm aun asi no puedo dejarte entrar, lee el cartel y lo entenderas");
				}

				if(this.transform.parent.name == "TriggerAviso"){
					GUI.TextArea (rectText, "Llega lo mas rapido que puedas a la BIBLIOTECA DEL CYT para enviar el trabajo antes de que la cierren por el desalojo");
				}
				if( this.transform.parent.name != "Celador"){
				if (GUI.Button(rectBtn2, "Cancelar")){

					GUI.TextArea (rectText, "Vuelve Pronto!!");
					player.cancelMission(player.misionActual);
					player.hideMissions();
					isOpened=false;

					}
				}
			}
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Jimmy");
		} else if(this.transform.parent.name == player.misionActual.finaliza && player.misionActual.estado == Mision.ENPROGRESO){
			if(this.transform.parent.name=="Nigga"&& player.misionActual.idMision==0){
				GUI.TextArea (rectText, "DONDE ESTABAS! Te andaba buscando!. Tengo que decirte algo muy importante");
			} else if(this.transform.parent.name=="ProfesorCuchilla"&& player.misionActual.idMision==1){
				GUI.TextArea (rectText, "Jajajaja, con que te has enterado de mis planes. No importa, igual no hay forma de que un mocoso vago como tu pueda vencerme. Preparate.");
			} else if(this.transform.parent.name=="TriggerBiblioteca"&& player.misionActual.idMision==2){
				GUI.TextArea (rectText, "Encontraste la BIBLIOTECA Central!. Vamos, no pierdas tiempo, busca la forma de entrar!");
			} else if(this.transform.parent.name=="TriggerAviso"&& player.misionActual.idMision==3){
				GUI.TextArea (rectText, "El aviso dice: AVISO DE DESALOJO: El Rector Cara de Papa dio orden de desalojo por miedo a que los capuchines " +
					"lo hagan pure. Por esto no se puede ingresar a la biblioteca. Original Firmado por el Rector Cara de Papa...");
			} else if(this.transform.parent.name=="Capucho"&& player.misionActual.idMision==4){
				GUI.TextArea (rectText, "Hey tu! Me has dicho Mamerto???.... No? .... Pero si te acabo de escuchar, no sabes con quien te has metido!. Camilo Vive! Soy Estudiante!");
			}
			if(GUI.Button(rectBtn2, "Continuar")) {
				if (player.misionActual.escena != null) {
					GameObject pj = GameObject.Find("PersonajePrincipal");
					sessionData.lastXBeforeBattle =pj.GetComponent<Transform> ().position.x;
					sessionData.lastYBeforeBattle =pj.GetComponent<Transform> ().position.y;
					sessionData.lastZBeforeBattle =pj.GetComponent<Transform> ().position.z;
					sessionData.saveLastMisionBeforeBattle = pj.GetComponent<registroJugador> ().misionActual.idMision;
					sessionData.saveStateBeforeBattle = pj.GetComponent<registroJugador> ().state;
					sessionData.inBattle = 1;
					Application.LoadLevel(player.misionActual.escena);
				}else{


				player.completeMission(player.misionActual);
				//player.hideMissions();
				GUI.TextArea (rectText, player.misionActual.nombre + " finalizada!!!");
					//isOpened=false;
				}
			}
		} else if(this.transform.parent.name == player.misionActual.finaliza && player.misionActual.estado == Mision.FINALIZADA){
			GUI.TextArea (rectText, "Deseas finalizar la mision: "+ player.misionActual.nombre + "?");
			if(GUI.Button(rectBtn1, "Aceptar")) {
				player.finishMission(player.misionActual);
				player.hideMissions();
				GUI.TextArea (rectText, player.misionActual.nombre + " finalizada!!!");
				player.state++;
				//if(player.getMision(player.state)!=null){
					
				//}
			}else if (GUI.Button(rectBtn2, "Cancelar")){
				GUI.TextArea (rectText, "Vuelve Pronto!!");
				isOpened=false;
			}
		} else if(this.transform.parent.name == player.misionActual.finaliza && player.misionActual.estado == Mision.ENTREGADA){
			if(player.misionActual.idMision==2){
				mostrardialogo biblioTrigger = GameObject.Find ("TriggerBiblioteca").GetComponentInChildren<mostrardialogo>();
				biblioTrigger.enabled=false;
				isOpened=false;
			}
			player.misionActual= player.getMision(player.state);
			isOpened=false;
		}else if(this.transform.parent.name != player.misionActual.finaliza && (this.transform.parent.name != player.misionActual.dueño || player.misionActual.estado==Mision.ENTREGADA)){
			if(this.transform.parent.name=="Jimmy"){
				GUI.TextArea (rectText, "Hola, como estas, en que puedo ayudarte? ");
			} else if(this.transform.parent.name=="Nigga"){
				GUI.TextArea (rectText, "Que necesita el 'eñor?");
			} else if(this.transform.parent.name=="ProfesorCuchilla"){
				GUI.TextArea (rectText, "Vaya a estudiar vago!!");
			} else if(this.transform.parent.name=="Celador"){
				GUI.TextArea (rectText, "Carnet por favor!");
			} else if(this.transform.parent.name=="Capucho"){
				GUI.TextArea (rectText, "Camilo VIVE!!!");
			} else if(this.transform.parent.name=="TriggerAviso"){
				GUI.TextArea (rectText, "El aviso dice: AVISO DE DESALOJO: El Rector Cara de Papa dio orden de desalojo por miedo a que los capuchines " +
				              "lo hagan pure. Por esto no se puede ingresar a la biblioteca. Original Firmado por el Rector Cara de Papa...");
			}
		}



		if(this.transform.parent.name=="Celador"&& player.misionActual.idMision==3 && player.misionActual.estado == Mision.ENPROGRESO){
			GUI.TextArea (rectText, "Como carajos???. Me has derrotado!.... Emmm aun asi no puedo dejarte entrar, lee el cartel y lo entenderas");
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
