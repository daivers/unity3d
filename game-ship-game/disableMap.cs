using UnityEngine;
using System.Collections;

public class disableMap : MonoBehaviour {

	public GameObject player;


	
	// Update is called once per frame
	void FixedUpdate () {
		var	heading = this.GetComponent<Transform> ().position - player.GetComponent<Transform> ().position;

		if (heading.sqrMagnitude < 230000) {
			this.GetComponent<SpriteRenderer> ().enabled = true;
			
		} else
			this.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
