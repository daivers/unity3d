using UnityEngine;
using System.Collections;

public class AudioFX : MonoBehaviour {

	public AudioClip death, jetPack, coinCollect, bonusCollect;
	
	void Update () {
	
	if(Input.GetButton ("Jump")) {
			GetComponent<AudioSource> ().PlayOneShot (jetPack, 0.2f);
	}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Ground") {
			GetComponent<AudioSource>().PlayOneShot (death, 0.2f);
		}
		if (col.gameObject.tag == "Coin") {
			GetComponent<AudioSource>().PlayOneShot (coinCollect, 1.0f);
		}
		if (col.gameObject.tag == "Bonus") {
			GetComponent<AudioSource>().PlayOneShot (bonusCollect, 1.0f);
		}
	}
}

