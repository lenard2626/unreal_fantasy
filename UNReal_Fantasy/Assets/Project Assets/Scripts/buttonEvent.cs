using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class buttonEvent : MonoBehaviour {
	public gui1MainScript gui1Controller;
	public Renderer rend;
	public Texture2D inactiveTexture;
	public Texture2D pressedTexture;
	public Texture2D activeTexture;
	public string sceneName;
	
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void OnMouseEnter() {
		rend.material.mainTexture=activeTexture;
	}
	void OnMouseDown() {
		rend.material.mainTexture=pressedTexture;
		Application.LoadLevel(sceneName);
	}
	void OnMouseUp(){
		rend.material.mainTexture=activeTexture;
	}
	void OnMouseExit() {
		rend.material.mainTexture=inactiveTexture;
	}

	public void loadScene(string name){
		Application.LoadLevel(name);

	}

	public void validatelogin(string name){
		gui1Controller = GameObject.Find("Canvas").GetComponent<gui1MainScript>();
		InputField username = GameObject.Find("UsernameField").GetComponent<InputField>();
		InputField password = GameObject.Find("PassField").GetComponent<InputField>();
		if (username.text == "test" && password.text == "test") {
			this.loadScene (name);
		} else {
			gui1Controller.showLoginError();
		}
	}

}
