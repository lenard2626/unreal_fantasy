using UnityEngine;
using System.Collections;
using System;

public class errorReport : MonoBehaviour {
	public GUISkin newSkin;
	public Texture2D logoTexture;
	public String errorText;
	
	void theErrorReport() {
		//layout start
		GUI.BeginGroup(new Rect(Screen.width /4, Screen.height/4, Screen.width/2, Screen.height/2));
		
		//the menu background box
		GUI.Box(new Rect(0, 0, Screen.width/2, Screen.height/2), "");
		
		//logo picture
		GUI.Label(new Rect(15, 10, 300, 68), "Reporte de Errores");
		
		GUI.Label (new Rect (15, 80, Screen.width /2 - 10, Screen.width /2-10), errorText);
		
		//quit button
		if(GUI.Button(new Rect(Screen.width/8, Screen.height*3/8, Screen.width/4, Screen.height/10), "OK")) {
			this.enabled = false;
		}
		
		//layout end
		GUI.EndGroup(); 
	}
	
	void OnGUI () {
		//load GUI skin
		GUI.skin = newSkin;
		
		//show the mouse cursor
		Cursor.visible = true;
		
		//run the pause menu script
		theErrorReport();
	}
}
