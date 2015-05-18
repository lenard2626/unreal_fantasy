using UnityEngine;
using System.Collections;

public class loadPJ : MonoBehaviour {
	private sessionData session;
	// Use this for initialization
	void Start () {
	this.session = GameObject.Find("SessionData").GetComponent<sessionData>();
		GameObject.Find ("PersonajePrincipal/EthanBody").GetComponent<SkinnedMeshRenderer> ().material = session.classMaterials [sessionData.load_selectedPjClass];
		GameObject.Find ("PersonajePrincipal/characterName").GetComponent<TextMesh> ().text = sessionData.load_selectedPjName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
