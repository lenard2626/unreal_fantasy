using UnityEngine;
using System.Collections;

public class pasos : MonoBehaviour {
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W))
		{
			GetComponent<AudioSource>().Play ();
			
		}
		if (Input.GetKeyUp (KeyCode.W))
		{
			
			GetComponent<AudioSource>().Stop ();
			
		}
	}
}
