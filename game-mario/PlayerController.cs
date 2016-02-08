using UnityEngine;
using System.Collections;

public class PlayerController : RayCastController {

	float maxAngleClimpAngle = 80;
	public Sounds soundSC;
	public Money money;

	public CollisionInfo collisions;
	[HideInInspector]
	public Vector2 playerInput;




	public override void Start() {
		base.Start();
		collisions.faceDir = 1;
	}

	public void Move (Vector3 velocity, bool standingOnPlatform) {
		Move (velocity, Vector2.zero, standingOnPlatform);
	}
	public void Move(Vector3 velocity, Vector2 input, bool standingOnPlatform = false) {
		
		UpdateRaycastOrigins(); //координаты
		collisions.Reset(); //сбрасываем значения колизии на false
		collisions.velocityOld = velocity;
		playerInput = input;

		if (velocity.x != 0) {
			collisions.faceDir = (int)Mathf.Sign (velocity.x);
		}



		HorizontalCollisions(ref velocity);

		if (velocity.y != 0) {
		VerticalCollisions(ref velocity);
		}
		transform.Translate (velocity);

		if (standingOnPlatform) {
			collisions.below = true;
		}
	}


	void HorizontalCollisions(ref Vector3 velocity) {  //проверка RayCasta по x

		float directionX = collisions.faceDir;
		float rayLenght = Mathf.Abs(velocity.x) + skinWidth;  //значение бе знака минус

		if (Mathf.Abs(velocity.x) < skinWidth) {
			rayLenght = 2 * skinWidth;
		}

		for (int i = 0; i< horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLenght, Color.red);

			if (hit) {

				if (hit.distance == 0) {
					continue;
				}

				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);  //вычисляем угол поверхности


				if (!collisions.climbingSlope || slopeAngle > maxAngleClimpAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX; //при колизии указываем координаты
					rayLenght = hit.distance;

					collisions.left = directionX == -1;
					collisions.right = directionX == 1;
				}
			}

		}
	}


	void VerticalCollisions(ref Vector3 velocity) { //проверка RayCasta по y
		float directionY = Mathf.Sign (velocity.y); //если число с минусом то 0, если с + то 1
		float rayLenght = Mathf.Abs(velocity.y) + skinWidth;  //убираем знак 

		for (int i = 0; i< verticalRayCount; i++) {  //рисуем четыре линии
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;  //присваиваем значение в зависимости от знака
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);  //пускаем луч для проверки с колизией слоя
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLenght, Color.red); //выводи луч

			if (hit) {

				if (hit.collider.tag == "Through") {
					if (directionY == 1 || hit.distance == 0) {
						soundSC.track("prize");
						money = money.GetComponent<Money>();
						money.createMoney (rayOrigin);
					}
					if (collisions.fallingThroughPlatform) {
					}
					if (playerInput.y == -1) {
						
					}
				}

				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLenght = hit.distance;


				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			} 
		}


	}



	void ResetFallingThroughtPlatform() {
		collisions.fallingThroughPlatform = false;
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public bool climbingSlope;
		public bool descendingSlope;
		public float slopeAngle, slopeAngleOld;
		public Vector3 velocityOld;
		public int faceDir;
		public bool fallingThroughPlatform;

		public void Reset() {
			above=below=false;
			left=right=false;
			climbingSlope = false;
			descendingSlope = false;
			
			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}
}
