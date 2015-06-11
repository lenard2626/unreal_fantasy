using UnityEngine;
using System.Collections;

public class golpe: MonoBehaviour {
		
	// Update is called once per frame
	void Update () {

	}

	public void play(){
		GetComponent<AudioSource>().Play ();
	}
	

}
