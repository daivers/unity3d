using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour {

	public GameObject UImenu;
	public float maxJumpHeight =3;
	public float minJumpHeight = 1;
	float timeToJumpApex = .5f;
	float accelerationTimeAirborne = 0.2f;
	float accelerationTimeGround = .1f;
	float moveSpeed = 7f;

	public int playerHealth =10;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSliderSpeedMax = 3;
	public float wallStickTime = .02f;
	float timeToWallUnstick;


	Vector3 velocity;
	float velocityXSmoothing;
	float gravity;
	float minJumpVelocity;
	float maxJumpVelocity;
	PlayerController controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController>();

		playerHealth = 10;
		gravity = -(2 * maxJumpHeight)/Mathf.Pow (timeToJumpApex,2);  //улучшаем связь гравитации и прыжка
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpHeight = Mathf.Sqrt(2*Mathf.Abs(gravity) * minJumpHeight);
		//print ("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			GetComponent<SpriteRenderer> ().flipX = true;
			GetComponent<Animator> ().SetInteger ("animCharacter", 1);
		} else if (Input.GetAxisRaw ("Horizontal") > 0) {
			GetComponent<SpriteRenderer> ().flipX = false;
			GetComponent<Animator> ().SetInteger ("animCharacter", 1);
		} else if (Input.GetAxisRaw ("Horizontal") == 0) {
			GetComponent<Animator> ().SetInteger ("animCharacter", 0);
		}
			


		int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX= input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGround:accelerationTimeAirborne);


	

	

		if (Input.GetKeyDown(KeyCode.Space)) {
			if  (controller.collisions.below) {
				velocity.y = maxJumpVelocity;
				}
		}

//		if (Input.GetKeyUp (KeyCode.Space)) {    //ограничение на высоту прыжка
//			if (velocity.y > minJumpVelocity) {
//				velocity.y = minJumpVelocity;
//			}
//		}		

		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime, input);

		if (controller.collisions.above || controller.collisions.below) {  //проверка на на hit
			velocity.y = 0;
		}
			else {
			GetComponent<Animator> ().SetInteger ("animCharacter", 2);

		}
	}
		
	public void PlayerDie() {
		GetComponent<Player>().enabled = false;
		GetComponent<PlayerController>().soundSC.track ("mariodie");
			
		GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<Animator> ().SetInteger ("animCharacter", 3);
		//Debug.Log ("Die");
		UImenu.active = true;
		GetComponent<AudioSource> ().enabled = false;
	}

	public void PlayerWin() {
		GetComponent<Player>().enabled = false;
		GetComponent<PlayerController>().soundSC.track ("stageClear");

		GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<Animator> ().SetInteger ("animCharacter", 0);
		UImenu.active = true;
		GetComponent<AudioSource> ().enabled = false;
	}

}	
	