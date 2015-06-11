using UnityEngine;
using System.Collections;

public class golpe: MonoBehaviour {
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Y)||Input.GetKeyDown (KeyCode.U)||Input.GetKeyDown (KeyCode.I)||Input.GetKeyDown (KeyCode.O)||Input.GetKeyDown (KeyCode.P))
		{
			GetComponent<AudioSource>().Play ();
			
		}
		if (Input.GetKeyDown (KeyCode.Y)||Input.GetKeyDown (KeyCode.U)||Input.GetKeyDown (KeyCode.I)||Input.GetKeyDown (KeyCode.O)||Input.GetKeyDown (KeyCode.P))
		{
			
			GetComponent<AudioSource>().Stop ();
			
		}
	}
}
