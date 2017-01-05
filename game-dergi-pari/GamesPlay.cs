using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Collections.Generic;

public class GamesPlay : MonoBehaviour {

	public GameObject calculatorObj;
	public GameObject answerObj;
	public GameObject nextQuestion;
	public GameObject mainMenu;
	public GameObject GameOverObj;
	public GameObject MenuTurnir;

	private string json;
	private JsonData itemData;

	public Text qText;
	public int GoodAnswers;
	public int randomMin;
	public int randomMax;
	public int levelScore;
	public Text levelScoreText;
	public Text GameOverText;
	public Text GameOverTextRound;

	public GameObject TimerObj;

	public int turnirRound;
	public bool levelCheak;

	public AudioSource AudioSource;
	public AudioClip GameOver;
	public AudioClip WinSound;


	List<object> qList;
	int randQ;

	public void OnClickPlay(int x) {

		#if UNITY_ANDROID && !UNITY_EDITOR
		string path = Path.Combine(Application.streamingAssetsPath, "questions.json");
		WWW reader = new WWW(path);
		while (!reader.isDone) { }
		json = reader.text;
		itemData = JsonMapper.ToObject (json);
		#endif
		#if UNITY_EDITOR
		json = File.ReadAllText (Application.streamingAssetsPath + "/questions.json");
		itemData = JsonMapper.ToObject (json);
		#endif

		if (x == 0) {
			questionGenerate ();
		} else {
			turnirGenerate ();
		}
	}

	public void OnliveGame() {
		MultiplayerController.Instance.SignInAndStartMPGame();
	}


	public void MenuTornamentBtn() {

		MenuTurnir.active = true;
		GameOverObj.GetComponent<Animator> ().SetTrigger ("turnirOpen");
	}
	public void MenuTornamentBtnBack() {
		MenuTurnir.active = false;

	}


	public void turnirGenerate() {
		turnirRound = 4;
		int score = PlayerPrefs.GetInt ("scoreMoney"); //забираем деньги за игру в турнир
		PlayerPrefs.SetInt ("scoreMoney", score - 50);
		questionGenerate();
		MenuTurnir.active = false;
	}

