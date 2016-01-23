//создание гильз
using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

	public Rigidbody myRigidbody;
	public float forceMin; //сила разброса
	public float forceMax;

	float lifetime = 6;
	float fadetime = 4; //время затухания

	void Start () {
		float force = Random.Range (forceMin, forceMax);// получение рандомного числа силы
		myRigidbody.AddForce (transform.right * force);  //изменение положения умн на силу
		myRigidbody.AddTorque (Random.insideUnitSphere * force);  //AddTorque задает вращение
		StartCoroutine(Fade());
	}
	
	IEnumerator Fade() {
		yield return new WaitForSeconds(lifetime);   //время жизни гильзы

		float percent = 0;
		float fadeSpeed = 1 / fadetime;
		Material mat = GetComponent<Renderer> ().material;
		Color initialColour = mat.color;


		while (percent < 1) {
			percent += Time.deltaTime * fadeSpeed;
			mat.color = Color.Lerp (initialColour, Color.clear, percent);  //затухание цвета
			yield return null;
		}

		Destroy (gameObject);
	}
}
