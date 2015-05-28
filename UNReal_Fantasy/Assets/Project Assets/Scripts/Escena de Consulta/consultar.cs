using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class consultar : MonoBehaviour {
	public string sceneToLoad;
	public Material inactivo;
	public Material activo;
	public Material presionado;
	private sessionData session;
	private errorReport reporter;
	private String apiUrl;

	void Start(){
		this.session = GameObject.Find("SessionData").GetComponent<sessionData>();
		this.apiUrl = session.apiUrl;
	}

	void OnMouseEnter(){
		gameObject.GetComponent<Renderer>().material = activo; 
	}
	void OnMouseExit(){
		gameObject.GetComponent<Renderer>().material = inactivo; 
	}
	void OnMouseDown(){
		gameObject.GetComponent<Renderer>().material = presionado; 
	}
	void OnMouseUp(){
		gameObject.GetComponent<Renderer>().material = activo; 
		String characterName = GameObject.Find ("characterName/Text").GetComponent<Text> ().text;
		sessionData.creation_selectedName = characterName;
		StartCoroutine ("createCharacter");
	}


	public IEnumerator createCharacter(){

		WWWForm requestData = new WWWForm ();
		requestData.AddField ("email", sessionData.userLoggedEmail);
		requestData.AddField ("userHash", sessionData.userHash);
		requestData.AddField ("characterName", sessionData.creation_selectedName);
		requestData.AddField ("characterType", sessionData.creation_selectedClass);
		
		HTTP.Request someRequest = new HTTP.Request( "post", apiUrl+"/character/createCharacter",requestData );
		someRequest.Send();
		
		while( !someRequest.isDone )
		{
			yield return null;
		}

		if (someRequest.response.Text.Length < 5) {
			Application.LoadLevel (sceneToLoad);
			sessionData.load_selectedPjClass = sessionData.creation_selectedClass;
			sessionData.load_selectedPjName = sessionData.creation_selectedName;
			sessionData.saveX = -71.17f;
			sessionData.saveY = 0.1f;
			sessionData.saveZ = -25.66f;
			sessionData.saveLastMision = 0;
			sessionData.load_selectedPjId = Convert.ToInt64(someRequest.response.Text);
		} else {
			JSONObject responseJSON = new JSONObject (someRequest.response.Text);
			String requestErrors = "Errores: \n";
			ArrayList requesteErrorsList = new ArrayList(responseJSON.GetField("errors").list);
			foreach (JSONObject element in requesteErrorsList) {
				requestErrors += "-"+element.GetField("message")+"\n"; 
			}
			reporter = GetComponent<errorReport>();
			reporter.errorText = requestErrors;
			reporter.enabled = true;
		}

	}

}
