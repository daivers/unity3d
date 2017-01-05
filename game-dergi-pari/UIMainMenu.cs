using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour {

	public GameObject TurnirBtn;

	public Text levelGamer;
	public Text scoreOpenQuestion;
	public Text scoreWinTournament;
	public Text money;

	public int scoreQuestion;
	public int scoreMoney;
	public int scoreWins;
	public int scoreWinsTournament;

	public int testOpen;

	public AudioSource AudioSource;
	public AudioClip acivSound;





	void Start () {
		UpdateMainMenu ();
	}


	public void UpdateMainMenu() {

		scoreQuestion = PlayerPrefs.GetInt ("scoreQuestion");
		scoreMoney = PlayerPrefs.GetInt ("scoreMoney");
		scoreWins = PlayerPrefs.GetInt ("scoreWins");
		scoreWinsTournament = PlayerPrefs.GetInt ("scoreWinsTournament");

		testOpen = PlayerPrefs.GetInt ("testOpen");


		if (scoreWins == 0) {
			levelGamer.text = "Новичек";
			testOpen = 0;
		}
		if (scoreWins > 2 && testOpen == 0) {
			levelGamer.text = "Ученик";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQAg");
			testOpen = 1;
			AudioSource.PlayOneShot(acivSound);

		}
		if (scoreWins > 6 && testOpen == 1) {
			levelGamer.text = "Студент";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQAw");
			testOpen = 2;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 11 && testOpen == 2) {
			levelGamer.text = "Профи";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQBA");
			testOpen = 3;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 26 && testOpen == 3) {
			levelGamer.text = "Магистр";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQBQ");
			testOpen = 4;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 52 && testOpen == 4) { 
			levelGamer.text = "Гуру";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQBg");
			testOpen = 5;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 102 && testOpen == 5) { 
			levelGamer.text = "Мудрец";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQBw");
			testOpen = 6;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 152 && testOpen == 6) {
			levelGamer.text = "Мыслитель";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQCA");
			testOpen = 7;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 252 && testOpen == 7) {
			levelGamer.text = "Оракул";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQCQ");
			testOpen = 8;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 351 && testOpen == 8) {
			levelGamer.text = "Гений";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQCg");
			testOpen = 9;
			AudioSource.PlayOneShot(acivSound);
		}
		if (scoreWins > 452 && testOpen == 9) {
			levelGamer.text = "Высший разум";
			GetComponent<googlePlayAch> ().GetTheAchiv ("CgkIx77hmJEPEAIQCw");
			testOpen = 10;
			AudioSource.PlayOneShot(acivSound);
		}

		money.text = scoreMoney.ToString();
		scoreOpenQuestion.text = scoreQuestion.ToString ();
		scoreWinTournament.text = scoreWinsTournament.ToString ();

		if (scoreMoney < 50) {
			TurnirBtn.GetComponent<Button> ().interactable = false;
		} else {
			TurnirBtn.GetComponent<Button> ().interactable = true;
		}


	}



	public void ResetScore() {
		PlayerPrefs.SetInt ("scoreQuestion",0);
		PlayerPrefs.SetInt ("scoreMoney",0);
		PlayerPrefs.SetInt ("scoreWins",0);
		PlayerPrefs.SetInt ("scoreWinsTournament",0);
}
}