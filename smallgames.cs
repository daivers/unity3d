using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class smallgames : MonoBehaviour {

	public GameObject [] kubs;
	public GameObject afterBonus;
	public GameObject bonuGame;
	public GameObject greenFifty;
	public GameObject greenTwenty;
	public GameObject greenX;

	public GameObject Buttonone;
	public GameObject Buttontwo;
	public GameObject Buttonfree;
	public int bonus;
	public int k;
	public int coins;

	void Start () {
		bonus = Player_Col.bonus;
	}

	public void Newf () {

		Debug.Log(bonus + "bonus");
		--bonus;
		if (bonus >= 1) {
			Buttonone.SetActive (true); 
			Buttontwo.SetActive (true);
			Buttonfree.SetActive (true);
			Debug.Log(bonus + "--bonus");
		}
		else {

			afterBonus.gameObject.SetActive (true); 
		}
	}

	public void SmallGame () {  
		int i = Random.Range(1,8);
		if (i <= 3) {
			greenFifty.SetActive (true);
			Debug.Log("Test1111");
			DataManagement.datamanagement.currentScore = DataManagement.datamanagement.currentScore + 20;
			DataManagement.datamanagement.SaveData ();

		}
		if (i > 3 && i <= 6) {
			greenTwenty.SetActive (true); 
			Debug.Log("Test2222");
			DataManagement.datamanagement.currentScore = DataManagement.datamanagement.currentScore + 50;
			DataManagement.datamanagement.SaveData ();
		}
		if (i >= 7) {
			greenX.SetActive (true); 
			Debug.Log("Test3333");
			bonus = bonus + 3;
		}
			Debug.Log(i + "i=");
			StartCoroutine(TestCoroutine());
		GameObject.Find("Buttonone").SetActive (false); 
		GameObject.Find("Buttontwo").SetActive (false);
		GameObject.Find("Buttonfree").SetActive (false);

	}
	
		
	IEnumerator TestCoroutine(){
		yield return new WaitForSeconds(2f);
		Debug.Log ("Just waited 1 second");
		greenFifty.SetActive (false); 
		greenTwenty.SetActive (false);
		greenX.SetActive (false);
		Newf ();
		yield break;
		Debug.Log ("You'll never see this"); // produces a dead code warning
	}


}
	

