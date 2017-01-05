using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class translateSceneMenu : MonoBehaviour {


	public Text StartGame;
	public Text Leaderboard;
	public Text Achievement;
	public Text AboutGame;
	public Text Exit;

	public Text Moskwa;
	public Text Dubna;
	public Text Kaliasin;
	public Text Michkin;
	public Text Yglich;

	public Text Marhrut;
	public Text NameGame;

	public Text Forward;
	public Text Back;

	public Text parkingPlace;
	public Text savePlace;

	void Start() {
		MenuLang();
	}



	public void MenuLang() {
		StartGame.text = LangSystem.lng.StartGame;
		Leaderboard.text = LangSystem.lng.Leaderboard;
		Achievement.text = LangSystem.lng.Achievement;
		AboutGame.text = LangSystem.lng.AboutGame;
		Exit.text = LangSystem.lng.Exit;

		Moskwa.text = LangSystem.lng.Moskwa;
		Dubna.text = LangSystem.lng.Dubna;
		Kaliasin.text = LangSystem.lng.Kaliasin;
		Michkin.text = LangSystem.lng.Michkin;
		Yglich.text = LangSystem.lng.Yglich;

		Marhrut.text =  LangSystem.lng.Marhrut;
		NameGame.text =  LangSystem.lng.NameGame;

		Forward.text =  LangSystem.lng.forward;
		Back.text =  LangSystem.lng.back;

		parkingPlace.text =  LangSystem.lng.parkingPlace;
		savePlace.text =  LangSystem.lng.savePlace;




	}
}
