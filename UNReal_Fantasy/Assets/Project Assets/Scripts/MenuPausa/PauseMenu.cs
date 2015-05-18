using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public GUISkin newSkin;
	public Texture2D logoTexture;
	
	public PauseMenu scriptMenu;

	void thePauseMenu() {
		//layout start
		GUI.BeginGroup(new Rect(Screen.width /4, Screen.height/4, Screen.width/2, Screen.height/2));
		
		//the menu background box
		GUI.Box(new Rect(0, 0, Screen.width/2, Screen.height/2), "");
		
		//logo picture
		GUI.Label(new Rect(15, 10, 300, 68), logoTexture);
		
		///////pause menu buttons
		//game resume button
		if(GUI.Button(new Rect(Screen.width/8, Screen.height/8, Screen.width/4, Screen.height/10), "Volver al Juego")) {
			//resume the game
			Time.timeScale = 1;
			//disable pause menu
			scriptMenu = GetComponent<PauseMenu>();
			scriptMenu.enabled = false;
		}
		
		//main menu return button (level 0)
		if(GUI.Button(new Rect(Screen.width/8, Screen.height/4, Screen.width/4, Screen.height/10), "Menu de Seleccion de Personajes")) {
			Application.LoadLevel("gui-3");
		}
		
		//quit button
		if(GUI.Button(new Rect(Screen.width/8, Screen.height*3/8, Screen.width/4, Screen.height/10), "Salir")) {
			Application.Quit();
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
		thePauseMenu();
	}
}
