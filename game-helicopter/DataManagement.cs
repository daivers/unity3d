using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DataManagement : MonoBehaviour {

	public static DataManagement datamanagement;
	public int currentScore;
	public int highScore;
	public int coinsCollected;
	public int timess;
	public int goodTimes;

	public int proverkaIU;


	void Awake () {
		if (datamanagement == null) {
			DontDestroyOnLoad (gameObject);  //непонятно
			datamanagement = this;
		}
	
		else if (datamanagement != this)
		{
			Destroy (gameObject);
		}


	}


	void Update () {
		timess = GameObject.FindGameObjectWithTag("distanse").GetComponent<distanse>().dist;

	}
	



	public void SaveData () {
		//BinaryFormatter binForm = new BinaryFormatter();
		//FileStream file = File.Create (Application.persistentDataPath + "/gameInfo.dat");
		//gameData data = new gameData();
		//data.highScore = highScore;
		//data.coinsCollected = coinsCollected;
		//binForm.Serialize (file, data);
		//file.Close ();
		GameObject.FindGameObjectWithTag("distanse").GetComponent<distanse>().dist = timess;
		PlayerPrefs.SetInt("timess", timess);
		PlayerPrefs.SetInt("goodTimes", goodTimes);
		PlayerPrefs.SetInt("currentScore", currentScore);
		PlayerPrefs.SetInt("highScore", highScore);
		PlayerPrefs.SetInt("coinsCollected", coinsCollected);
		PlayerPrefs.SetInt("proverkaIU", proverkaIU);

		PlayerPrefs.Save();
	}

	public void LoadData () {
	//	if (File.Exists (Application.persistentDataPath + "/gameInfo.dat")) {
	//		BinaryFormatter binForm = new BinaryFormatter();
	//		FileStream file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
	//		gameData data = (gameData)binForm.Deserialize (file);
	//		file.Close();
	//		highScore = data.highScore;
	//		coinsCollected = data.coinsCollected;
		//PlayerPrefs.DeleteAll();
			goodTimes = PlayerPrefs.GetInt("goodTimes");
			timess = PlayerPrefs.GetInt("timess");
			currentScore = PlayerPrefs.GetInt("currentScore");
			highScore = PlayerPrefs.GetInt("highScore");
			coinsCollected = PlayerPrefs.GetInt("coinsCollected");
			proverkaIU = PlayerPrefs.GetInt("proverkaIU");
			PlayerPrefs.Save();
	//	}
	}
}

[Serializable]
class gameData {
		public int highScore;
		public int coinsCollected;
		public int timess;
		public int goodTimes;

}

