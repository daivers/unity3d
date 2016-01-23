using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public Image fadePlane;
	public GameObject gameOverUI;
	public Image heartScore;
	public RectTransform newWaveBanner;  //баннер с информацией о волне и кол-ве врагов
	public Text newWaveTitle; // название волны
	public Text newWaveEnemyCount; // количество врагов
	public Text scoreUI;
	public Text gameOverScoreUI;
	public RectTransform healtBar;

	Spawner spawner;  //
	Player player;


	void Start () {
		player = FindObjectOfType<Player>();
		player.OnDeath += OnGameOver;
	}

	void Awake() {
		spawner = FindObjectOfType<Spawner> ();
		spawner.OnNewWave += OnNewWave;
	}

	void Update () {
		scoreUI.text = ScoreKeeper.score.ToString("D5");  //очки игрока
		float healthPercent = 0;
		if (player !=null) {
			healthPercent = player.health / player.startingHealth;    //процент жизни. Делим здоровье на стартовое здоровье
		}
			healtBar.localScale = new Vector3 (healthPercent, 1, 1);  //изменение размера полоски жизни
	}

	void OnNewWave (int waveNumber) {
		string[] numbers = { "One", "Two", "Free", "Four", "Five"};  //создаем array с цифрами вывода волны
		newWaveTitle.text = " - Wave" + numbers[waveNumber-1] + " -";  //выводим название волны -1 потому что с нуля счет
		string enemyCountString =((spawner.waves [waveNumber -1].infinite)?"Infinity" : spawner.waves [waveNumber -1].enemyCount + ""); //количество врагов с учетом что может быть бесконечная игра
		newWaveEnemyCount.text = "Enemies: " + enemyCountString;

		StopCoroutine ("AnimateNewWaveBunner");  //старт и стоп чтобы на новой волне начиналась заново анимация баннера
		StartCoroutine ("AnimateNewWaveBunner");

	}

	IEnumerator AnimateNewWaveBunner() {
		float delayTime = 1f;
		float speed = 2.5f;
		float animatePercent = 0;
		int dir = 1;

		float endDelayTime = Time.time + 1/speed + delayTime;

		while (animatePercent >=0) {
			animatePercent += Time.deltaTime * speed * dir;

			if (animatePercent >=1) {
				animatePercent =1;
				if (Time.time > endDelayTime) {
					dir = -1;
				}
			}
			newWaveBanner.anchoredPosition = Vector2.up* Mathf.Lerp(-100, 100, animatePercent);  //выплывающий баннер
			yield return null;
		}

	}


	void OnGameOver() {
		Cursor.visible = true;
		StartCoroutine (Fade (Color.white, new Color(0,0,0,.75f),1));
		gameOverScoreUI.text = scoreUI.text;
		scoreUI.gameObject.SetActive (false);
		healtBar.transform.parent.gameObject.SetActive (false);
		gameOverUI.SetActive (true);
		heartScore.gameObject.SetActive (false);

	}

	IEnumerator Fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp (from,to,percent);
			yield return null;
		}
	}

	//UI Input

	public void StartNewGame() {
		SceneManager.LoadScene(1);
	}

	public void ReturnToMainMenu() {
		SceneManager.LoadScene(0);
	}
}
