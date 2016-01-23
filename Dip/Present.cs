using UnityEngine;
using System.Collections;

public class Present : MonoBehaviour {

	public Rigidbody[] present;
	int index;

	// Use this for initialization
	void Start () {
	}

	public void CreatePresent(Vector3 hitPoint) {
		index = Random.Range (0, present.Length);
		Rigidbody presentDieClone = (Rigidbody) Instantiate(present[index], hitPoint, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
