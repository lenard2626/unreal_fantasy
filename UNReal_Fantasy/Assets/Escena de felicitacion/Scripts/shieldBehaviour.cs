using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shieldBehaviour : MonoBehaviour {

	public int speedY=10;
	public int maxDeltaY=100;
	public int timeoutAfterCongrats=2;
	public string sceneToLoad;
	private int countY=0;
	private Text congratsTextRef=null;
	private int timeoutACCount=0;

	// Use this for initialization
	void Start () {
		congratsTextRef= GetTextObject();
		congratsTextRef.material.color=new Color(0,0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (countY < maxDeltaY) {
			transform.Translate (Vector3.up * Time.deltaTime * speedY);
			countY += speedY;
		} else {
			congratsTextRef.material.color=new Color(255,255,255,1.0f);
			if(timeoutACCount<timeoutAfterCongrats){
				timeoutACCount+=1;
			}else{
				Application.LoadLevel(sceneToLoad);
			}
		}
	}
	private Text GetTextObject()
	{
		GameObject canvas = GameObject.Find("Canvas");
		Text[] textValue = canvas.GetComponentsInChildren<Text>();
		return textValue[0];
	}
}