using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class gui3MainScript : MonoBehaviour {
	private sessionData session;
	private String apiUrl;
	private GameObject noPj;
	private GameObject anyPj;
	private int selectedCharacter;
	private ArrayList characters;
	private errorReport reporter;
	// Use this for initialization
	void Start () {
		this.session = GameObject.Find("SessionData").GetComponent<sessionData>();
		this.apiUrl = session.apiUrl;
		this.executeRoutine ("getUserCharacters");
		noPj = GameObject.Find("NoPj");
		anyPj = GameObject.Find("AnyPj");
		anyPj.SetActive(false);
		noPj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void executeRoutine(String name){
		StartCoroutine(name);
	}


	public void nextCharacter(){
		this.selectedCharacter += 1;
		if (this.selectedCharacter > this.characters.Count-1) {
			this.selectedCharacter = 0;
		}
		this.loadCharacter ();
	}

	public void prevCharacter(){
		this.selectedCharacter -= 1;
		if (this.selectedCharacter <0) {
			this.selectedCharacter = this.characters.Count-1;
		}
		this.loadCharacter ();
	}

	public void loadCharacter (){
		JSONObject actualCharacter = (JSONObject)this.characters[selectedCharacter];
		sessionData.load_selectedPjClass = (int)actualCharacter.GetField ("type").n;
		sessionData.load_selectedPjName = actualCharacter.GetField ("name").str;
		sessionData.load_selectedPjId = (long)actualCharacter.GetField ("id").n;
		sessionData.saveX = actualCharacter.GetField ("x").n;
		sessionData.saveY = actualCharacter.GetField ("y").n;
		sessionData.saveZ = actualCharacter.GetField ("z").n;
		sessionData.saveLastMision = (int)actualCharacter.GetField ("lastMision").n;
		sessionData.saveLastMisionState = (int)actualCharacter.GetField ("lastMisionState").n;
		sessionData.saveState = (int)actualCharacter.GetField ("state").n;

		GameObject.Find("PJ/default").GetComponent<MeshRenderer>().material = session.classMaterials[(int)actualCharacter.GetField("type").n];
		GameObject.Find("nombrePersonajeTxt").GetComponent<Text>().text = actualCharacter.GetField("name").str;
		GameObject.Find ("clasePersonajeTxt").GetComponent<Text> ().text = session.classNames [(int)actualCharacter.GetField ("type").n];
		GameObject.Find("fuerzaTxt").GetComponent<Text>().text = actualCharacter.GetField("strength").n.ToString();
		GameObject.Find("inteligenciaTxt").GetComponent<Text>().text = actualCharacter.GetField("intelligence").n.ToString();
		GameObject.Find("agilidadTxt").GetComponent<Text>().text = actualCharacter.GetField("agility").n.ToString();
		GameObject.Find("espirituTxt").GetComponent<Text>().text = actualCharacter.GetField("spirit").n.ToString();
		GameObject.Find("armaTxt").GetComponent<Text>().text = actualCharacter.GetField("weapon").n.ToString();
		GameObject.Find("armaduraTxt").GetComponent<Text>().text = actualCharacter.GetField("armor").n.ToString();
		GameObject.Find("accesorioTxt").GetComponent<Text>().text = actualCharacter.GetField("trinket").n.ToString();
	}


	public IEnumerator deleteCharacter(){
		JSONObject actualCharacter = (JSONObject)this.characters[selectedCharacter];
		WWWForm requestData = new WWWForm ();
		requestData.AddField ("email", sessionData.userLoggedEmail);
		requestData.AddField ("userHash", sessionData.userHash);
		requestData.AddField ("id", actualCharacter.GetField("id").n.ToString());
		
		
		HTTP.Request someRequest = new HTTP.Request( "post", apiUrl+"/character/deleteCharacter",requestData );
		someRequest.Send();
		
		while( !someRequest.isDone )
		{
			yield return null;
		}

		if (someRequest.response.Text.Equals ("")) {
			Application.LoadLevel(Application.loadedLevel);
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

	public IEnumerator getUserCharacters() {
		WWWForm requestData = new WWWForm ();
		requestData.AddField ("email", sessionData.userLoggedEmail);
		requestData.AddField ("userHash", sessionData.userHash);
		
		
		HTTP.Request someRequest = new HTTP.Request( "post", apiUrl+"/user/userCharacters",requestData );
		someRequest.Send();
		
		while( !someRequest.isDone )
		{
			yield return null;
		}
		if (someRequest.response.Text.Equals ("nocharacters")) {
			sessionData.charactersNumber = 0;
			sessionData.userCharacters = new JSONObject ();
			noPj.SetActive(true);
		} else {
			if (someRequest.response.Text.Equals ("nouser")) {
				Application.LoadLevel ("gui-1");
			} else{
				anyPj.SetActive(true);
				sessionData.userCharacters = new JSONObject (someRequest.response.Text);
				sessionData.charactersNumber = sessionData.userCharacters.Count;
				this.characters = new ArrayList(sessionData.userCharacters.list);
				this.selectedCharacter = 0;
				this.loadCharacter();
			}
		}

	}
}
