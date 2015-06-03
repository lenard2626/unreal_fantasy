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
	public static int saveLastMisionState;
	public static int saveState;

	public static float lastXBeforeBattle;
	public static float lastYBeforeBattle;
	public static float lastZBeforeBattle;
	public static int saveLastMisionBeforeBattle;
	public static int saveLastMisionStateBeforeBattle;
	public static int saveStateBeforeBattle;
	public static int inBattle = 0;  

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
		saveCharacter.AddField ("lastmisionstate", saveLastMisionState.ToString());
		saveCharacter.AddField ("state", saveState.ToString());

		HTTP.Request someRequest = new HTTP.Request ("post", this.apiUrl+"/character/saveCharacter", saveCharacter);
		someRequest.Send ();
		
		while (!someRequest.isDone) {
			yield return null;
		}
	}


	public static void loadPjAfterBattle(){
		//sessionData session = GameObject.Find("SessionData").GetComponent<sessionData>();
		GameObject pj = GameObject.Find ("PersonajePrincipal");
		pj.transform.position = new Vector3(lastXBeforeBattle,lastYBeforeBattle,lastZBeforeBattle);
		pj.GetComponent<registroJugador> ().loadMissions ();
		pj.GetComponent<registroJugador> ().misionActual = pj.GetComponent<registroJugador> ().missions.Find (it => it.idMision == saveLastMisionBeforeBattle);
		if (pj.GetComponent<registroJugador> ().misionActual.dueño == "Celador" && saveLastMisionStateBeforeBattle == Mision.FINALIZADA) {
			pj.GetComponent<registroJugador> ().misionActual.estado = Mision.ENPROGRESO;
		} else {
			pj.GetComponent<registroJugador> ().misionActual.estado = saveLastMisionStateBeforeBattle;
		}

		pj.GetComponent<registroJugador> ().state = saveStateBeforeBattle;
		inBattle = 0;
		//GameObject.Find ("PersonajePrincipal/EthanBody").GetComponent<SkinnedMeshRenderer> ().material = session.classMaterials [sessionData.load_selectedPjClass];
		//GameObject.Find ("PersonajePrincipal/characterName").GetComponent<TextMesh> ().text = sessionData.load_selectedPjName;
	}

}
