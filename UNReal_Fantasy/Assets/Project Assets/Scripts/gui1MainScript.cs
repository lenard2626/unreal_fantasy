using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class gui1MainScript : MonoBehaviour {
	public static GameObject errorText;
	public errorReport reporter;
	public String apiUrl;

	void Start () {
		this.hideLoginError();
	}

	void Update () {
	
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
		if (someRequest.response.Text.Equals("sucess")) {
		
		} else {
			this.showLoginError();
		}
	}

	/*public IEnumerator register() {
		WWWForm registerForm = new WWWForm ();
		registerForm.AddField ("email", "davidcamiloneo@gmail.com");
		registerForm.AddField ("password", "asdasads");

	
		HTTP.Request someRequest = new HTTP.Request ("post", "http://localhost:8080/UnrealFantasyApi/user/save", registerForm);
		someRequest.Send ();
		
		while (!someRequest.isDone) {
			yield return null;
		}
		
		// parse some JSON, for example:
		Debug.Log ("YAY");
		if (!someRequest.response.Text.Equals ("")) {
		  
			JSONObject responseJSON = new JSONObject (someRequest.response.Text);
			String requestErrors = "Errores: \n";
			ArrayList requesteErrorsList = new ArrayList(responseJSON.GetField("errors").list);
			foreach (JSONObject element in requesteErrorsList) {
				requestErrors += "-"+element.GetField("message")+"\n"; 
			}
			Debug.Log(requestErrors);
			reporter = GetComponent<errorReport>();
			reporter.errorText = requestErrors;
			reporter.enabled = true;
		}
	}*/

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
