using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class googlePlayAch : MonoBehaviour {


	private const string achievementLevel1 = "CgkI0t7q37sbEAIQAQ";
	private const string achievementLevel2 = "CgkI0t7q37sbEAIQAg";
	private const string achievementLevel3 = "CgkI0t7q37sbEAIQAw";
	private const string achievementLevel4 = "CgkI0t7q37sbEAIQBQ";
	private const string achievementLevel5 = "CgkI0t7q37sbEAIQBg";
	private const string achievementDriven100km = "CgkI0t7q37sbEAIQCA";
	private const string achievementDriven300km = "CgkI0t7q37sbEAIQCQ";
	private const string achievementDriven500km = "CgkI0t7q37sbEAIQCg";
	private const string achievementDriven1000km = "CgkI0t7q37sbEAIQCw";
	private const string achievementDriven2500km = "CgkI0t7q37sbEAIQDA";
	private const string leaderboardLeaderboard = "CgkI0t7q37sbEAIQBw";

	public int scoreGame;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("cheakEnterGoogle") != 2) {
			PlayGamesPlatform.Activate ();
			Social.localUser.Authenticate ((bool success) => {
				if (success) {
					print ("Удачно"); 
					PlayerPrefs.SetInt ("cheakEnterGoogle", 1);
				} else {
					print ("Неудачно");
					PlayerPrefs.SetInt ("cheakEnterGoogle", 2);
				}
			});



			scoreGame = PlayerPrefs.GetInt ("ProgressGame");
			Social.ReportScore (scoreGame, leaderboardLeaderboard, (bool success) => {
				if (success)
					print ("Удачно добавлено в таблицу лидеров!");
			});
		}
	}
	


	public void GetTheAchiv (string id) {
		Social.ReportProgress (id, 100.0f, (bool success) => {
			if (success) {
				print ("Получение достижения: " + id);
			}
			else print ("Неудачно Получение достижения: " + id);
		});

	}





	public void acivkiBtn() {
		Social.ShowAchievementsUI ();
	}
	public void liderboardBtn() {
		Social.ShowLeaderboardUI ();
	}
}
