using UnityEngine;
using System.Collections;

public class botonesBatalla : MonoBehaviour {
	
	public Texture2D btnPuñoDebil;
	public Texture2D btnPuñoFuerte;
	public Texture2D btnPatadaDebil;
	public Texture2D btnPatadaFuerte;
	public Texture2D btnPatadaDefinitiva;
	public bool show;
	playerAttack pa;

	void Awake(){
		pa = GameObject.Find ("PersonajePrincipal").GetComponent<playerAttack> ();
	}
	void OnGUI(){

		if (show) {
			GUI.BeginGroup (new Rect (Screen.width / 2.0f - 125, Screen.height-60, 250, 50));
			if (GUI.Button (new Rect (0, 0,50,50), btnPuñoDebil)) {
				pa.setCurSkillIndex(0);
			}
			if (GUI.Button (new Rect (50, 0,50,50), btnPuñoFuerte)) {
				pa.setCurSkillIndex(1);
			}
			if (GUI.Button (new Rect (100, 0,50,50), btnPatadaDebil)) {
				pa.setCurSkillIndex(2);
			}
			if (GUI.Button (new Rect (150, 0,50,50), btnPatadaFuerte)) {
				pa.setCurSkillIndex(3);
			}
			if (GUI.Button (new Rect (200, 0,50,50), btnPatadaDefinitiva)) {
				pa.setCurSkillIndex(4);
			}
			GUI.EndGroup ();
		}
		
	}
	public void hideStatus(){
		show = false;
	}
	public void showStatus(){
		show = true;
	}
}
