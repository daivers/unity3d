using UnityEngine;
using System.Collections;

public class proverkaGames : Player_Col {

	public static Player_Col player_Col;
	public GameObject restartUI;
	public GameObject bonusUI;





	public void sapusk () {
		if (bonus == 0) {
			Debug.Log(bonus);
			restartUI.gameObject.SetActive (true); 
		}

		for (int i = bonus; i > 0; i--) {
			Debug.Log(bonus);
				bonusUI.gameObject.SetActive (true); 

			}




	}
	}

