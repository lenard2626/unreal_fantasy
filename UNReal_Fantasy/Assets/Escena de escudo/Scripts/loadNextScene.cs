using UnityEngine;
using System.Collections;

public class loadNextScene : MonoBehaviour {

	public int timeout=2;

	private int timestart=0;
	private int timefinish=100;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timefinish < timestart) {
			Application.LoadLevel("gui-5");
		} else {
			timestart+=1;
		}
	}
}
