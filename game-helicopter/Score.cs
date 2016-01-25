using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Score : MonoBehaviour {

	public GameObject scoreUI;
	public GameObject highScoreUI;
	public GameObject coinsUI;
	public GameObject timerUI;
	public GameObject goodTimesUI;
	public GameObject bonusUI;


	void Update () {
		if (DataManagement.datamanagement.currentScore > DataManagement.datamanagement.highScore) {           // условие отбора лучшего показателя
			DataManagement.datamanagement.highScore = DataManagement.datamanagement.currentScore;
		}
		if (DataManagement.datamanagement.timess > DataManagement.datamanagement.goodTimes) {           // условие отбора лучшего показателя
			DataManagement.datamanagement.goodTimes = DataManagement.datamanagement.timess;
		}

		scoreUI.GetComponent<Text>().text = ("Score " + DataManagement.datamanagement.currentScore.ToString ());
		highScoreUI.GetComponent<Text>().text = ("High Score " + DataManagement.datamanagement.highScore.ToString ());
		coinsUI.GetComponent<Text>().text = ("Coins Collected " + DataManagement.datamanagement.coinsCollected.ToString ());
		timerUI.GetComponent<Text>().text = ("Timer: " + DataManagement.datamanagement.timess );
		goodTimesUI.GetComponent<Text>().text = ("Good Times " + PlayerPrefs.GetInt("goodTimes"));


	}

}
