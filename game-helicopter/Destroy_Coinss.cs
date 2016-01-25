using UnityEngine;
using System.Collections;

public class Destroy_Coinss : MonoBehaviour {
		



	void OnTriggerEnter2D(Collider2D coll) {       //при попадании в тригер по тегу Enemy выполнение PlayerDies();
		if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Bonus") {
			Destroy (gameObject);

		}
}
}