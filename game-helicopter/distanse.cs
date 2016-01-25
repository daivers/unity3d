using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class distanse : MonoBehaviour {


public Transform point;
public Transform x;
public  int dist;
public float sliderText;

	void Start() {
		dist = 0;
	}
void Update () {
sliderText = Vector3.Distance(x.position,point.position);
		dist = (int)sliderText;


	}
}
