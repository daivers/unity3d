using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {
	
	private GameObject player;
	public float cameraSpeed = 2.0f;
	
	// Use this for initializationксу
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	
	void FixedUpdate () {
		
		if (Game_Init.gameIsPlaying == true) {
			
			//x position follow - следование камеры за Player по X
			Vector3 camPos = transform.position;
			camPos.x = player.transform.position.x + 65.0f;
			transform.position = Vector3.Lerp (transform.position, camPos, 120 * Time.fixedDeltaTime);   // скорость следования
			
			
			//y position follow  следование камеры за Player по Y
			//camPos.y = player.transform.position.y + 1 ;
			//transform.position = Vector3.Lerp (transform.position, camPos, 7.0f * Time.fixedDeltaTime);   // скорость следования
			
		} else {
			// если плеер умирает
			Vector3 deathCamPos = transform.position;
			deathCamPos.x = player.transform.position.x;
			transform.position = Vector3.Lerp (transform.position, deathCamPos, 2.0f * Time.fixedDeltaTime);
		}
	}
}