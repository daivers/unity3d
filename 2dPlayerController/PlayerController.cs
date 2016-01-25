using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class PlayerController : MonoBehaviour {

	public LayerMask collisionMask; 

	const float skinWidth = .015f;
	public int horizontalRayCount = 4; //количество Ray 
	public int verticalRayCount = 4;  //количество Ray 

	float maxAngleClimpAngle = 80;
	float maxDescendAngle = 75;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	BoxCollider2D collider;
	RaycastOrigins raycastOrigins; //углы нашего квадрата в структуре

	public CollisionInfo collisions;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
	}
		

	public void Move(Vector3 velocity) {
		
		UpdateRaycastOrigins();
		collisions.Reset();
		collisions.velocityOld = velocity;
		if (velocity.y <0) {
			DescendSlope(ref velocity);
		}

		if (velocity.x != 0) {
		HorizontalCollisions(ref velocity);
		}
		if (velocity.y != 0) {
		VerticalCollisions(ref velocity);
		}
		transform.Translate (velocity);
	}


	void HorizontalCollisions(ref Vector3 velocity) {  //проверка RayCasta по x
		float directionX = Mathf.Sign (velocity.x);  //Данная функция возвращает 1, если число не отрицательное (больше или равно нуля). И -1, если отрицательное (меньше нуля).
		float rayLenght = Mathf.Abs(velocity.x) + skinWidth;  //значение бе знака минус

		for (int i = 0; i< horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLenght, Color.red);

			if (hit) {

				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);  //вычисляем угол поверхности

				if (i == 0 && slopeAngle <= maxAngleClimpAngle) {

					if (collisions.descendingSlope) {
						collisions.descendingSlope = false;
						velocity = collisions.velocityOld;
					}
					float distanceToSlopeStart = 0;
					if (slopeAngle != collisions.slopeAngleOld) {
						distanceToSlopeStart = hit.distance-skinWidth;
						velocity.x -= distanceToSlopeStart * directionX;
					}
					ClimbSlope (ref velocity, slopeAngle);
					velocity.x += distanceToSlopeStart * directionX;
				}

				if (!collisions.climbingSlope || slopeAngle > maxAngleClimpAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX; //при колизии указываем координаты
					rayLenght = hit.distance;

					if (collisions.climbingSlope) {
						velocity.y = Mathf.Tan (collisions.slopeAngle*Mathf.Deg2Rad)* Mathf.Abs(velocity.x);
					}

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
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLenght = hit.distance;

				if (collisions.climbingSlope) {
					velocity.x = velocity.y / Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
				}

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}

		if (collisions.climbingSlope) {  //при восхождении убираем застревания в точках изменения угла
			float directionX = Mathf.Sign(velocity.x);
			rayLenght = Mathf.Abs(velocity.x) + skinWidth;
			Vector2 rayOrigin = ((directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight) + Vector2.up * velocity.y;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

			if (hit) {
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if (slopeAngle != collisions.slopeAngle) {
					velocity.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
				}
			}
		}
	}

	void ClimbSlope(ref Vector3 velocity, float slopeAngle) {  //угол препятствий
		float moveDistance = Mathf.Abs(velocity.x);
		float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad)*moveDistance;
		if (velocity.y <= climbVelocityY) {
			velocity.y = climbVelocityY;
			velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad)*moveDistance* Mathf.Sign(velocity.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngle;
		}
	}

	void DescendSlope (ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomRight: raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);
		if (hit) {
			float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle <= maxDescendAngle) {
				if (Mathf.Sign(hit.normal.x) == directionX) {
					if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)) {
						float moveDistance = Mathf.Abs(velocity.x);
						float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad)*moveDistance;
						velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad)*moveDistance* Mathf.Sign(velocity.x);
						velocity.y -= descendVelocityY;
						collisions.slopeAngle = slopeAngle;
						collisions.descendingSlope = true;
						collisions.below = true;

					}
				}
			}
		}
	}

	void UpdateRaycastOrigins() {  //вычисляем координаты обьекта
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x,bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x,bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x,bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x,bounds.max.y);

	}

	void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount -1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount -1);
	}
		
	struct RaycastOrigins {  
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;

	}


	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public bool climbingSlope;
		public bool descendingSlope;
		public float slopeAngle, slopeAngleOld;
		public Vector3 velocityOld;

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
