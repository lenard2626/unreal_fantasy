using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class registroJugador : MonoBehaviour {
	public int state=0;
	public List<Mision> missions = new List<Mision>();
	public Sprite interrogante;
	public bool hasMission=false;
	public Mision misionActual;
	void start(){

	}

	public void loadMissions(){
		if (!hasMission) {
			this.addMission (new Mision (0, "Bienvenido a la Universidad", "Busca al vendedor de la plaza central, el te dara un consejo muy valioso.", "Jimmy", "Nigga"));
			this.addMission (new Mision(1, "Estudiar no es facil", "Busca al profesor Cuchilla, el seguramente tiene ideas de como enseñarte cosas nuevas. El se encuentra en la perola", "Nigga", "ProfesorCuchilla"));
			this.addMission (new Mision(2, "No es facil lidiar con profesores cuchilla...", "Cree que puede conmigo señor?? le vpoy a poner CEROOO!!!", "ProfesorCuchilla", "ProfesorCuchilla", "battle_scene_1"));
			this.addMission (new Mision(3, "La biblioteca, tu mejor aliada", "Cree que puede conmigo señor?? le vpoy a poner CEROOO!!!", "ProfesorCuchilla", "TriggerBiblioteca"));
			this.addMission (new Mision(4, "Edicifios cerrados y otras viscicitudes", "No lo puedo dejar entrar a la biblioteca, primero tendra que derrotarme...", "Celador", "TriggerAviso"));
			this.addMission (new Mision(5, "El encuentro fatal", "Camina hacia la playita para llegar a la biblioteca del C&T", "TriggerAviso", "Capucho"));

			this.misionActual=this.getMision(0);
			hasMission=true;
		}
	}
	public Mision getMision(int id){
		Mision mision= null;
		foreach (Mision mission in missions) {
			if(mission.idMision==id)mision=mission;
		}
		return mision;
	}
	public void addMission(Mision mision){
		missions.Add (mision);
		mision.estado = Mision.SININICIAR;
	}
	public void startMission(Mision mision){
		Image imagenMisionAceptada=GameObject.Find ("ImagenMisionAceptada").GetComponent<Image> ();
		Image imagenMisionCompletada=GameObject.Find ("ImagenMisionCompletada").GetComponent<Image> ();
		AudioSource soundMisiones=GameObject.Find ("MisionesAceptadas").GetComponent<AudioSource> ();
		soundMisiones.Play ();
		imagenMisionAceptada.enabled = true;
		imagenMisionAceptada.CrossFadeAlpha (0, 6f, false);
		imagenMisionCompletada.CrossFadeAlpha (1, 0, false);
		imagenMisionCompletada.enabled = false;
		if (misionActual.escena != null) {

			GameObject pj = GameObject.Find("PersonajePrincipal");
			sessionData.lastXBeforeBattle =pj.GetComponent<Transform> ().position.x;
			sessionData.lastYBeforeBattle =pj.GetComponent<Transform> ().position.y;
			sessionData.lastZBeforeBattle =pj.GetComponent<Transform> ().position.z;
			sessionData.saveLastMisionBeforeBattle = pj.GetComponent<registroJugador> ().misionActual.idMision;
			sessionData.inBattle = 1;
			Application.LoadLevel(misionActual.escena);
		}
		mision.estado = Mision.ENPROGRESO;
	}
	public void cancelMission(Mision mision){
		mision.estado = Mision.SININICIAR;
		Image imagenMisionAceptada=GameObject.Find ("ImagenMisionAceptada").GetComponent<Image> ();
		AudioSource soundMisiones=GameObject.Find ("MisionesAceptadas").GetComponent<AudioSource> ();
		soundMisiones.Play ();
		imagenMisionAceptada.CrossFadeAlpha (1, 0, false);
		imagenMisionAceptada.enabled = false;

	}
	public void completeMission(Mision mision){
		AudioSource soundMisiones=GameObject.Find ("MisionesCompletadas").GetComponent<AudioSource> ();
		Image imagenMisionCompletada=GameObject.Find ("ImagenMisionCompletada").GetComponent<Image> ();
		imagenMisionCompletada.enabled = true;
		imagenMisionCompletada.CrossFadeAlpha (0, 6f, false);
		soundMisiones.Play ();
		mision.estado = Mision.FINALIZADA;
		Animator animador=this.GetComponent<Animator> ();
		animador.SetBool ("MissionComplete", true);

	}
	public void finishMission(Mision mision){
		mision.estado = Mision.ENTREGADA;
		AudioSource soundMisiones=GameObject.Find ("MisionesCompletadas").GetComponent<AudioSource> ();
		Image imagenMisionAceptada=GameObject.Find ("ImagenMisionAceptada").GetComponent<Image> ();
		soundMisiones.Play ();
		Canvas canvasMisiones=GameObject.Find ("MenuMisiones").GetComponent<Canvas> ();
		imagenMisionAceptada.CrossFadeAlpha (1, 0, false);
		imagenMisionAceptada.enabled = false;
		canvasMisiones.enabled=false;
		Animator animador=this.GetComponent<Animator> ();
		animador.SetBool ("MissionComplete", false);
	}
	public void showMission(Mision mission){
		Text menuMisiones=GameObject.Find ("TextoMision").GetComponent<Text> ();
		Text detallesMisiones=GameObject.Find ("TextoDetalles").GetComponent<Text> ();
		Canvas canvasMisiones=GameObject.Find ("MenuMisiones").GetComponent<Canvas> ();
		menuMisiones.text=mission.nombre;
		detallesMisiones.text=mission.detalles;
		canvasMisiones.enabled=true;
	}
	public void hideMissions(){
		Canvas canvasMisiones=GameObject.Find ("MenuMisiones").GetComponent<Canvas> ();
		canvasMisiones.enabled=false;
	}
	void Update(){
	}
}
