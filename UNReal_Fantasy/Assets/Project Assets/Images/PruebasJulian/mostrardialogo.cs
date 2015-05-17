using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mostrardialogo : MonoBehaviour {
	private Canvas dialogo;
	private Text texto;
	private Image imagen;
	public Sprite sprite;
	// Use this for initialization
	void Start () {
		dialogo = GameObject.FindGameObjectWithTag ("Dialog").GetComponent<Canvas>();
		texto=GameObject.FindGameObjectWithTag ("Dialog").GetComponentInChildren<Text> ();
		imagen=GameObject.FindGameObjectWithTag ("DgImage").GetComponent<Image> ();
		dialogo.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(){
		dialogo.enabled=true;
		if (this.transform.parent.name == "Jimmy") {
			imagen.sprite = this.sprite;
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Jimmy");
			texto.text = "Hola, como estas, en que puedo ayudarte";
		} else if(this.transform.parent.name == "Capucho"){
			imagen.sprite = this.sprite;
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
			texto.text = "CAMILO VIVE!!!";
		}else if(this.transform.parent.name == "Celador"){
			imagen.sprite = this.sprite;
			texto.text = "El carnet por favor!";
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		}else if(this.transform.parent.name == "ProfesorCuchilla"){
			imagen.sprite = this.sprite;
			texto.text = "Vaya a estudiar vago!!";
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		}else if(this.transform.parent.name == "Nigga"){
			imagen.sprite = this.sprite;
			texto.text = "Que anda buscando el señor";
			//texto.text = this.transform.parent.name + (this.transform.parent.name == "Capucho");
		}

	}

	void OnTriggerExit(){
		dialogo.enabled=false;
	}
}
