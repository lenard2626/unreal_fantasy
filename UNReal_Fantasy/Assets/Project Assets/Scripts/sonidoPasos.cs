using UnityEngine;
using System.Collections;

public class sonidoPasos : MonoBehaviour {
	public AudioClip[] SonidoPlazaChe;
	public AudioClip[] SonidoPlayita;
	private CharacterController controller;
	private float delayTime;
	private float NextPlay;
	
	void PlayFootStepSound()
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 10f) & Time.time > NextPlay)
		{
			NextPlay = delayTime + Time.time;
			delayTime = Random.Range(0.25f, 0.5f);
			switch (hit.collider.tag)
			{
			case "plazaChe":
				GetComponent<AudioSource>().clip = SonidoPlazaChe[Random.Range(0, SonidoPlazaChe.Length)];
				GetComponent<AudioSource>().Play ();
				break;
			case "playita":
				GetComponent<AudioSource>().clip = SonidoPlayita[Random.Range(0, SonidoPlayita.Length)];
				GetComponent<AudioSource>().Play();
				break;
			}
		}
	}
	
	// Funcion start para inicializar el juego
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Funcion update para determinar frame por frame y mandar llamar objetos establecidos en el constructor
	void Update () 
	{
		if (controller.isGrounded & controller.velocity.magnitude > 0.5)
		{
			PlayFootStepSound();
		}
	}
}
