using UnityEngine;
using System.Collections;

/**
 * Script para manejo de controles relacionados al personaje principal
 * */

public class customController : MonoBehaviour {
	
	private playerInfoGUI playerInfoScript;
	private playerStatusGUI playerStatusScript;
	private playerAttack playerAtkScript;

	// Use this for initialization
	void Start () {
		playerInfoScript = gameObject.GetComponent<playerInfoGUI>();
		playerStatusScript = gameObject.GetComponent<playerStatusGUI>();
		playerAtkScript =  gameObject.GetComponent<playerAttack>();
	}

	void Update () {
		if (Input.GetButtonDown ("showStatus")) {
			//Muestra/oculta la barra de estado
			playerStatusScript.showStatus=!playerStatusScript.showStatus;
		}
		if (Input.GetButtonDown ("showPlayerInfo")) {
			//Muestra/oculta la barra de estado
			playerInfoScript.showStatus=!playerInfoScript.showStatus;
		}
		if (Input.GetButtonDown ("EscapeFromBattle")) {
			//Abandona el ataque
			playerAtkScript.setAttacking(false);
		}
	}
}
