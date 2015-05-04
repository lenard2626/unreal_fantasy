using UnityEngine;
using System.Collections;

public class scaleULogo : MonoBehaviour {
	public float maxScale=0.4f;
	public string sceneToGo="gui-5";
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < maxScale) {
			Vector3 tempScale = transform.localScale;
			transform.localScale = tempScale + new Vector3 (0.005f, 0.005f, 0.005f);
		} else {
			Application.LoadLevel(sceneToGo);
		}
	}
}
