using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {


	public void LoadToScene (int sceneToLoad) {
		Application.LoadLevel ("Menu");
	}
}
