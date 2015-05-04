using UnityEngine;
using System.Collections;

public class rotatePJ : MonoBehaviour {
	
	public int speed=10;
	public float friction=5;
	public float lerpSpeed=5;
	private float xDeg;
	private float yDeg;
	private Quaternion fromRotation;
	private Quaternion toRotation ;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)) {

		}
	}

	void OnMouseDrag() {
		xDeg -= Input.GetAxis("Mouse X") * speed * friction;
		yDeg += Input.GetAxis("Mouse Y") * speed * friction;
		fromRotation = transform.rotation;
		toRotation = Quaternion.Euler(0,xDeg,0);
		transform.rotation = Quaternion.Lerp(fromRotation,toRotation,Time.deltaTime  * lerpSpeed);
	}
}
