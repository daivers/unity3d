using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


public class Player_Col : MonoBehaviour {

	
	public GameObject coinsAnim;
	public GameObject otklScripts;
	public GameObject findGO;
	public GameObject MobileController;
	//public GameObject otklScriptstwo;
	//public GameObject otklScriptsfree;
	//public GameObject otklScriptsBuild;
	public GameObject vklOgnia;
	public static bool booldie = false;
	public static int bonus; 




	

	void OnTriggerEnter2D(Collider2D coll) {       //при попадании в тригер по тегу Enemy выполнение PlayerDies();
		if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Ground" ) {
			Debug.Log("HIT!");
			PlayerDies();
		}
		

		if(coll.gameObject.tag == "Coin") {
			//increase score
			DataManagement.datamanagement.coinsCollected ++;
			//increase coin collection
			DataManagement.datamanagement.currentScore ++;
			//play audio
			Debug.Log("HIT!");
			Destroy (coll.gameObject);  //при попадании в тригер по тегу Coin удаление обьекта
		}
		if(coll.gameObject.tag == "Bonus") {
			bonus++;
			Debug.Log("BoNUs+");
			Debug.Log(bonus);
			Destroy (coll.gameObject);
		}


	}
	
	public void PlayerDies () {

		booldie = true; //сообщаем о том что мы умерли

		//else {
		//	restartUI.gameObject.SetActive (true);  //activate UI for restarting game

		//}

		//GetComponent<bloorEffect> ().enabled = true;


		//MobileController.SetActive(false);
		//animationVint.gameObject.GetComponent<AudioSource>().enabled = false;
		//Time.timeScale = 0; //остоновка времени

		findGO = GameObject.Find("smallgame");
		findGO.GetComponent<proverkaGames>().sapusk ();


		otklScripts.GetComponent<Object_Spawner> ().enabled = false;
		GetComponent<Rigidbody2D>().isKinematic = true;

		GetComponent<Player_Controls> ().enabled = false;
		GetComponent<Player_Move>().enabled = false;
		
		//GetComponent<ParticleSystem> ().Play ();
		DataManagement.datamanagement.SaveData ();


	}
}
