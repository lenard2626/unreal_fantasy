using UnityEngine;
using System.Collections;

public class EscapeScript : MonoBehaviour {

	public PauseMenu scriptMenu;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("escape")) {
			Time.timeScale = 1;
			//show the pause menu
			scriptMenu = GetComponent<PauseMenu>();
			scriptMenu.enabled = true;
		}
	}
}
