using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class Ship2dcontroller : MonoBehaviour {

	float speedForce = 10f;
	float torqueForce = -0.3f;
	public float velocite ;
//	public Animator shipAnim;
	private Rigidbody rb;
	private int mover;
	private int moverRotation;
	public AudioSource shipGo;
	public AudioClip ShipDie;
	public AudioClip ShipPlay;
//	int jumpHash = Animator.StringToHash ("Jump");





	void Awake () {
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		shipGo = GetComponent<AudioSource> ();
//		shipAnim = GetComponent<Animator> ();


	}


	public void UpButton() {
		mover = 1;
	}

	public void DownButton() {
		mover = 2;
	}

	public void StopMoveButton() {
		mover = 0;
	}

	public void LeftButton() {
		moverRotation = 1;
	}

	public void RightButton() {
		moverRotation = 2;
	}

	public void StopButton() {
		moverRotation = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {


		velocite = rb.velocity.magnitude;  //вычисляем скорость
		if (velocite >= 14) {

			GetComponent<Crash> ().Die (LangSystem.lng.overspeed);
		} 

		shipGo.clip = ShipPlay;
		shipGo.pitch = Mathf.Clamp (velocite / 10, 0.5f, 3f);



		if (Input.GetButton ("Vertical") || mover == 1) {
			//Debug.Log ("dfwfw");
			rb.AddRelativeForce(0,speedForce,0);



		}

		if (mover == 2) {
			//Debug.Log ("dfwfw");
			rb.AddRelativeForce(0,-speedForce/2,0);


		}



//		if (moveJoystick.InputDirection != Vector3.zero) {
//			rb.AddRelativeForce(0, moveJoystick.InputDirection.z*speedForce, 0);
//		}

		//float turn = Input.GetAxis("Horizontal");
		//rb.AddTorque2D(transform.up * torqueForce);

//		rb.AddRelativeTorque (0, 0, moveJoystickTwo.InputDirection.x * torqueForce , ForceMode.Impulse);
		rb.AddRelativeTorque (0, 0, Input.GetAxis("Horizontal") * torqueForce , ForceMode.Impulse);
		if (moverRotation == 1) {
			rb.AddRelativeTorque (0, 0, -1 * torqueForce, ForceMode.Impulse);
		} 
		if (moverRotation == 2) {
			rb.AddRelativeTorque (0, 0, 1 * torqueForce, ForceMode.Impulse);
		} 
		if (moverRotation == 0) {
			rb.AddRelativeTorque (0, 0, 0, ForceMode.Impulse);
		}


}

}
