using UnityEngine;
using System.Collections;


public class Game_Init : MonoBehaviour {
	

	public static bool gameIsPlaying;

	// Use this for initialization
	void Start () {
		gameIsPlaying = true;
		DataManagement.datamanagement.currentScore = 0; //обнуляет currentScore при смерти
		Player_Col.bonus = 0;

		DataManagement.datamanagement.LoadData ();
		//Time.timeScale = 1;



	}
}