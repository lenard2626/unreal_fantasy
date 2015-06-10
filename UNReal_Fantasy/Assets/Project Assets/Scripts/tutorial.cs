using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour {
	
	public Texture2D btnClose;
	public Texture2D btnOpenActive;
	public Texture2D btnOpenInactive;
	Texture2D btnOpen;
	public Texture2D flechaSig;
	public Texture2D flechaAnt;
	public Texture2D[] tutoriales;
	int tutorialActual;
	public bool show;

	float groupWidth;
	void Awake(){
		groupWidth = Screen.width / 4.0f;
		tutorialActual = 0;
		btnOpen = btnOpenInactive;
		show = false;
	}
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 2.0f - 25, 0,50,50), btnOpen)) {
			show=true;
			btnOpen=btnOpenActive;
		}
		if (show) {
			GUI.BeginGroup (new Rect (Screen.width / 4.0f, Screen.height / 4.0f, Screen.width / 2.0f, Screen.height / 2.0f));
			GUI.Label (new Rect (0, 0, Screen.width / 2.0f, Screen.height / 2.0f), tutoriales [tutorialActual]);

			if(tutorialActual<tutoriales.Length-1){
				if (GUI.Button (new Rect (Screen.width / 2.0f - 90, Screen.height / 2.0f - 60, 90, 30), flechaSig)) {
					tutorialActual++;
				}
			}
			if(tutorialActual>0){
				if (GUI.Button (new Rect (0, Screen.height / 2.0f - 60, 90, 30), flechaAnt)) {
					tutorialActual--;
				}
			}
			if (GUI.Button (new Rect (Screen.width / 2.0f - 40, 0, 40, 40), btnClose)) {
				hideStatus ();
			}
			GUI.EndGroup ();
		}

	}
	public void hideStatus(){
		show = false;
		btnOpen = btnOpenInactive;
	}
	public void showStatus(){
		show = true;
		btnOpen = btnOpenActive;
	}
}

