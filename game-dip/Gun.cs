﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public enum FireMode {Auto, Burst, Single};  //auto постоянно стреляем burst по выставленному коичеству single один раз
	public FireMode fireMode;

	public Transform[] projectileSpawn;  //точка появления пули массив
	public Projectile projectile;   //пуля
	public float msBetweenShots = 100;  //время между выстрелами
	public float muzzleVelocity = 35; // скорость пули
	public int bustCount;
	public int projectilesPerMag;
	public float reloadTime = .3f;

	[Header("Recoil")]
	public Vector2 kickMinMax = new Vector2 (.05f, 0.2f);
	public Vector2 recoilAngleMinMax = new Vector2 (20,30);
	public float recoilMoveSettleTime= 0.1f;
	public float recoilRotationSettleTime = 0.1f;

	[Header("Effects")]
	public Transform shell;
	public Transform shellEjection;
	public AudioClip shootAudio;
	public AudioClip reloadAudio;
	MussleFlash musselflash;
	float nextShotTime;

	bool triggerReleasedSinceLastShot;
	int shotRemainingInBurst;
	int projectilesRemainingInMag;
	bool isReloading;


	Vector3 recoilSmoothDampVelocity;
	float recoilRotSmoothDampVelocity;
	float recoilAngle;

	void Start() {
		musselflash = GetComponent<MussleFlash> ();
		shotRemainingInBurst = bustCount;
		projectilesRemainingInMag = projectilesPerMag;
	}

	void LateUpdate() {
		//анимация отдачи оружия при выстреле, оружие изменит координаты из точки А (localPos) в точку Б (Vector3.zero) за одну десятую секунды. 
		transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero,ref recoilSmoothDampVelocity, recoilMoveSettleTime);  //
		recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmoothDampVelocity, recoilRotationSettleTime);
		transform.localEulerAngles = transform.localEulerAngles + Vector3.left * recoilAngle;

		if (!isReloading && projectilesRemainingInMag == 0) {
			Reload();
		}
	}

	void Shoot() {

		if (!isReloading && Time.time > nextShotTime && projectilesRemainingInMag > 0) {

			if (fireMode == FireMode.Burst) {       //проверяем условек если стреляем Burst
				if (shotRemainingInBurst == 0) {
					return;
				}
				shotRemainingInBurst --;
			}
			else if (fireMode == FireMode.Single) {  //проверяем условек если стреляем Single
				if (!triggerReleasedSinceLastShot) {
					return;
				}
			}

			for (int i = 0; i < projectileSpawn.Length; i++) {
				if (projectilesRemainingInMag == 0) {
					break;
				}
				projectilesRemainingInMag --;
				nextShotTime = Time.time + msBetweenShots /1000;  // время выстрела пули
				Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile; //создаем пулю
				newProjectile.SetSpeed (muzzleVelocity);   // задаем скорость пули
			}

			Instantiate(shell,shellEjection.position, shellEjection.rotation);  //создаем гильзу
			musselflash.Activate(); //активируем вспышку при выстреле
			transform.localPosition -= Vector3.forward * Random.Range(kickMinMax.x,kickMinMax.y); //изменение координат при выстреле по Z
			recoilAngle += Random.Range(recoilAngleMinMax.x, recoilAngleMinMax.y);
			recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);

			AudioManager.instance.PlaySound (shootAudio, transform.position);

		}
	}

	public void Reload() {
		if (!isReloading && projectilesRemainingInMag != projectilesPerMag) {
		StartCoroutine (AnimateReload());
			AudioManager.instance.PlaySound (reloadAudio, transform.position);

		}
	}

	IEnumerator AnimateReload() {
		isReloading = true;
		yield return new WaitForSeconds(0.2f);

		float reloadSpeed = 1f / reloadTime;
		float percent = 0;
		Vector3 initialRot = transform.localEulerAngles;
		float maxReloadAngle = 30;

		while (percent < 1) {
			percent += Time.deltaTime * reloadSpeed;
			float interpolation = (-Mathf.Pow(percent,2) + percent)*4; 
			float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);
			transform.localEulerAngles = initialRot + Vector3.left * reloadAngle;

			yield return null;

		}

		isReloading = false;
		projectilesRemainingInMag = projectilesPerMag;

	}

		public void Aim(Vector3 aimPoint) {
		if (!isReloading) {
		transform.LookAt(aimPoint);//изменяем координаты aimPoint
		}
		}


		public void OnTriggerHold() {
			Shoot ();
			triggerReleasedSinceLastShot = false;
		}

		public void OnTriggerRelease() {
			triggerReleasedSinceLastShot = true;
			shotRemainingInBurst = bustCount;

		}

		
	}

