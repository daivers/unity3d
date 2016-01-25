using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	int gridRows = 4 ; // Значения, указывающие количество ячеек сетки и их расстояние друг от друга.
	int gridCols = 5;
	int numberGame;
	int perv = 0;
	int vtr = 1;
	int tre = 2;
	int chet = 3;
	int piat = 4;
	float offsetX = 4.5f;
	float offsetY = 6f;
	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;
	[SerializeField] public Text scoreLabel;
	[SerializeField] public Text bestT;
	[SerializeField] private Text timer; 

	private int timeBest;

	private int timeg; 
	//[SerializeField] public PopUp settingsPopup;

	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int _score = 0;
	public bool canReveal {
		get {return _secondRevealed == null;} //Функция чтения, которая возвращает значение false, если вторая карта уже открыта.
	}


	void Update() {
		Shet();
	}

	void Shet() {
		timeg = (int)Time.timeSinceLevelLoad;
		timer.text = timeg.ToString();
	}

	void Start() {

		Game ();
	}

	public void Game() {
		PlayerPrefs.DeleteAll();
		numberGame = PlayerPrefs.GetInt("level");
		timeBest = PlayerPrefs.GetInt("best");
		scoreLabel.text = "Level: " + numberGame;
		bestT.text = "Best time: " + timeBest;

		if (numberGame == 0) {
			perv = 0;
			vtr = 1;
			tre = 0;
			chet = 1;
			piat = 0;
		}
		if (numberGame == 1) {
			perv = 0;
			vtr = 1;
			tre = 2;
			chet = 0;
			piat = 1;
			Debug.Log("Next Level");
		}
		if (numberGame == 2) {
			perv = 0;
			vtr = 1;
			tre = 2;
			chet = 3;
			piat = 2;
			Debug.Log("Next Level");
		}
		if (numberGame == 3) {
			perv = 0;
			vtr = 1;
			tre = 2;
			chet = 3;
			piat = 4;
			Debug.Log("Next Level");
		}




		int[] numbers = new int[20];

		for (int i = 0; i < numbers.Length; i += 2 ) {
			if (i == 2) perv = vtr;
			if (i == 4) perv = tre;
			if (i == 6) perv = chet;
			if (i == 8) perv = piat;
			if (i == 10) perv = vtr;
			if (i == 12) perv = tre;
			if (i == 14) perv = chet;
			if (i == 16) perv = piat;
			numbers[i] = perv;
			numbers[i+1] = perv;

		}


		Vector3 startPos = originalCard.transform.position; //берем координаты карты

		numbers = ShuffleArray(numbers); // Вызов функции, перемешивающей элементы массива

		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) { 
				MemoryCard card; // Ссылка на контейнер для исходной карты или ее копий.
				if (i == 0 && j == 0) {
					card = originalCard; //если i и j равны нулю значит не создаем карту  
				} else {
					card = Instantiate(originalCard) as MemoryCard; //создаем новую карту
				}

				int index = j * gridCols + i;
				int id = numbers[index]; // Получаем идентификаторы из перемешанного списка, а не из случайных чисел.
				card.SetCard(id, images[id]);

				float posX = (offsetX * i) + startPos.x;
				float posY = -(offsetY * j) + startPos.y;
				card.transform.position = new Vector3(posX, posY, startPos.z);  //изменяем координаты
			}
		}
	}
	private int[] ShuffleArray(int[] numbers) { // Реализация алгоритма тасования Кнута. (алгоритмом Фишера—Йетса)
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++ ) {
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card) {
		if (_firstRevealed == null) { //Сохранение карт в одной из двух переменных в зависимости от того, какая из них свободна.
			_firstRevealed = card;
		} else {
			_secondRevealed = card;
			StartCoroutine(CheckMatch()); //вызывает сопрограмму после открытия двух карт.
		}
	}
	
	private IEnumerator CheckMatch() {
		if (_firstRevealed.id == _secondRevealed.id) {
			_score++;
		}
		else {
			yield return new WaitForSeconds(.5f);
			_firstRevealed.Unreveal(); // Закрытие несовпадающих карт.
				_secondRevealed.Unreveal();
		}
		_firstRevealed = null; // Очистка переменных вне зависимости от того, было ли совпадение.
			_secondRevealed = null;

		if (_score==10) {
		
			Restart();
		}
	}

	public void Restart() {
		numberGame = PlayerPrefs.GetInt("level");
		PlayerPrefs.SetInt("level", ++numberGame);
		timeBest = PlayerPrefs.GetInt("best");
		if (timeBest > timeg) {
			PlayerPrefs.SetInt("best", (int)timeg);
		}
		Application.LoadLevel("Scene"); 
	}

	public void RestartButton() {
		Application.LoadLevel("Scene");
	}


}