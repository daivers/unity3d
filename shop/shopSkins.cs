using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;


public class shopSkins : MonoBehaviour {
	public GameObject coinsUI;
	public Transform myButton;
	string lev;
	int coinsCollected;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt("myButtonOne") == 1) {
			myButton.GetComponent<Button>().interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		coinsCollected = PlayerPrefs.GetInt("coinsCollected");
		coinsUI.GetComponent<Text>().text = ("Coins Collected " + coinsCollected);
	}



public void buttonFist () {

		if (coinsCollected>1) {
			coinsCollected = coinsCollected - 0;
			PlayerPrefs.SetInt("coinsCollected", coinsCollected);
			PlayerPrefs.SetInt("myButtonOne", 1);
			PlayerPrefs.Save();


			myButton.GetComponent<Button>().interactable = false;
		}

		//if (PlayerPrefs.HasKey(«lev″)) {
		//	myButton.GetComponent<Button>().interactable = false;
		//}
	}

}
