using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class gui2MainScript : MonoBehaviour {
	private int actualClass = 0;
	private Material [] classMaterials;
	private String[] classNames;
	// Use this for initialization
	void Start () {
		this.classMaterials = GameObject.Find("SessionData").GetComponent<sessionData>().classMaterials;
		this.classNames = GameObject.Find("SessionData").GetComponent<sessionData>().classNames;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextClass(){
		this.actualClass += 1;
		if(this.actualClass > 3){
			this.actualClass = 0;
		}
		Debug.Log (this.actualClass);
		this.loadClass ();

	}

	public void prevClass(){
		this.actualClass -= 1;
		if(this.actualClass < 0){
			this.actualClass = 3;
		}
		Debug.Log (this.actualClass);
		this.loadClass ();
		
	}

	public void loadClass(){
		GameObject PJ = GameObject.Find ("ClassPJ/default");
		GameObject ClassTitle = GameObject.Find ("className");
		PJ.GetComponent<MeshRenderer> ().material = this.classMaterials [this.actualClass];
		ClassTitle.GetComponent<Text> ().text = this.classNames [this.actualClass];
		sessionData.creation_selectedClass = this.actualClass;
	}


}
