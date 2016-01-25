using UnityEngine;
using System.Collections;

public class randoms : MonoBehaviour {
	public int randomin;
	public GameObject scenafist;
	public GameObject scenasecond;
	public GameObject scenaeight;
	public GameObject scenafotu;
	// Use this for initialization
	public void Awake () {

		randomin = Random.Range (0, 4);
		if (randomin == 0) {
			scenafist.gameObject.SetActive (true);
		}
		else if (randomin == 1){
			scenasecond.gameObject.SetActive (true);
		}
		else if (randomin == 2) {
			scenaeight.gameObject.SetActive (true);
		}
		else if (randomin == 3){
			scenafotu.gameObject.SetActive (true);
		}
	}
	

}