	public void questionGenerate()
	{
		if (levelScore != 7) {
			levelScore++;
			levelScoreText.text = levelScore + "/7";
			answerObj.active = true;
			calculatorObj.active = true;
			int x = Random.Range (0, 190); //количество вопросов
			Debug.Log ("Ответ на вопрос: " + itemData ["questions"] [x] ["answer"]);
			qText.text = itemData ["questions"] [x] ["question"].ToString ();
			GoodAnswers = System.Convert.ToInt32 (itemData ["questions"] [x] ["answer"].ToString ());
			randomMin = System.Convert.ToInt32 (itemData ["questions"] [x] ["randomMin"].ToString ());
			randomMax = System.Convert.ToInt32 (itemData ["questions"] [x] ["randomMax"].ToString ());

			if (!calculatorObj.GetComponent<Animator> ().enabled) {
				calculatorObj.GetComponent<Animator> ().enabled = true;
				answerObj.GetComponent<Animator> ().enabled = true;
				TimerObj.GetComponent<Animator> ().enabled = true; 
				TimerObj.GetComponent<Animator> ().SetTrigger ("timerOpen");
				answerObj.GetComponent<Animator> ().SetTrigger ("AnswerOpen");
				calculatorObj.GetComponent<Animator> ().SetTrigger ("CalculatorOpen");
			}
			if (calculatorObj.GetComponent<Animator> ().enabled) {
				answerObj.GetComponent<Animator> ().SetTrigger ("AnswerOpen");
				calculatorObj.GetComponent<Animator> ().SetTrigger ("CalculatorOpen");
				TimerObj.GetComponent<Animator> ().SetTrigger ("timerOpen");
			}

			TimerObj.GetComponent<GameTimer> ().StartTimer ();

		} else {
				
				calculatorObj.active = false;
				int[] raundPleer = new int [5];
				raundPleer [0] = GetComponent<round> ().raundPleer01;
				raundPleer [1] = GetComponent<round> ().raundPleer02;
				raundPleer [2] = GetComponent<round> ().raundPleer03;
				raundPleer [3] = GetComponent<round> ().raundPleer04;
				raundPleer [4] = GetComponent<round> ().raundPleer05;
				
			if (raundPleer [0] >= raundPleer [1] && raundPleer [0] >= raundPleer [2] && raundPleer [0] >= raundPleer [3] && raundPleer [0] >= raundPleer [4]) {
				levelCheak = true; //проверка победа или поражение в турнире
				Debug.Log ("levelCheak = true");
				AudioSource.PlayOneShot(WinSound);
			} else {
				levelCheak = false;//проверка победа или поражение в турнире
				AudioSource.PlayOneShot(GameOver);
			}
				



			if (levelCheak == true && turnirRound == 0) {  //условие если выиграл и не турнир
				GameOverTextRound.text = "Твой выигрыш "+raundPleer [0]+"$";

				int scoreMoney = raundPleer[0] + PlayerPrefs.GetInt ("scoreMoney");
				PlayerPrefs.SetInt ("scoreMoney", scoreMoney);

				int newScoreWins = PlayerPrefs.GetInt ("scoreWins") + 1;
				PlayerPrefs.SetInt ("scoreWins", newScoreWins);

				int scoreQuestion = PlayerPrefs.GetInt ("scoreQuestion") + 7;
				PlayerPrefs.SetInt ("scoreQuestion", scoreQuestion);

				GameOverText.text = "Ты выиграл!";

				} 
			if (levelCheak == false && turnirRound == 0) {  //условие если проиграл и не турнир
					GameOverText.text = "Ты проиграл!";
				}


			if (levelCheak == true && turnirRound > 1) {  
				GameOverText.text = "Ты выиграл!";
				if (turnirRound == 4) {
					GameOverTextRound.text = "1/3";
				}
				if (turnirRound == 3) {
					GameOverTextRound.text = "2/3";
				}

				turnirRound--;
			}


			if (levelCheak == true && turnirRound == 1) {  
				GameOverText.text = "Ты выиграл турнир!";
				GameOverTextRound.text = "Твой выигрыш 250$";

				int scoreMoney = PlayerPrefs.GetInt ("scoreMoney") + 250;
				PlayerPrefs.SetInt ("scoreMoney", scoreMoney);

				int newScoreWins = PlayerPrefs.GetInt ("scoreWins") + 3;
				PlayerPrefs.SetInt ("scoreWins", newScoreWins);

				int scoreQuestion = PlayerPrefs.GetInt ("scoreQuestion") + 21;
				PlayerPrefs.SetInt ("scoreQuestion", scoreQuestion);
				turnirRound--;
			}
			if (levelCheak == false && turnirRound > 0) {  
				GameOverText.text = "Ты проиграл турнир!";
				GameOverTextRound.text = "";
				turnirRound = 0;
			}


			GetComponent<round> ().ZeroScoreAfterAllGame ();		//обнуление очков
			GameOverObj.active = true;	
			GameOverObj.GetComponent<Animator> ().SetTrigger ("GameOverOpen");
			levelScore = 0;
			TimerObj.GetComponent<Animator> ().SetTrigger ("timerClose");
			GetComponent<googlePlayAch> ().connectGoogle ();
		
		} 
			




		//обнуление
		nextQuestion.active = false;
		GetComponent<round> ().answerOpenText.text = "";//обнуляем ответ
		GameObject[] gos = GameObject.FindGameObjectsWithTag("fishka"); //находим все фишки с поля
				for (int i = 0; i < gos.Length; ++i) { //удаляем их
					Destroy (gos [i]);
				}
			
	}

	public void answerBttns() {
		//qList.RemoveAt (randQ);  //удаляем вопрос из списка
		questionGenerate (); //генерируем новый вопрос
		GetComponent<calcul>().numberFirst = "0"; //обнуляем калькулятор
		GetComponent<calcul>().UpdateCalc (); 

	}

	IEnumerator animQuestion() {

		yield return new WaitForSeconds (1);
	}



	public void NewGameAfterGOBttns() {
		
		OnClickPlay(0); //генерируем новый вопрос
		GetComponent<calcul>().numberFirst = "0"; //обнуляем калькулятор
		GetComponent<calcul>().UpdateCalc (); 
		GameOverObj.active = false;
	}


	public void ExitMenuBtn() {
		GameOverObj.active = false;
		mainMenu.active = true;
		GetComponent<UIMainMenu> ().UpdateMainMenu ();
		answerObj.active = false;

	
	}
}



