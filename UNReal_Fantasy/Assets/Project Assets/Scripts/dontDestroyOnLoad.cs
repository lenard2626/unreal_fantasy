using UnityEngine;
using System.Collections;

public class dontDestroyOnLoad : MonoBehaviour {

	public static dontDestroyOnLoad instance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		if (dontDestroyOnLoad.instance == null) {
			dontDestroyOnLoad.instance = this;
			GameObject.DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}
}
