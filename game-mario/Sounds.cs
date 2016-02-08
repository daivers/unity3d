using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	public AudioClip gameover, levelUp, bump, coin, fireball, jump, kick, mariodie, powerup, stageClear, stomp, warning;
	AudioSource sound;

	void Start() {
		sound = GetComponent<AudioSource> ();
	}

	void Update () {

		if(Input.GetButtonDown ("Jump")) {
			GetComponent<AudioSource>().PlayOneShot (jump, 1f);

		}
	}

	public void track (string name) {
		if (name == "bump") {
			sound.PlayOneShot (bump, 0.2f);
		}
		if (name == "mariodie") {
			sound.PlayOneShot (mariodie, 1f);
		}
		if (name == "prize") {
			sound.PlayOneShot (kick, 1f);
		}
		if (name == "stageClear") {
			sound.PlayOneShot (stageClear, 1f);
		}

	}
}

