using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class registroJugador : MonoBehaviour {
	public int state;
	public ArrayList missions = new ArrayList ();
	public Sprite interrogante;
	public bool hasMission=false;

	void start(){
		state = 0;
	}

	public void addMission(string mision){
		state++;
		hasMission = true;
		missions.Add (mision);
	}
	public void completeMission(string mision){
		missions.Remove (mision);
		hasMission = false;
	}
	public void listMissions(){
		Text menuMisiones=GameObject.FindGameObjectWithTag ("MenuMisiones").GetComponent<Text> ();
		Canvas canvasMisiones=GameObject.Find ("MenuMisiones").GetComponent<Canvas> ();
		foreach (string mission in missions) {
			menuMisiones.text=mission;
			canvasMisiones.enabled=true;
		}
	}
}
