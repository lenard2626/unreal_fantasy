using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.IO;

public class gui1MainScript : MonoBehaviour {
	public sessionData session;
	public static GameObject errorText;
	public errorReport reporter;
	private String apiUrl;
	public String nextScene;

	void Start () {
		this.hideLoginError();
		this.session = GameObject.Find("SessionData").GetComponent<sessionData>();
		String url = this.readUrlFromConfig ();
		this.session.apiUrl = url;
		this.apiUrl = session.apiUrl;
	}

	void Update () {
	
	}


	public String readUrlFromConfig(){
		FileInfo theSourceFile = null;
		StreamReader reader = null;
		String url = "http://unrealfantasy.herokuapp.com/";
		theSourceFile = new FileInfo (Application.dataPath + "/unrealconfig.txt");
		if ( theSourceFile != null && theSourceFile.Exists )
			reader = theSourceFile.OpenText();
		
		if ( reader == null )
		{
			Debug.Log("unrealconfig.txt not found or not readable");
		}
		else
		{
			url = reader.ReadLine();
			url.Trim();
		}
		return url;
	}

	public void hideLoginError(){
		if (!errorText) {
			errorText = GameObject.Find ("errorText");
		}
		errorText.SetActive(false);
	}
	public void showLoginError(){
		if (!errorText) {
			errorText = GameObject.Find ("errorText");
		}
		errorText.SetActive(true);
	}

	public void executeRoutine(String name){
		StartCoroutine(name);
	}

	public IEnumerator login() {
		InputField username = GameObject.Find("UsernameField").GetComponent<InputField>();
		InputField password = GameObject.Find("PassField").GetComponent<InputField>();

		WWWForm loginData = new WWWForm ();
		loginData.AddField ("email", username.text);
		loginData.AddField ("password", ComputeHash(password.text));


		HTTP.Request someRequest = new HTTP.Request( "post", apiUrl+"/user/findUser",loginData );
		someRequest.Send();
		
		while( !someRequest.isDone )
		{
			yield return null;
		}
		if (!someRequest.response.Text.Equals("")) {
			String userHash = ComputeHash(username.text+ComputeHash(password.text)+username.text);
			if(someRequest.response.Text.Equals(userHash)){ 
				sessionData.userLoggedEmail = username.text;
				sessionData.userHash = userHash;
				Application.LoadLevel(nextScene);
			}
		} else {
			this.showLoginError();
		}
	}



	public static string ComputeHash(string s){
		// Form hash
		System.Security.Cryptography.MD5 h = System.Security.Cryptography.MD5.Create();
		byte[] data = h.ComputeHash(System.Text.Encoding.Default.GetBytes(s));
		// Create string representation
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		for (int i = 0; i < data.Length; ++i) {
			sb.Append(data[i].ToString("x2"));
		}
		return sb.ToString();
	}
	
	
}
