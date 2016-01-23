using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]  //Автоматически этому объекту добавится компонент PlayerController
[RequireComponent (typeof (GunController))]   //Автоматически этому объекту добавится компонент GunController

public class Player : LivingEntity {     //наследуется от LivingEntity

	public float moveSpeed = 5;

	public CrossHairs crosshairs;

	PlayerController controller;    //скрипт управления персонажем
	Camera viewCamera;              // main Camera
	GunController gunController;    //скрипт выбора оружия
	public Animator animator;

	public enum State {Idle, Run, TurnLeft, TurnRight};   //создаем группу состояний, может быть выбрано только одно состояние  (бездействие/преследование/атака)
	public State currentState;   // текущее состояние


	protected override void Start () {
		base.Start ();

	}

	void Awake() {
		controller = GetComponent<PlayerController>();
		gunController = GetComponent<GunController>();
		currentState = State.Idle;
		viewCamera = Camera.main;
		FindObjectOfType<Spawner> ().OnNewWave += OnNewWave;
	}

	void OnNewWave (int waveNumber) {
		health = startingHealth;
		//gunController.EquipGun (waveNumber-1); //изменение оружия с новой волной
		gunController.EquipGun (0); //изменение оружия с новой волной

	}
	
	void Update () {
		//Управление персонажем
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));  //берем input с клавиатуры

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)) {
			currentState = State.Run;
		} else {
			currentState = State.Idle;
		}


		Vector3 moveVelocity = moveInput.normalized*moveSpeed; //движение умноженое на скорость
		controller.Move(moveVelocity);  //вызываем Move в скрипте PlayerController

		//Слежение за мышкой
		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);  //луч для отслеживания положения мышки
		Plane groundPlane = new Plane (Vector3.up, Vector3.up * gunController.GunHeight);     //создаем plane
		float rayDistanse;


		if (groundPlane.Raycast(ray, out rayDistanse)) {
			Vector3 point = ray.GetPoint(rayDistanse);
			Debug.DrawLine(ray.origin,point,Color.red);  //смотрим луч
			controller.LookAt (point);      //следим за изменением координат персонажа, чтобы вращать его за движением мышки 

			crosshairs.transform.position = point;
			crosshairs.DetectTargets(ray);
			if 	((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1) { //модуль вектора m=sqrt(x^2+Z^2), то есть длина вектора между указателем и координатами позиции
			gunController.Aim(point);  //вызываем метод Aim и передаем ему значение point
			}
		}

		//Оружие
		if (Input.GetMouseButton(0)) {
			gunController.OnTriggerHold();   //если нажата кнопка мыши вызываем Shoot -> в скрипте gunController
		}
		if (Input.GetMouseButtonUp(0)) {
			gunController.OnTriggerRelease();   //если нажата кнопка мыши вызываем Shoot -> в скрипте gunController
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			gunController.Reload();
		}

		if (transform.position.y < -10) {
			TakeDamage(health);
		}
		Animate();
	}



	void Animate() {
		animator.SetInteger("state", (int)currentState);
	}


	void OnTriggerEnter(Collider col) //раздаем бонусы
	{
		if (col.gameObject.name == "BonusHealth(Clone)")
		{
			TakeDamage (-0.5f);
		}
		if (col.gameObject.name == "BonusGun4(Clone)")
		{
			gunController.EquipGun (3);

		}
		if (col.gameObject.name == "BonusGun3(Clone)")
		{
			gunController.EquipGun (2);

		}
		if (col.gameObject.name == "BonusGun2(Clone)")
		{
			gunController.EquipGun (1);

		}
		if (col.gameObject.name == "BonusGun1(Clone)")
		{
			gunController.EquipGun (0);
		}

	}



	public override void Die () {
		AudioManager.instance.PlaySound ("Player death", transform.position);
		base.Die ();
	}
}
