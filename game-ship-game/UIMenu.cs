using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class UIMenu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject fistMenu;
	public GameObject secondMenu;
	public GameObject aboutGame;



	public static int NumberScene;






	// Use this for initialization
	void Start () {
		mainMenu.active = true;
		fistMenu.active = false;
		secondMenu.active = false;
		aboutGame.active = false;
	}

	public void ButtonFist() {

		mainMenu.active = false;
		fistMenu.active = true;
		secondMenu.active = false;
		aboutGame.active = false;

	}

	public void mainMenuBtn() {

		mainMenu.active = true;
		fistMenu.active = false;
		secondMenu.active = false;
		aboutGame.active = false;

	}

	public void secondMenuBtn() {
		mainMenu.active = false;
		fistMenu.active = false;
		secondMenu.active = true;
		aboutGame.active = false;
	}

	public void aboutGameBtn() {
		mainMenu.active = false;
		fistMenu.active = false;
		secondMenu.active = false;
		aboutGame.active = true;
	}

	public void ButtonStartFistLevel() {
		NumberScene = 2;
		Application.LoadLevel(1);

	}

	public void ExitApp() {

		Application.Quit ();
	}


	public void ResetGameProcess() {


		PlayerPrefs.SetInt ("ProgressGame", 0);
		PlayerPrefs.SetInt ("savePlace", 0) ;
		PlayerPrefs.SetFloat ("distanse", 0) ;
		PlayerPrefs.SetInt ("cheakDistans", 0);
		PlayerPrefs.SetInt ("cheakEnterGoogle", 0);
		PlayerPrefs.SetInt ("proverkSetLang", 0);

		Debug.Log (PlayerPrefs.GetInt ("ProgressGame"));
		Debug.Log (PlayerPrefs.GetInt ("savePlace"));
		Debug.Log (PlayerPrefs.GetInt ("distanse"));
		Debug.Log (PlayerPrefs.GetInt ("cheakDistans"));
	}
		




}
