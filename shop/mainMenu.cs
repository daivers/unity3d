using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class mainMenu : MonoBehaviour {

	public GameObject scoreUI;
	public GameObject openTrack;
	public GameObject closeTrack;
	public int closeT;
	public GameObject coinsUI;
	public GameObject goodTimesUI;



	// Use this for initialization
	void Start () {
		closeT = PlayerPrefs.GetInt("proverkaIU");
		closeT = 8 - closeT;

		scoreUI.GetComponent<Text>().text = ("" + PlayerPrefs.GetInt("currentScore"));
		goodTimesUI.GetComponent<Text>().text = ("" + PlayerPrefs.GetInt("goodTimes"));
		openTrack.GetComponent<Text>().text = ("" + PlayerPrefs.GetInt("proverkaIU"));
		closeTrack.GetComponent<Text>().text = ("" + closeT);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
