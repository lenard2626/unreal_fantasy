using UnityEngine;
using System.Collections;

public class gui1MainScript : MonoBehaviour {
	public static GameObject errorText;
	// Use this for initialization
	void Start () {
		this.hideLoginError();
	}
	
	// Update is called once per frame
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

}
