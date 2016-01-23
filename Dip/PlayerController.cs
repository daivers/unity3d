using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {
	Vector3 velocity;
	Rigidbody myRigitbody;


	void Start () {
		myRigitbody = GetComponent<Rigidbody>();
	}
	
	public void Move(Vector3 _velocity) {
		velocity = _velocity;   //обновляем значение velocity из вызова метода Move
	}

	public void LookAt(Vector3 lookPoint)   // изменение положения от мышки
	{
		Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y,lookPoint.z); //вращение плеера за координатами мышки
		transform.LookAt(heightCorrectedPoint); 
	}
	void FixedUpdate() {        
		myRigitbody.MovePosition(myRigitbody.position + velocity * Time.fixedDeltaTime);   // движение через Rigitbody
	}


}
