using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class register : MonoBehaviour {
	public sessionData session;
	public errorReport reporter;
	private String apiUrl;
	public String nextScene;

	// Use this for initialization
	void Start () {
		this.session = GameObject.Find("SessionData").GetComponent<sessionData>();
		this.apiUrl = session.apiUrl;
	}
	
	// Update is called once per frames
	void Update () {
	
	}

	public void executeRoutine(String name){
		StartCoroutine(name);
	}

	public IEnumerator registerAction() {

		InputField username = GameObject.Find("UsernameField").GetComponent<InputField>();
		InputField password = GameObject.Find("PassField").GetComponent<InputField>();

		WWWForm registerForm = new WWWForm ();
		registerForm.AddField ("email", username.text);
		registerForm.AddField ("password", ComputeHash(password.text));
	
		HTTP.Request someRequest = new HTTP.Request ("post", apiUrl+"/user/save", registerForm);
		someRequest.Send ();
		
		while (!someRequest.isDone) {
			yield return null;
		}
		
		// parse some JSON, for example:
		if (!someRequest.response.Text.Equals("")) {
		  
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
		}else{
			sessionData.userLoggedEmail = username.text;
			sessionData.userHash = ComputeHash(username.text+ComputeHash(password.text)+username.text);
			Application.LoadLevel(nextScene);
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
