using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour {

	float jumpHeight =6;
	float timeToJumpApex = .6f;
	float accelerationTimeAirborne = 0.2f;
	float accelerationTimeGround = .1f;
	float moveSpeed = 5f;


	Vector3 velocity;
	float velocityXSmoothing;
	float gravity;
	float jumpVelocity;
	PlayerController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController>();

		gravity = -(2 * jumpHeight)/Mathf.Pow (timeToJumpApex,2);  //улучшаем связь гравитации и прыжка
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		print ("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
	}
	
	// Update is called once per frame
	void Update () {

		if (controller.collisions.above || controller.collisions.below) {  //проверка на на hit
			velocity.y = 0;
		}
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

		float targetVelocityX= input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGround:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
}
