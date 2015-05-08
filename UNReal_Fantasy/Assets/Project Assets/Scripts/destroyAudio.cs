using UnityEngine;
using System.Collections;

public class destroyAudio : MonoBehaviour {
	public GameObject introAudio;
	// Use this for initialization
	void Start () {
		//Destroy the introAudio
		introAudio = GameObject.Find("IntroAudio");
		Destroy (introAudio);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
