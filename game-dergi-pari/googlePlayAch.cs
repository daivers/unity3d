using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class googlePlayAch : MonoBehaviour {


	private const string achievement1 = "CgkIx77hmJEPEAIQAg";
	private const string achievement2 = "CgkIx77hmJEPEAIQAw";
	private const string achievement3 = "CgkIx77hmJEPEAIQBA";
	private const string achievement4 = "CgkIx77hmJEPEAIQBQ";
	private const string achievement5 = "CgkIx77hmJEPEAIQBg";
	private const string achievement6 = "CgkIx77hmJEPEAIQBw";
	private const string achievement7 = "CgkIx77hmJEPEAIQCA";
	private const string achievement8 = "CgkIx77hmJEPEAIQCQ";
	private const string achievement9 = "CgkIx77hmJEPEAIQCg";
	private const string achievement10 = "CgkIx77hmJEPEAIQCw";
	private const string leaderboardScore = "CgkIx77hmJEPEAIQAA";
	private const string leaderboardWins = "CgkIx77hmJEPEAIQAQ";


	public int scoreWins;
	public int scoreCoins;
	public bool conntGoogle=true;




	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate ();
		connectGoogle ();
	}
	


	public void connectGoogle() {

		if (conntGoogle) {
			Social.localUser.Authenticate ((bool success) => {
				if (success) {
					print ("Удачно"); 
				} else {
					print ("Неудачно");
				}
			});
		}
		conntGoogle = false;

			scoreCoins = PlayerPrefs.GetInt ("scoreMoney");
			scoreWins = PlayerPrefs.GetInt ("scoreWins");

			Social.ReportScore (scoreWins, leaderboardWins, (bool success) => {
				if (success)
					print ("Удачно добавлено в таблицу лидеров!");
			});
			Social.ReportScore (scoreCoins, leaderboardScore, (bool success) => {
				if (success)
					print ("Удачно добавлено в таблицу лидеров!");
			});


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
