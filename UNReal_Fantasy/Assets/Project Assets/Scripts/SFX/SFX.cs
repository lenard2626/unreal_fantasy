using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour {

	public GameObject SFXContainer;

	private AudioSource[] attackSFXs;
	private AudioSource[] griefSFXs;

	void Start ()
	{
		attackSFXs = SFXContainer.GetComponents<AudioSource> ();
	}

	public void PlayRandomAttack(){
		if (attackSFXs.Length > 0) {
			int index = Random.Range (0, attackSFXs.Length);
			if (attackSFXs [index] != null ) {
				attackSFXs[index].enabled = true;
				attackSFXs[index].Play();	
			}
		}
	}
}
