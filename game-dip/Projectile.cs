//скрипт на пуле
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;  //лайер для колизии выбираем Layer с врагом
	public Color trailColour; //цвет волны у пули
	float speed = 5f;
	float damage = 1;

	float lifetime = 3;  //время жизни пули
	float skinWidth = .1f;

	public ParticleSystem hitEffect;

	void Start() {
		Destroy(gameObject, lifetime);  // удаляем пулю через 3 сек

		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject(initialCollisions[0], transform.position);
		}

		GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColour); //устанавливаем цвет
	}


	public void SetSpeed(float newSpeed) {
		speed = newSpeed; 
	}

	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate(Vector3.forward*moveDistance);   //направление движение пули forward на скорость 
	
	}

	void CheckCollisions(float moveDistance) {              //проверяем колизию
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point);
			Destroy(Instantiate(hitEffect.gameObject, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.point)) as GameObject, hitEffect.startLifetime);  //вызываем ПартиклСистем при смерти

		}
	}



	void OnHitObject (Collider c, Vector3 hitPoint) {

		IDamageable damageableObject = c.GetComponent<IDamageable>();
		if (damageableObject != null) {
			damageableObject.TakeHit(damage, hitPoint, transform.forward);
		}
		GameObject.Destroy (gameObject);
	}

}


