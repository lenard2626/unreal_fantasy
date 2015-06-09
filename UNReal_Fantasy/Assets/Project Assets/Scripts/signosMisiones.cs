using UnityEngine;
using System.Collections;

public class signosMisiones : MonoBehaviour {
	registroJugador reg;
	GameObject personaje;
	MeshRenderer interrogante;
	MeshRenderer exclamacion;
	Mision misionActual;
	// Use this for initialization
	void Start () {
		reg = GameObject.Find("PersonajePrincipal").GetComponent<registroJugador>();
	}
	
	// Update is called once per frame
	void Update () {
		misionActual = reg.misionActual;
		if (misionActual.estado == Mision.SININICIAR) {
			personaje = GameObject.Find (misionActual.dueño);
			exclamacion = personaje.GetComponentsInChildren<MeshRenderer> () [1];
			exclamacion.enabled = true;
			exclamacion = personaje.GetComponentsInChildren<MeshRenderer> () [3];
			exclamacion.enabled = true;
		} else if (misionActual.estado == Mision.ENPROGRESO) {
			personaje = GameObject.Find (misionActual.dueño);
			exclamacion = personaje.GetComponentsInChildren<MeshRenderer> () [1];
			exclamacion.enabled = false;
			exclamacion = personaje.GetComponentsInChildren<MeshRenderer> () [3];
			exclamacion.enabled = false;
		} else if (misionActual.estado == Mision.FINALIZADA) {
			personaje = GameObject.Find (misionActual.finaliza);
			interrogante = personaje.GetComponentsInChildren<MeshRenderer> () [0];
			interrogante.enabled = true;
			interrogante = personaje.GetComponentsInChildren<MeshRenderer> () [2];
			interrogante.enabled = true;
		} else if (misionActual.estado == Mision.ENTREGADA) {
			personaje = GameObject.Find (misionActual.finaliza);
			interrogante = personaje.GetComponentsInChildren<MeshRenderer> () [0];
			interrogante.enabled = false;
			interrogante = personaje.GetComponentsInChildren<MeshRenderer> () [2];
			interrogante.enabled = false;
		} 


	}
}
