using UnityEngine;
using System.Collections;

public class TriggerSaludo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter() { 
		GetComponent<AudioSource>().Play(); 
	} 

	void OnTriggerExit() {
		GetComponent<AudioSource>().Stop(); 
	}
}
