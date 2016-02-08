using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {

	public GameObject coins;
	public bool test;

	// Use this for initialization
	void Start () {

		test = true;

	}
		

	public void createMoney(Vector2 rayOrigin) {
		if (test) {
			rayOrigin = new Vector2 (rayOrigin.x, rayOrigin.y + 1.8f);
			GameObject rocketClone = (GameObject)Instantiate (coins, rayOrigin, transform.rotation);
		}
		//test = false;

	}



}
