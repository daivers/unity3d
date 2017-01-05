using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	public Transform[] go;
	public int placeWhereCoins;

	// Use this for initialization
	void Start () {
		if (this.name != "fishka01(Clone)" && this.name != "fishka02(Clone)") {
		go [0] = GameObject.FindWithTag ("one").transform;
		go [1] = GameObject.FindWithTag ("two").transform;
		go [2] = GameObject.FindWithTag ("free").transform;
		go [3] = GameObject.FindWithTag ("four").transform;
		go [4] = GameObject.FindWithTag ("five").transform;

			placeWhereCoins = Random.Range (0, go.Length);
			//transform.position = Vector3.Lerp (transform.position, new Vector3 (go [placeWhereCoins].position.x + Random.Range (-60, 60), go [placeWhereCoins].position.y + Random.Range (-60, 60), 0), 1);
		
			this.transform.SetParent(go [placeWhereCoins]);
		
		}


	}


}
