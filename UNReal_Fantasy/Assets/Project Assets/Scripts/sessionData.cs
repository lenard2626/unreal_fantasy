using UnityEngine;
using System.Collections;
using System;

public class sessionData : MonoBehaviour {

	private static sessionData currentSession;
	public static String userLoggedEmail;
	public static String userHash;
	public static int charactersNumber;
	public static JSONObject userCharacters;

	public Material [] classMaterials = new Material[4];
	public String[] classNames = new String[4];
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
