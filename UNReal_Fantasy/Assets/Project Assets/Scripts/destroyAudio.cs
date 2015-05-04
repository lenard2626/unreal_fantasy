using UnityEngine;
using System.Collections;

public class destroyAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (dontDestroyOnLoad.instance, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
