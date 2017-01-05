using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public googlePlayAch googleAch;
	public float time;
	public float distans; 
	public Ship2dcontroller shipContrl;
	public Text textCount;
	public Text textSpeed;
	public Text textDistans;
	public int cheakDistans;


	void Awake() {

		distans = PlayerPrefs.GetFloat ("distanse");
		cheakDistans = PlayerPrefs.GetInt ("cheakDistans");
	}


	void FixedUpdate () {
			if (time >= 0) { 
				time += Time.deltaTime;
			}


		if (shipContrl.velocite > 2) {
			distans += shipContrl.velocite / 1000;
		}

		if (distans >= 100 && cheakDistans < 1) {
			if (PlayerPrefs.GetInt ("cheakEnterGoogle") == 1) {
				googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQCA");
			}
			PlayerPrefs.SetInt ("cheakDistans", 1);
			cheakDistans = 1;
		}
		if (distans >= 300 && cheakDistans < 2) {
			if (PlayerPrefs.GetInt ("cheakEnterGoogle") == 1) {
				googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQCQ");
			}
			PlayerPrefs.SetInt ("cheakDistans", 2);
			cheakDistans = 2;
		}
		if (distans >= 500 && cheakDistans < 3) {
			if (PlayerPrefs.GetInt ("cheakEnterGoogle") == 1) {
				googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQCg");
			}
			PlayerPrefs.SetInt ("cheakDistans", 3);
			cheakDistans = 3;
		}
		if (distans >= 1000 && cheakDistans < 4) {
			if (PlayerPrefs.GetInt ("cheakEnterGoogle") == 1) {
				googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQCw");
			}
			PlayerPrefs.SetInt ("cheakDistans", 4);
			cheakDistans = 4;
		}
		if (distans >= 2500 && cheakDistans < 5) {
			if (PlayerPrefs.GetInt ("cheakEnterGoogle") == 1) {
				googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQDA");
			}
			PlayerPrefs.SetInt ("cheakDistans", 5);
			cheakDistans = 5;
		}

		textCount.text = LangSystem.lng.time + Mathf.Round(time).ToString ();
		textSpeed.text = LangSystem.lng.speed + Mathf.Round(shipContrl.velocite*2).ToString () + "/28";
		textDistans.text = "-> " + Mathf.Round(distans).ToString ();


	}


}