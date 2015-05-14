using UnityEngine;
using System.Collections;

public class rotateEtwas : MonoBehaviour {

	public int speedY=10;
	public int maxDeltaY=100;
	private int countY=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (countY < maxDeltaY) {
			transform.Translate (Vector3.up * Time.deltaTime * speedY);
			countY += speedY;
		}
	}
}
