using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class translateGame : MonoBehaviour {


	public Text RestartLevel;
	public Text RestartLevelAIDS;

	// Use this for initialization
	void Start () {
		RestartLevel.text = LangSystem.lng.restartLevel;
		RestartLevelAIDS.text = LangSystem.lng.restartLevelAIDS;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
