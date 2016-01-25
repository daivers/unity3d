using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public void LoadToSceneMain (int sceneToLoadMain) {
		Application.LoadLevel ("Main");
	}

	public void LoadToSceneShop (int sceneToLoadShop) {
		Application.LoadLevel ("Shop");
	}
}
