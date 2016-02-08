using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : RayCastController {

	public LayerMask passengerMask;
	public Sounds sounds;
	public GameObject player;

	public Vector3[] localWaypoints;
	Vector3[] globalWaypoints;

	public float speed = 2f;
	public bool cyclic;
	public float waitTime;
	[Range(0,2)]
	public float easeAmount;

	int fromWaypointIndex;
	float percentBetweenWaypoints;
	float nextMoveTime;

	List<PassengerMovement> passengerMovement;
	Dictionary<Transform,PlayerController> passengerDictionary = new Dictionary<Transform, PlayerController>(); //коллекция ключей и значений

	public override void Start () {


		base.Start();

		globalWaypoints = new Vector3 [localWaypoints.Length]; //точки движения платформы
		for (int i = 0; i < localWaypoints.Length; i++) {
			globalWaypoints[i] = localWaypoints [i] + transform.position;  //координаты платформы до начала игры
		}
	}
	
	void Update () {
		UpdateRaycastOrigins(); //вычисляем координаты обьекта
		Vector3 velocity = CalculatePlatformMovement ();
		CalculatePassehgerMovement(velocity);
		MovePassengers(true);
		transform.Translate (velocity);
		MovePassengers(false);
		if (transform.position.x - player.transform.position.x < 10 && GetComponent<BoxCollider2D>().enabled != false) {
			speed = 2;		
		} 
		else {
		speed = 0;		
		}
	}

	float Ease(float x) {
		float a = easeAmount + 1;
		return Mathf.Pow (x, a) / (Mathf.Pow (x, a) + Mathf.Pow (1 - x, a)); //изменение скорости движение платформы по формуле
	}

	Vector3 CalculatePlatformMovement () {  //расчет движения платформы

		if (Time.time < nextMoveTime) {
			return Vector3.zero;
		}

		fromWaypointIndex %= globalWaypoints.Length;
		int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
		float distanceBetweenWaypoints = Vector3.Distance (globalWaypoints [fromWaypointIndex], globalWaypoints [toWaypointIndex]);
		percentBetweenWaypoints += Time.deltaTime * speed/distanceBetweenWaypoints;
		percentBetweenWaypoints = Mathf.Clamp01 (percentBetweenWaypoints);
		float easedPercentBetweenWaypoints = Ease (percentBetweenWaypoints);

		Vector3 newPos = Vector3.Lerp (globalWaypoints [fromWaypointIndex], globalWaypoints [toWaypointIndex], easedPercentBetweenWaypoints);

		if (percentBetweenWaypoints >= 1) {
			percentBetweenWaypoints = 0;
			fromWaypointIndex++;

			if (!cyclic) {
				if (fromWaypointIndex >= globalWaypoints.Length - 1) {
					fromWaypointIndex = 0;
					System.Array.Reverse (globalWaypoints); //Изменяет порядок элементов во всем массиве
			
				}
			}
			nextMoveTime = Time.time + waitTime;
		}

		return newPos - transform.position;
	}

	void MovePassengers (bool beforeMovePlarform) {
		foreach (PassengerMovement passenger in passengerMovement) {
			if (!passengerDictionary.ContainsKey(passenger.transform)) {
				passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<PlayerController>());
			}
			if (passenger.moveBeforePlatform == beforeMovePlarform) {
				passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
			}
		}

	}

	void CalculatePassehgerMovement(Vector3 velocity) {
		HashSet<Transform> movePassengers = new HashSet<Transform> ();
		passengerMovement = new List<PassengerMovement>();

		float directionX = Mathf.Sign (velocity.x);
		float directionY = Mathf.Sign (velocity.y);

		//вертикальное движение платформы
		if (velocity.y == 0 ) {
			float rayLenght = Mathf.Abs(velocity.y) + skinWidth;  //убираем знак 

			for (int i = 0; i< verticalRayCount; i++) {  //рисуем четыре линии
				Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft; 
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, passengerMask);  //пускаем луч для проверки с колизией слоя
				if (hit && hit.distance != 0) {


						GetComponent<BoxCollider2D> ().enabled = false;
						GetComponent<Animator> ().SetInteger ("enemyAnim", 1);
						speed = 0;
						sounds.track ("bump");

				}	
			}
		}
		//горизонтальное движение платформы
		if (velocity.x != 0) {
			float rayLenght = Mathf.Abs(velocity.x) + skinWidth;  //значение бе знака минус

			for (int i = 0; i< horizontalRayCount; i++) {
				Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, passengerMask);

				if (hit && hit.distance != 0) {
					if (!movePassengers.Contains(hit.transform)) {
						movePassengers.Add(hit.transform);
						float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
						float pushY = -skinWidth;
						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), false, true));
						speed = 0;
					}
					player.GetComponent<Player> ().PlayerDie ();


				}
			}	
		}
	
		//если платформа идет вниз с плеером
		if (directionY == -1 || velocity.y == 0 && velocity.x !=0) {
			float rayLenght = skinWidth * 2;

			for (int i = 0; i< verticalRayCount; i++) {  //рисуем четыре линии
				Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLenght, passengerMask);  //пускаем луч для проверки с колизией слоя

				if (hit) {

					if (!movePassengers.Contains(hit.transform)) {
						movePassengers.Add(hit.transform);
						float pushX = velocity.x;
						float pushY = velocity.y;

						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), true, false));
					}
				}	
			}
		}
	
	}

	struct PassengerMovement {
		public Transform transform;
		public Vector3 velocity;

		public bool standingOnPlatform;
		public bool moveBeforePlatform;

		public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform) {
			transform = _transform;
			velocity  = _velocity;
			standingOnPlatform = _standingOnPlatform;
			moveBeforePlatform = _moveBeforePlatform;

		}

	}

	void OnDrawGizmos() {
		if (localWaypoints != null) {
			Gizmos.color = Color.red;
			float size = .3f;

			for (int i = 0; i < localWaypoints.Length; i++) {
				Vector3 globalWaypointPos = (Application.isPlaying)?globalWaypoints[i] : localWaypoints [i] + transform.position; //условие если play берем globalWaypoints
				Gizmos.DrawLine (globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
				Gizmos.DrawLine (globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);

			}
		}
	}


}