using UnityEngine;
using System.Collections;

public class BG_Move : MonoBehaviour {
	
		
		public int playerSpeed = 72;   // скорость игрока
		
		
		
		
		
		void FixedUpdate () {
			gameObject.transform.Translate (Vector3.right * playerSpeed * Time.fixedDeltaTime);    // движение вправо умножнное на скорость на дельто тайм
		}
	}
