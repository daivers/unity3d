using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;



public class Player_Controls : MonoBehaviour {

	//public static float jetPackFuel = 1.5f;               // задается значение  jetPackFuel которые тратятся при нажатии прыжка
	public float jetPackForce = 7000.0f;             // сила для прыжка
	//public GameObject jetPack;  //партикл систем


	
	void FixedUpdate () {

		if (Input.GetButton ("Jump"))
		//if (Input.touchCount > 0)
		{
				GetComponent<Rigidbody2D> ().AddForce (new Vector3 (-500, jetPackForce));
			//Debug.Log("Touch");

		}
		

	}
	
}

//	void BoostUp () {
//jetPackFuel = Mathf.MoveTowards (jetPackFuel, 0, Time.fixedDeltaTime);    //изменение положения текущего обьекта
	//GetComponent<Rigidbody2D> ().AddForce (new Vector3 (-500, jetPackForce));
//	}

	//void OnCollisionEnter (Collision Col){                         //при столкновении колайдеров с тегом "Ground" дается 1.5f
	//if (Col.gameObject.tag == "Ground") {
	//	jetPackFuel = 1.5f;
	//}


