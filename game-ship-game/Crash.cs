using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class Crash : MonoBehaviour {

	public googlePlayAch googleAch;
	public Text score;
	public GameObject menuDie;
	public GameObject menuPasNaBoard;
	public Button buttonShavrt;
	public Button buttonTrap;
	public Button buttonADS;
	public GameObject trap;
	public Text TextInformer;
	public int countLevel=0;
	public bool shvatProverka=false;
	public bool trapProverka=false;
	public int xx = 0;
	public int yy = 0;
	public int zz = 0;
	public AudioSource empty;

	public int count;



	private const string banner = "ca-app-pub-7842766805544242/2308297217";
	private InterstitialAd ad;


	void Start() {

		if (PlayerPrefs.GetInt ("ProgressGame") == 0) 
		{
			score.text = LangSystem.lng.shvrat;
		}



	}


	void OnTriggerStay( Collider other )
	{    
		if (other.gameObject.tag == "pier") { 
			xx = 1;
			if (xx + yy + zz == 3) {     
				if (GetComponent<Ship2dcontroller> ().velocite < 1.0f) { 
					

					if (other.gameObject.name == "pier01" && PlayerPrefs.GetInt ("ProgressGame") == 0) {//условие для првильной швартовки
						buttonShavrt.interactable = true;
						//other.gameObject.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
						shvatProverka = false;
						PlayerPrefs.SetInt("ProgressGame", 1);
						Debug.Log (PlayerPrefs.GetInt ("ProgressGame"));
					}

					if (other.gameObject.name == "pier02" &&  PlayerPrefs.GetInt ("ProgressGame") == 1) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("ProgressGame", 2);
						googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQAQ");
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.helloDubna;//"Добро пожаловать в Дубну.";
						StartCoroutine (PauseGame ());

					}
					if (other.gameObject.name == "pier03" &&  PlayerPrefs.GetInt ("ProgressGame") == 2) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("ProgressGame", 3);
						googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQAg");
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.helloKaliasin;// "Добро пожаловать в Калязин.";
						StartCoroutine (PauseGame ());

					}
					if (other.gameObject.name == "pier04" &&  PlayerPrefs.GetInt ("ProgressGame") == 3) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("ProgressGame", 4);
						googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQAw");
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.helloYglich;//"Добро пожаловать в Углич.";
						StartCoroutine (PauseGame ());

					}
					if (other.gameObject.name == "pier05" &&  PlayerPrefs.GetInt ("ProgressGame") == 4) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("ProgressGame", 5);
						googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQBQ");
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.helloMichkin;//"Добро пожаловать в Мышкин.";
						StartCoroutine (PauseGame ());

					}
					if (other.gameObject.name == "pier01" &&  PlayerPrefs.GetInt ("ProgressGame") == 5) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("ProgressGame", 6);
						googleAch.GetTheAchiv ("CgkI0t7q37sbEAIQBg");
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.levelComplite;
						StartCoroutine (PauseGame ());

					}




					if (other.gameObject.name == "savePlace01" && PlayerPrefs.GetInt ("savePlace") != 1) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("savePlace", 1);
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.save;
						StartCoroutine (PauseGame ());

					}

					if (other.gameObject.name == "savePlace02" && PlayerPrefs.GetInt ("savePlace") != 2) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("savePlace", 2);
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.save;
						StartCoroutine (PauseGame ());

					}

					if (other.gameObject.name == "savePlace03" && PlayerPrefs.GetInt ("savePlace") != 3) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("savePlace", 3);
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.save;
						StartCoroutine (PauseGame ());

					}

					if (other.gameObject.name == "savePlace04" && PlayerPrefs.GetInt ("savePlace") != 4) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("savePlace", 4);
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.save;
						StartCoroutine (PauseGame ());

					}
					if (other.gameObject.name == "savePlace05" && PlayerPrefs.GetInt ("savePlace") != 5) {//условие для првильной швартовки
						menuPasNaBoard.SetActive (true);
						PlayerPrefs.SetInt("savePlace", 5);
						menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.save;
						StartCoroutine (PauseGame ());

					}
			}
		}
		}
		if (other.gameObject.tag == "piers") { 
			yy = 1;
		}
		if (other.gameObject.tag == "pierss") { 
			zz = 1;
	}

	
			


}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "wall")
		{
			Die (LangSystem.lng.crashShore);
		}

		if(col.gameObject.tag == "shipAI")
		{
			Die (LangSystem.lng.crashShip);
		}

		if(col.gameObject.tag == "shlus")
		{
			Die (LangSystem.lng.crashShore);
		}
	}

	void OnTriggerExit( Collider other )
	{    
		if (other.gameObject.tag == "pier") { 
			xx = 0;
			buttonShavrt.interactable = false;
			buttonTrap.interactable = false;
			score.text = "";

		}


		if (other.gameObject.tag == "piers") { 
			yy = 0;
			buttonShavrt.interactable = false;
			buttonTrap.interactable = false;
			score.text = "";
		}


		if (other.gameObject.tag == "pierss") { 
			zz = 0;
			buttonShavrt.interactable = false;
			buttonTrap.interactable = false;
			score.text = "";
		}

	}

	public void ShvatButton() {

		if (shvatProverka == false) {
			GetComponent<Rigidbody> ().isKinematic = true;
			buttonTrap.interactable = true;
			score.text = LangSystem.lng.postLadder; //"Отлично, подай трап для посадки пассажиров";
			buttonShavrt.interactable = false;
			shvatProverka = true;
		}

		else if (shvatProverka == true) {
			GetComponent<Rigidbody> ().isKinematic = false;
			buttonTrap.interactable = false;
			score.text = LangSystem.lng.nextPier; //"Двигайся к следующему пункту назначения!";
			buttonShavrt.interactable = false;
			shvatProverka = false;
			countLevel = 1;

		}


	}
		

	public void trapButton() {

		if (trapProverka == false) {
			trap.SetActive (true);
			trapProverka = true;
			score.text = "";
			StartCoroutine(Example());

		} else if (trapProverka == true) {
			trap.SetActive (false);
			score.text = LangSystem.lng.mooring; //"Отшвартовывайся";
			trapProverka = false;
			buttonShavrt.interactable = true;
			buttonTrap.interactable = false;


		}

	}


	public void Die(string informer) {
		GetComponent<Rigidbody> ().isKinematic = true;
		AudioClip shipDie = GetComponent<Ship2dcontroller> ().ShipDie;
		GetComponent<AudioSource> ().mute = true;

		empty.PlayOneShot(shipDie, 0.7F);
		Time.timeScale = 0;
		TextInformer.text = informer;
		if (PlayerPrefs.GetInt ("savePlace") != 0) {
			buttonADS.interactable = true;
		} else {
			buttonADS.interactable = false;
		}
		menuDie.SetActive(true);
		PlayerPrefs.SetFloat ("distanse", GetComponent<Timer> ().distans);

	}

	public void ButtonRestartAfterDie(){
		count = PlayerPrefs.GetInt ("countGoogleAds");
		count++;
		PlayerPrefs.SetInt ("countGoogleAds", count);

		if (count == 6) {
			ad = new InterstitialAd (banner);  //рекламаz
			AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("357F32E5754D9AEA").Build();
			ad.LoadAd (request);
			ad.OnAdLoaded += OnAdLoaded;
			PlayerPrefs.SetInt ("countGoogleAds", 0);
		}
		Application.LoadLevel(2);
	}

	public void SceneMenu(){
		PlayerPrefs.SetInt ("cheakEnterGoogle", 0);
		Application.LoadLevel(0);
	}

	public void ButtonRestartAfterADS(){
		ad = new InterstitialAd (banner);  //реклама
		AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("357F32E5754D9AEA").Build();
		ad.LoadAd (request);
		ad.OnAdLoaded += OnAdLoaded;


		PlayerPrefs.SetInt("CheckADS", 1);
		Application.LoadLevel(2);

	}


	public void OnAdLoaded(object sender, System.EventArgs args) {
		ad.Show ();
	}

	IEnumerator Example() {
		menuPasNaBoard.SetActive (true);
		menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.passengerOnShip + "   0"; //Пассажиров на борту:   0
		yield return new WaitForSeconds(1);
		menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.passengerOnShip +"  14"; //Пассажиров на борту:   0
		yield return new WaitForSeconds(1);
		menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.passengerOnShip +"  35";
		yield return new WaitForSeconds(1);
		menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.passengerOnShip +"  63";
		yield return new WaitForSeconds(1);
		menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = LangSystem.lng.passengerOnShip +" 137";
		yield return new WaitForSeconds(2);
		menuPasNaBoard.SetActive (false);
		score.text = LangSystem.lng.trapRemove;//"Трап можно убрать";


	}



	IEnumerator PauseGame() {
		
		yield return new WaitForSeconds(2);
		menuPasNaBoard.transform.GetChild (0).GetComponent<Text> ().text = "";
		menuPasNaBoard.SetActive (false);

	}

	IEnumerator PauseRB() {
		yield return new WaitForSeconds(3);
	}


}