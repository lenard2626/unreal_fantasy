using UnityEngine;
using System.Collections;

public class EscapeScript : MonoBehaviour {

	public PauseMenu scriptMenu;
	private bool menuIsClosed = true;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("escape")&&menuIsClosed) {
			Time.timeScale = 1;
			//show the pause menu
			scriptMenu = GetComponent<PauseMenu>();
			scriptMenu.enabled = true;
			menuIsClosed = false;
		}else if(Input.GetKeyUp("escape")&&!menuIsClosed){
			Time.timeScale = 1;
			//show the pause menu
			scriptMenu = GetComponent<PauseMenu>();
			scriptMenu.enabled = false;
			menuIsClosed = true;

		}
	}
}
