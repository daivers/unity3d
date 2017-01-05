using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	IEnumerator Start() {
		AsyncOperation async = Application.LoadLevelAsync(UIMenu.NumberScene);
		yield return async;
	}
	

}
