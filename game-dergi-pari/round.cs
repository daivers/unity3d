using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class round : MonoBehaviour {
	public GameObject calculatorObj;
	public GameObject nextGame;
	public GameObject stavciCloseBtn;
	public GamesPlay gamePlay;
	public int[] answerArr = {0,0,0,0,0};

	public Text[] answerTxt;  //отображаем ответы на карте

	public int goodAnswer;
	private int randomMin;
	private int randomMax;
	public Text answerOpenText;  //правильный ответ


	public int winner;
	public int[] winnerPlayerArray = {0,0,0,0,0};
	public Transform coinsPrfTwo;
	public Transform coinsPrfFree;
	public Transform coinsPrfFour;
	public Transform coinsPrfFive;

	public GameObject playerCoinsOne;
	public GameObject playerCoinsTwo;
	public GameObject pointForPlayerCoinsOne;
	public GameObject pointForPlayerCoinsTwo;



	public int[] scorePlayer = {0,0,0,0,0};
	public int[] scorePlayerProverka = {0,0,0,0,0};

	public int raundPleer01;
	public int raundPleer02;
	public int raundPleer03;
	public int raundPleer04;
	public int raundPleer05;

	public Text plrr01;
	public Text plrr02;
	public Text plrr03;
	public Text plrr04;
	public Text plrr05;

	public Transform parentCard;
	public Transform beforeParentCard;
	public GameObject[] playerCard;
	public Text[] scoreRound;
	public bool proverkaTimerAnswer;  //если пользователь нажал раньше ответ чем счетчик, счетчик обнуляется.







	public void answers(int x) {

		for (int i = 0; i < playerCard.Length; i++) {                  //снимаем выделение выигрывшей карты в начале нового раунда
			if (playerCard [i].GetComponent<Image> ().enabled != false) {
				playerCard [i].GetComponent<Image> ().enabled = false;
			};
		}
		nextGame.active = false;
		stavciCloseBtn.active = true;

		GameObject fishkaPleerPlace01 = new GameObject(); //создаем фишки игрока
		fishkaPleerPlace01 = Instantiate(playerCoinsOne, transform.position, transform.rotation) as GameObject; 
		fishkaPleerPlace01.transform.parent = pointForPlayerCoinsOne.transform.parent;
		fishkaPleerPlace01.GetComponent<RectTransform> ().position = pointForPlayerCoinsOne.GetComponent<RectTransform> ().position;
		fishkaPleerPlace01.GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);
		GameObject fishkaPleerPlace02 = new GameObject();  //создаем фишки игрока
		fishkaPleerPlace02 = Instantiate(playerCoinsTwo, transform.position, transform.rotation) as GameObject; 
		fishkaPleerPlace02.transform.parent = pointForPlayerCoinsTwo.transform.parent;
		fishkaPleerPlace02.GetComponent<RectTransform> ().position = pointForPlayerCoinsTwo.GetComponent<RectTransform> ().position;
		fishkaPleerPlace02.GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);


		goodAnswer = gamePlay.GoodAnswers; //берем из загрузки правильный ответ, мин и макс
		randomMax = gamePlay.randomMax;
		randomMin = gamePlay.randomMin;
		Debug.Log(goodAnswer);



		answerArr[0] = x; //массив ответов
		// генерируем ответы в зависимости от правильного ответа
		answerArr [1] = Random.Range (randomMin, randomMax);
		answerArr [2] = Random.Range (randomMin, randomMax);
		answerArr [3] = Random.Range (randomMin, randomMax);
		answerArr [4] = Random.Range (randomMin, randomMax);

		//передаем ответы обьектам
		answerTxt[0].text = answerArr [0].ToString();
		answerTxt[1].text = answerArr [1].ToString();
		answerTxt[2].text = answerArr [2].ToString();
		answerTxt[3].text = answerArr [3].ToString();
		answerTxt[4].text = answerArr [4].ToString();


		Debug.Log (answerArr[0]+"+"+ answerArr[1]+"+"+ answerArr[2]+"+"+ answerArr[3]+"+"+ answerArr[4]);

		winner = answerArr[0];//расчет лучшего ответа
		int min = Mathf.Abs(answerArr[0] - goodAnswer);
		if (min == 0) {
			winner = answerArr[0];  //это значение выигрыша Например 321
			winnerPlayerArray[0] = 0; // это число в массиве выигрывшего 
		} else {
			for (int i = 0; i < answerArr.Length; i++) {
				if (Mathf.Abs (answerArr [i] - goodAnswer) < min) {
					min = Mathf.Abs (answerArr [i] - goodAnswer);
					winner = answerArr [i];
					winnerPlayerArray[0] = i;
				}
			}
		}

		for (int i = 0; i < answerArr.Length; i++) {
			if (answerArr [i] == winner) {
				scorePlayer [i] = 1;
				Debug.Log ("scorePlayer [i]: " + scorePlayer [i] + "игрок: " + i);
				scorePlayerProverka [i] = 1;
			} else {
				scorePlayerProverka [i] = 0;
			}
		}
			
		Debug.Log (winner+"winner"+"игрок"+winnerPlayerArray[0]);


		System.Array.Sort (answerArr);
		Debug.Log (answerArr [0]+"+"+ answerArr [1]+"+"+ answerArr [2]+"+"+ answerArr [3]+"+"+ answerArr [4]);

		GenerateNewAnswers ();


	}
		




	public void GenerateNewAnswers() { //сортируем карточки игроков по номиналу

		for (int cc = 0; cc < answerArr.Length; cc++) {
			if (answerArr [0] == System.Convert.ToInt32 (answerTxt [cc].text)) {
				playerCard [cc].transform.SetParent (parentCard);
			}
		}
		for (int cc = 0; cc < answerArr.Length; cc++) {
			if (answerArr [1] == System.Convert.ToInt32 (answerTxt [cc].text)) {
				playerCard [cc].transform.SetParent (parentCard);
			}
		}
		for (int cc = 0; cc < answerArr.Length; cc++) {
			if (answerArr [2] == System.Convert.ToInt32 (answerTxt [cc].text)) {
				playerCard [cc].transform.SetParent (parentCard);
			}
		}
		for (int cc = 0; cc < answerArr.Length; cc++) {
			if (answerArr [3] == System.Convert.ToInt32 (answerTxt [cc].text)) {
				playerCard [cc].transform.SetParent (parentCard);
			}
		}
		for (int cc = 0; cc < answerArr.Length; cc++) {
			if (answerArr[4] == System.Convert.ToInt32(answerTxt [cc].text)) {
				playerCard[cc].transform.SetParent(parentCard);
			}
		}
	}



	public void finishDeal() { // кнопка заканчивающая игру
		stavciCloseBtn.active = false;
		proverkaTimerAnswer = true; // проверка на то что пользователь уже нажал на кнопку и счетчик можно обнулить
		StartCoroutine(InstantiatePause());

	}
		
	IEnumerator InstantiatePause() {
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfTwo, calculatorObj.transform.parent);
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfTwo, calculatorObj.transform.parent);
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfFree, calculatorObj.transform.parent);
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfFree, calculatorObj.transform.parent);
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfFour, calculatorObj.transform.parent);
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfFour, calculatorObj.transform.parent);
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfFive, calculatorObj.transform.parent);
		yield return new WaitForSeconds (Random.Range(0.1f,0.2f));
		Instantiate(coinsPrfFive, calculatorObj.transform.parent);
		yield return new WaitForSeconds (1);
		cheakWin ();
	}





	public void cheakWin() {


		answerOpenText.text = goodAnswer.ToString ();//открываем ответ
		GameObject[] gos = GameObject.FindGameObjectsWithTag("fishka"); //все фишки с поля

		if (gos.Length == 0) {
			Debug.Log("Фишки не найдены");
		}
			

		for (int vs = 0; vs < 5; vs++) {  //выделяем карту победителя
			if (scorePlayerProverka[vs] == 1) {
				Debug.Log ("vs" + vs + "scorePlayerProverka[vs]" + scorePlayerProverka [vs]);
				playerCard [vs].GetComponent<Image> ().enabled = true;
				for (int i = 0; i < gos.Length; i++) {
					if (gos [i].GetComponent<test> ().placeWhereCoins == vs) { 

						if (gos [i].name == "fishka01(Clone)") {
							scorePlayer [0] = scorePlayer [0] + 2;
						}
						if (gos [i].name == "fishka02(Clone)") {
							scorePlayer [0] = scorePlayer [0] + 2;
						}
						if (gos [i].name == "fishkaTwo(Clone)") {
							scorePlayer [1] = scorePlayer [1] + 2;
						}
						if (gos [i].name == "fishkaFree(Clone)") {
							scorePlayer [2] = scorePlayer [2] + 2;
						}
						if (gos [i].name == "fishkaFour(Clone)") {
							scorePlayer [3] = scorePlayer [3] + 2;
						}
						if (gos [i].name == "fishkaFive(Clone)") {
							scorePlayer [4] = scorePlayer [4] + 2;
						} 
					}
				}


			}
		}


		nextGame.active = true;

		stavciCloseBtn.active = false;

		ShetBallow ();





	}

	public void ShetBallow() {
		raundPleer01 = raundPleer01 + scorePlayer[0]; //подсчет очков за раунд
		raundPleer02 = raundPleer02 + scorePlayer[1];
		raundPleer03 = raundPleer03 + scorePlayer[2];
		raundPleer04 = raundPleer04 + scorePlayer[3];
		raundPleer05 = raundPleer05 + scorePlayer[4];



		plrr01.text = raundPleer01.ToString(); //выводим очки
		plrr02.text = raundPleer02.ToString();
		plrr03.text = raundPleer03.ToString();
		plrr04.text = raundPleer04.ToString();
		plrr05.text = raundPleer05.ToString();

		scoreRound [0].text = "+"+scorePlayer[0].ToString();
		scoreRound [1].text = "+"+scorePlayer[1].ToString();
		scoreRound [2].text = "+"+scorePlayer[2].ToString();
		scoreRound [3].text = "+"+scorePlayer[3].ToString();
		scoreRound [4].text = "+"+scorePlayer[4].ToString();


		scorePlayer[0] = 0; //обнуляем очки перед следующим раундом
		scorePlayer[1] = 0;
		scorePlayer[2] = 0;
		scorePlayer[3] = 0;
		scorePlayer[4] = 0;



	}


	public void ZeroScoreAfterAllGame() {
		raundPleer01 = 0;
		raundPleer02 = 0;
		raundPleer03 = 0;
		raundPleer04 = 0;
		raundPleer05 = 0;

		plrr01.text = "0"; //выводим очки
		plrr02.text = "0";
		plrr03.text = "0";
		plrr04.text = "0";
		plrr05.text = "0";
	}

	public void NewRound()  {

		nextGame.active = false;
		proverkaTimerAnswer = false;


		GetComponent<GamesPlay> ().answerBttns();

		for (int cc = 0; cc < answerArr.Length; cc++) {
			playerCard [cc].transform.SetParent (beforeParentCard);
		}

		scoreRound [0].text = "";
		scoreRound [1].text = "";
		scoreRound [2].text = "";
		scoreRound [3].text = "";
		scoreRound [4].text = "";

	}




	}

