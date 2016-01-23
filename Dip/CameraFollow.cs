using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform myTransform;
	float speed;
	Vector3 target_pos;

	void Update() 
	{ 
		if (myTransform != null) {
		speed = 1f;
		//target_pos = Vector3.Lerp(target_pos,myTransform.position,Time.deltaTime*speed);
		Vector3 target_pos = new Vector3(myTransform.position.x, myTransform.position.y + 9, myTransform.position.z -9);
		Camera.main.transform.position = target_pos;

		//target_pos = Quaternion.Lerp(target_pos,myTransform.position,Time.deltaTime*speed);
		}
	}
}
