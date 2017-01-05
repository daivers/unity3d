using UnityEngine;
using System.Collections;

public class StartPoint : MonoBehaviour {


	public GameObject Player;
	public GameObject StartPosition;
	public GameObject Point01;
	public GameObject Point02;
	public GameObject Point03;
	public GameObject Point04;
	public GameObject Point05;
	public GameObject SavePlace01;
	public GameObject SavePlace02;
	public GameObject SavePlace03;
	public GameObject SavePlace04;
	public GameObject SavePlace05;

	int progress;
	int savePlace;
	int checkADS;

	// Use this for initialization
	void Start () {
		progress = PlayerPrefs.GetInt ("ProgressGame");
		savePlace = PlayerPrefs.GetInt ("savePlace");
		checkADS = PlayerPrefs.GetInt("CheckADS");

		//Debug.Log (progress);
		//Debug.Log (savePlace);
		//Debug.Log (checkADS);

		if (checkADS == 1) {  

			if (savePlace == 1) 
			{
				Player.transform.position = new Vector3 (SavePlace01.transform.position.x, Player.transform.position.y, SavePlace01.transform.position.z);
				Player.transform.rotation = SavePlace01.transform.rotation;
			}
			if (savePlace == 2) 
			{
				Player.transform.position = new Vector3 (SavePlace02.transform.position.x, Player.transform.position.y, SavePlace02.transform.position.z);
				Player.transform.rotation = SavePlace02.transform.rotation;
			}
			if (savePlace == 3) 
			{
				Player.transform.position = new Vector3 (SavePlace03.transform.position.x, Player.transform.position.y, SavePlace03.transform.position.z);
				Player.transform.rotation = SavePlace03.transform.rotation;
			}

			if (savePlace == 4) 
			{
				Player.transform.position = new Vector3 (SavePlace04.transform.position.x, Player.transform.position.y, SavePlace04.transform.position.z);
				Player.transform.rotation = SavePlace04.transform.rotation;
			}

			if (savePlace == 5) 
			{
				Player.transform.position = new Vector3 (SavePlace05.transform.position.x, Player.transform.position.y, SavePlace05.transform.position.z);
				Player.transform.rotation = SavePlace05.transform.rotation;
			}
		}



		if (progress == 0 && checkADS != 1) 
			{
			Player.transform.position = new Vector3 (StartPosition.transform.position.x, Player.transform.position.y, StartPosition.transform.position.z);
			}
		if (progress == 1 && checkADS != 1) 
		{
			Player.transform.position = new Vector3 (Point01.transform.position.x, Player.transform.position.y, Point01.transform.position.z);
			Player.transform.rotation = Point01.transform.rotation;

		}
		if (progress == 2 && checkADS != 1) 
		{
			Player.transform.position = new Vector3 (Point02.transform.position.x, Player.transform.position.y, Point02.transform.position.z);
			Player.transform.rotation = Point02.transform.rotation;
		}
		if (progress == 3 && checkADS != 1) 
		{
			Player.transform.position = new Vector3 (Point03.transform.position.x, Player.transform.position.y, Point03.transform.position.z);
			Player.transform.rotation = Point03.transform.rotation;
		}
		if (progress == 4 && checkADS != 1) 
		{
			Player.transform.position = new Vector3 (Point04.transform.position.x, Player.transform.position.y, Point04.transform.position.z);
			Player.transform.rotation = Point04.transform.rotation;
		}
		if (progress == 5 && checkADS != 1) 
		{
			Player.transform.position = new Vector3 (Point05.transform.position.x, Player.transform.position.y, Point05.transform.position.z);
			Player.transform.rotation = Point05.transform.rotation;

		}



		PlayerPrefs.SetInt ("CheckADS", 0);


		Time.timeScale = 1;

	}
	

}
