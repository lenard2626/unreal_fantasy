using UnityEngine;
using System.Collections;

public class TriggerSaludo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter() { 
		GetComponent<AudioSource>().Play(); 
		Animator animador=this.GetComponentInParent<Animator> ();
		animador.SetBool ("enterOnTrigger", true);
	} 

	void OnTriggerExit() {
		GetComponent<AudioSource>().Stop(); 
		Animator animador=this.GetComponentInParent<Animator> ();
		animador.SetBool ("enterOnTrigger", false);
	}
}
