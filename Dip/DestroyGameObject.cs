using UnityEngine;
using System.Collections;

public class DestroyGameObject : MonoBehaviour {
	float lifetime = 1f;
	float fadetime = 1f;
	bool testBool = false;
	// Use this for initialization

	void Start () {

		StartCoroutine(Fade());

		if (gameObject.name != "rip(Clone)") {
			testBool = true;
		} else {
		gameObject.transform.Rotate (0,180,0);
			gameObject.transform.position = new Vector3(transform.position.x,0.8f,transform.position.z);
		}


	}

	void Update() {

		if (testBool) {
			transform.Rotate(Vector3.up * 180 * Time.deltaTime);
		}
	}

	public void Rotate () {
		transform.Rotate(Vector3.up * 100 * Time.deltaTime);
	}

	IEnumerator Fade() {
		yield return new WaitForSeconds(lifetime);   //время жизни 

		float percent = 0;
		float fadeSpeed = 1 / fadetime;


		while (percent < 1) {
			percent += Time.deltaTime * fadeSpeed;
			yield return null;
		}

		Destroy (gameObject);
	}


	void OnTriggerEnter(Collider col) 
	{
		if (col.gameObject.tag == "Player")
		{
			Destroy (gameObject);
		}
	}
		



}
