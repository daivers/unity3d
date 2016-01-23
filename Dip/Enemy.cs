using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]
// подключаем NavMeshAgent (карта)

 public class Enemy : LivingEntity
{
	public enum State {Idle, Chasing, Attacking};   //создаем группу состояний, может быть выбрано только одно состояние  (бездействие/преследование/атака)
	State currentState;   // текущее состояние

	public ParticleSystem deathEffect;
	public Rigidbody presentDie;

	public static event System.Action OnDeathStatic;

	Present present;
	NavMeshAgent pathfinder;   // компонент NavMeshAgent
	Transform target;          // трансформ Игрока
	LivingEntity targetEntity;
	Material skinMaterial;     // материал для смены цвета врага при ударе

	Color originalColor;   //оригинальный цвет врага

	float attackDistanceThreshold = .5f;
	float timeBetweenAttacks = 1;
	float damage = 0.5f;  

	float nextAttackTime;
	float myCollisionRadius;    // врага коллайдер
	float targetCollisionRadius;  // мой коллайдер для столкновения с врагом

	bool hasTarget;

	void Awake() {

		pathfinder = GetComponent<NavMeshAgent> ();  // компонент NavMeshAgent
		present = GetComponent<Present> ();
		
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			hasTarget = true;
			
			target = GameObject.FindGameObjectWithTag ("Player").transform; // находим по тегу трансформ Игрока
			targetEntity = target.GetComponent<LivingEntity>();

			myCollisionRadius = GetComponent<CapsuleCollider> ().radius;  //берем врага коллайдер.radius 
			targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius; // берем свой коллайдер.radius 

	}
	}

	protected override void Start ()
	{
		base.Start ();  //наследуемся от базового LivingEntity 

		if (hasTarget) {
		currentState = State.Chasing;  //присваем состояние преследования
		targetEntity.OnDeath += onTargetDeath;

	
		StartCoroutine (UpdatePath ());  //запуск UpdatePath
		}
	}

	public void SetCharacteristics (float moveSpeed, int hitsToKillPlayer, float enemyHealth, Color skinColour) {
		pathfinder.speed = moveSpeed;

		//if (hasTarget) {
		//	damage = Mathf.Ceil(targetEntity.startingHealth / hitsToKillPlayer);
		//}

		startingHealth = enemyHealth;
		deathEffect.startColor = new Color(skinColour.r, skinColour.g, skinColour.b,1);
		skinMaterial = GetComponent<Renderer>().material;
		skinMaterial.color = skinColour;
		originalColor = skinMaterial.color;


	}

	public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		AudioManager.instance.PlaySound ("Impact", transform.position);
		if (damage >= health) {
			if (OnDeathStatic !=null) {
				OnDeathStatic();
				present.CreatePresent(hitPoint);
			}
			AudioManager.instance.PlaySound ("Enemy Death", transform.position);
			Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);  //вызываем ПартиклСистем при смерти
		}
		base.TakeHit (damage, hitPoint, hitDirection);
	}


	void onTargetDeath() {
		hasTarget = false;
		currentState = State.Idle;



	}


	
	void Update ()
	{
		if (hasTarget) {
			if (Time.time > nextAttackTime) {           //рассчитываем время перед выполнением каждой аттаки
				float sqrDsrToTarget = (target.position - transform.position).sqrMagnitude;   //Возвращает квадрат длины вектора lо цели
				if (sqrDsrToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)) {   //Mathf.Pow возвращает число возведенное в степень 2
					nextAttackTime = Time.time + timeBetweenAttacks;
					AudioManager.instance.PlaySound ("Enemy Attack", transform.position);
					StartCoroutine (Attack ());
				}
			}
		}
	}

	IEnumerator Attack() {

		currentState = State.Attacking;
		pathfinder.enabled = false;

		Vector3 originalPosition = transform.position;  // берем позицию
		Vector3 dirToTarget = (target.position - transform.position).normalized; //расстояние до цели 
		Vector3 attackPosition = target.position - dirToTarget*(myCollisionRadius);  //от расстояния до цели вычитаем радиус коллайдера игрока

		float attackSpeed = 3;  //скорость атаки
		float percent = 0;

		//skinMaterial.color = Color.red;   // изменение цвета при атаке
		bool hasAppliedDamage = false;

		while (percent <= 1) {

			if (percent >= 0.5f && !hasAppliedDamage) {
				hasAppliedDamage = true;
				targetEntity.TakeDamage(damage);
			}
			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-Mathf.Pow (percent,2) + percent)*4; //затухание атаки
			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

			yield return null;
		}

	//	skinMaterial.color = originalColor;  // меняем цвет на оригинальный
		currentState = State.Chasing;  // переходим в состояние преследования, чтобы зациклить поиск-атака
		pathfinder.enabled = true;  
	}
		


	// Сокращаем количество вычеслений в Update до значения refreshRate
	IEnumerator UpdatePath() {
		float refreshRate = .25f;
		
		while (hasTarget) {
			if (currentState == State.Chasing) {
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
				if (!dead) {
					pathfinder.SetDestination (targetPosition);
				}
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}
}
