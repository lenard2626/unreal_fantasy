using UnityEngine;
using System.Collections;
using System;

public class sessionData : MonoBehaviour {

	public static sessionData currentSession;
	public static String userLoggedEmail;
	public  String apiUrl;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		if(currentSession == null){
			currentSession = this;
			GameObject.DontDestroyOnLoad(this.gameObject);

		}else{
			Destroy(this.gameObject);
		}


	}
}
