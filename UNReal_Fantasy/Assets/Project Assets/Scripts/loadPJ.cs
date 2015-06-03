﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loadPJ : MonoBehaviour {
	private sessionData session;
	private GameObject pj;
	// Use this for initialization
	void Start () {
		GameObject sessionInstance = GameObject.Find ("SessionData");
		if (!sessionData.inBattle.Equals (2)) {
			this.pj = GameObject.Find ("PersonajePrincipal");
			this.pj.transform.position = new Vector3 (sessionData.saveX, sessionData.saveY, sessionData.saveZ);
			this.pj.GetComponent<registroJugador> ().loadMissions ();
			this.pj.GetComponent<registroJugador> ().misionActual = this.pj.GetComponent<registroJugador> ().missions.Find (it => it.idMision == sessionData.saveLastMision);
			this.pj.GetComponent<registroJugador> ().misionActual.estado = sessionData.saveLastMisionState;
			this.pj.GetComponent<registroJugador> ().state = sessionData.saveState;
		} else {
			sessionData.loadPjAfterBattle();
		}
		if (sessionInstance != null) {
			this.session = sessionInstance.GetComponent<sessionData> ();
			GameObject.Find ("PersonajePrincipal/EthanBody").GetComponent<SkinnedMeshRenderer> ().material = session.classMaterials [sessionData.load_selectedPjClass];
			GameObject.Find ("PersonajePrincipal/characterName").GetComponent<TextMesh> ().text = sessionData.load_selectedPjName;
		}
			//this.pj.GetComponent<registroJugador> ().startMission (this.pj.GetComponent<registroJugador> ().misionActual);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
			sessionData.saveX = pj.GetComponent<Transform> ().position.x;
			sessionData.saveY = pj.GetComponent<Transform> ().position.y;
			sessionData.saveZ = pj.GetComponent<Transform> ().position.z;
			sessionData.saveLastMision = pj.GetComponent<registroJugador> ().misionActual.idMision;
		sessionData.saveState = pj.GetComponent<registroJugador> ().state;
			sessionData.saveLastMisionState = pj.GetComponent<registroJugador> ().misionActual.estado;
		if (session != null) {
			session.saveCharacter ();
		}
	}




}
