using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class LangSystem : MonoBehaviour 
{
	



	public Image langBttnImg;
	public Sprite[] flags;
	private int langIndex = 1;
	private string[] langArray = { "ru_RU", "en_EN" };
	private int proverka;
	string snachenie;

	private string json;
	public static lang lng = new lang ();


	void Awake() {
		if (PlayerPrefs.GetInt ("proverkSetLang") == 0) {
			if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
				PlayerPrefs.SetString ("Language", "ru_RU");
			else
				PlayerPrefs.SetString ("Language", "en_EN");
		}


			

		LangLoad ();

	}

	void Start() {

		for (int i=0; i < langArray.Length; i++) {
			if (PlayerPrefs.GetString("Language") == langArray[i])
			{
				langIndex = i+1;
				langBttnImg.sprite = flags [i];
				break;


			}



		}


	}



	void LangLoad() {


#if UNITY_ANDROID && !UNITY_EDITOR
		string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + PlayerPrefs.GetString("Language") + ".json");
		WWW reader = new WWW(path);
		while (!reader.isDone) { }
		json = reader.text;
#endif
#if UNITY_EDITOR
		json = File.ReadAllText (Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString ("Language") + ".json");
		lng = JsonUtility.FromJson<lang> (json);
#endif
	
		lng = JsonUtility.FromJson<lang>(json);
	}

	public void switchBttn() {
		if (langIndex != langArray.Length) {
			langIndex++;
		} else {
			langIndex = 1;
		}
		PlayerPrefs.SetString ("Language", langArray [langIndex - 1]);
		snachenie = langArray [langIndex - 1];
		PlayerPrefs.SetInt ("proverkSetLang", 1);
		PlayerPrefs.SetString ("Language", snachenie);
		langBttnImg.sprite = flags [langIndex - 1];
		LangLoad ();
		GetComponent<translateSceneMenu> ().MenuLang ();

	

	}
}

public class lang {

	public string StartGame;
	public string Leaderboard;
	public string Achievement;
	public string AboutGame;
	public string Exit;

	public string Moskwa;
	public string Dubna;
	public string Kaliasin;
	public string Michkin;
	public string Yglich;

	public string Marhrut;
	public string NameGame;
	public string shvrat;
	public string save;
	public string levelComplite;
	public string time;
	public string speed;
	public string km;
	public string overspeed;
	public string crashShip;
	public string crashShore;
	public string restartLevel;
	public string restartLevelAIDS;
	public string trapRemove;
	public string passengerOnShip;
	public string mooring;
	public string nextPier;
	public string postLadder;

	public string helloDubna;
	public string helloKaliasin;
	public string helloYglich;
	public string helloMichkin;
	public string forward;
	public string back;

	public string parkingPlace;
	public string savePlace;


}
