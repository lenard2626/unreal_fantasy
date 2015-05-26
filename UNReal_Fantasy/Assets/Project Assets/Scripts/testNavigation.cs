using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class testNavigation : MonoBehaviour {

	public List<GameObject>destinos = new List<GameObject>();
	int objectDestino;
	float xdif, ydif, zdif;
	// Use this for initialization
	void Start () {
		objectDestino = Random.Range(0,destinos.Count);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<NavMeshAgent>().destination = new Vector3 (destinos[objectDestino].transform.position.x, destinos[objectDestino].transform.position.y,destinos[objectDestino].transform.position.z);

		/*Debug.Log("x:"+transform.position.x+"y:"+transform.position.y+" z:"+transform.position.z+"\n"+
		          "xDes:"+destinos[objectDestino].transform.position.x+"yDes:"+destinos[objectDestino].transform.position.y+" zDes:"+destinos[objectDestino].transform.position.z);*/
		xdif = Mathf.Abs( Mathf.Abs (transform.position.x) - Mathf.Abs (destinos [objectDestino].transform.position.x));
		ydif = Mathf.Abs( Mathf.Abs (transform.position.y) - Mathf.Abs (destinos [objectDestino].transform.position.y));
		zdif = Mathf.Abs( Mathf.Abs (transform.position.z) - Mathf.Abs (destinos [objectDestino].transform.position.z));
		if ((xdif <= 1) & (ydif <= 1) & (zdif <= 1)) {
			objectDestino = Random.Range(0,destinos.Count);
		}
	}
}