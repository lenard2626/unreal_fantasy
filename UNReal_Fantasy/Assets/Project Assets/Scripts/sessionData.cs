using UnityEngine;
using System.Collections;
using System;

public class sessionData : MonoBehaviour {

	private static sessionData currentSession;
	public static String userLoggedEmail;
	public static String userHash;
	public static int charactersNumber;
	public static JSONObject userCharacters;

	public static int creation_selectedClass;
	public static String creation_selectedName;

	public static int load_selectedPjClass;
	public static long load_selectedPjId;
	public static String load_selectedPjName;

	public static float saveX;
	public static float saveY;
	public static float saveZ;
	public static int saveLastMision;

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

	public void saveCharacter(){
		StartCoroutine ("saveChar");

	}


	public IEnumerator saveChar() {
		
		WWWForm saveCharacter = new WWWForm ();
		saveCharacter.AddField ("id", load_selectedPjId.ToString());
		saveCharacter.AddField ("x",saveX.ToString());
		saveCharacter.AddField ("y",saveY.ToString());
		saveCharacter.AddField ("z",saveZ.ToString());
		saveCharacter.AddField ("lastmision", saveLastMision.ToString());
		
		HTTP.Request someRequest = new HTTP.Request ("post", this.apiUrl+"/character/saveCharacter", saveCharacter);
		someRequest.Send ();
		
		while (!someRequest.isDone) {
			yield return null;
		}
	}

}
