using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckProgressGame : MonoBehaviour {

	private int progress;
	public  GameObject City0101;
	public  GameObject City0102;
	public  GameObject City0103;
	public  GameObject City0104;
	public  GameObject City0105;

	public Button nextLevel;


	// Use this for initialization
	void Start () {
		progress = PlayerPrefs.GetInt ("ProgressGame");


		if (progress == 0) {
			City0101.GetComponent<Button>().interactable = true;
		}

		if (progress == 1) {
			City0101.GetComponent<Button>().interactable = true;
		}
	
		if (progress == 2) {
			City0101.GetComponent<Button>().interactable = true;
			City0102.GetComponent<Button>().interactable = true;
		}

		if (progress == 3) {
			City0101.GetComponent<Button>().interactable = true;
			City0102.GetComponent<Button>().interactable = true;
			City0103.GetComponent<Button>().interactable = true;
		}

		if (progress == 4) {
			City0101.GetComponent<Button>().interactable = true;
			City0102.GetComponent<Button>().interactable = true;
			City0103.GetComponent<Button>().interactable = true;
			City0104.GetComponent<Button>().interactable = true;
		}


		if (progress == 5) {
			City0101.GetComponent<Button>().interactable = true;
			City0102.GetComponent<Button>().interactable = true;
			City0103.GetComponent<Button>().interactable = true;
			City0104.GetComponent<Button>().interactable = true;
			City0105.GetComponent<Button>().interactable = true;
		}

		if (progress >= 6) {
			City0101.GetComponent<Button>().interactable = true;
			City0102.GetComponent<Button>().interactable = true;
			City0103.GetComponent<Button>().interactable = true;
			City0104.GetComponent<Button>().interactable = true;
			City0105.GetComponent<Button>().interactable = true;
			nextLevel.GetComponent<Button>().interactable = true;

		}
	}
	

}
