using UnityEngine;
using System.Collections;

public class Player_Move : MonoBehaviour {
	
	
	public static int playerSpeed = 120;   // скорость игрока
	
	
	
	
	
	void FixedUpdate () {
		gameObject.transform.Translate (Vector3.right * playerSpeed * Time.fixedDeltaTime);    // движение вправо умножнное на скорость на дельто тайм
	}
}
