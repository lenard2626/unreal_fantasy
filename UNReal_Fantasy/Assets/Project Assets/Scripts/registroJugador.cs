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
			this.addMission (new Mision (0, "Bienvenido a la Universidad", "Hola! Bienvenido! Tu debes ser el nuevo primiparo. Tu amigo el VENDEDOR DE LA PLAZA CENTRAL te estaba buscando y al parecer tiene algo importante que decirte, mejor buscalo lo mas pronto posible", "Jimmy", "Nigga"));
			this.addMission (new Mision(1, "La Justicia debe Prevalecer", "Me contaron que el PROFESOR CUCHILLA planea hacerte perder la materia solo por el placer de verte sufrir!. Debes hacerle frente y no dejar que ocurra tal injusticia. Si le das una lección tal vez aprenda a no meterse con los estudiante. Buscalo por la PEROLA, hace poco lo vi rondando el lugar", "Nigga", "ProfesorCuchilla","battle_scene_1"));
			//this.addMission (new Mision(2, "No es facil lidiar con profesores cuchilla...", "Cree que puede conmigo señor?? le vpoy a poner CEROOO!!!", "ProfesorCuchilla", "ProfesorCuchilla", "battle_scene_1"));
			this.addMission (new Mision(2, "Una oportunidad valiosa", "Noooo ... No es posible.... . Esta bien, solo por haber demostrado su punto y su valia le dare una oportunidad. Vaya a la BIBLIOTECA mas cercana envieme el trabajo hoy mismo y prometo ser justo al calificarlo", "ProfesorCuchilla", "TriggerBiblioteca"));
			this.addMission (new Mision(3, "El cela", "Buenas Tardes, Carnet Por Favor...... oh no tiene carnet?, pues que lastima, no me importa si es su ultima oportunidad para graduarse, de aqui no podrá pasar jajaja y no hay nada que pueda hacer", "Celador", "TriggerAviso"));
			this.addMission (new Mision(4, "La ultima esperanza", "Aun queda una biblioteca abierta antes de que se complete el desalojo, la BILIOTECA DEL CYT. Puedes llegar a ella a traves de la PLAYITA", "TriggerAviso", "Capucho","battle_scene_2"));

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
		mision.estado = Mision.ENPROGRESO;

		if (mision.dueño == "Celador") {
			GameObject pj = GameObject.Find("PersonajePrincipal");
			sessionData.lastXBeforeBattle =pj.GetComponent<Transform> ().position.x;
			sessionData.lastYBeforeBattle =pj.GetComponent<Transform> ().position.y;
			sessionData.lastZBeforeBattle =pj.GetComponent<Transform> ().position.z;
			sessionData.saveLastMisionBeforeBattle = pj.GetComponent<registroJugador> ().misionActual.idMision;
			sessionData.saveStateBeforeBattle = pj.GetComponent<registroJugador> ().state;
			sessionData.inBattle = 1;
			Application.LoadLevel("battle_scene_2");
		}
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
