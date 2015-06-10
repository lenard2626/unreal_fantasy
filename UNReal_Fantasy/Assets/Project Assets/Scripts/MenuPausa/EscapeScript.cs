using UnityEngine;
using System.Collections;

public class EscapeScript : MonoBehaviour {

	public PauseMenu scriptMenu;
	private bool menuIsClosed = true;
	// Update is called once per frame

	void Update () {
		tutorial tut=GameObject.Find ("Tutorial").GetComponent<tutorial>();
		if(Input.GetKeyUp("escape")&&menuIsClosed) {
			Time.timeScale = 1;
			tut.hideStatus();
			//show the pause menu
			scriptMenu = GetComponent<PauseMenu>();
			scriptMenu.enabled = true;
			menuIsClosed = false;
		}else if(Input.GetKeyUp("escape")&&!menuIsClosed){
			Time.timeScale = 1;
			tut.hideStatus();
			//show the pause menu
			scriptMenu = GetComponent<PauseMenu>();
			scriptMenu.enabled = false;
			menuIsClosed = true;

		}
	}
}
