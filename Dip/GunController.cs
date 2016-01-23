using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Transform weaponHold; // точка появления оружия
	public Gun[] allGuns;  // префаб оружия
	Gun equippedGun;  //скрипт Gun

	void Start() {

	}

	public void EquipGun(Gun gunToEquip) {
		if (equippedGun != null) {    //????????????
			Destroy(equippedGun.gameObject);
		}
		equippedGun = Instantiate (gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;  // создаем оружие используя точку появления
		equippedGun.transform.parent = weaponHold; //????????????
	}

	public void EquipGun(int weaponIndex) {

		EquipGun(allGuns [weaponIndex]);
	}

	public void OnTriggerHold() {

		if (equippedGun != null) {
			equippedGun.OnTriggerHold();
		}
	}

	public void OnTriggerRelease() {
		if (equippedGun != null) {
			equippedGun.OnTriggerRelease();
		}
	}

	public float GunHeight {
		get {
			return weaponHold.position.y;
		}

	}

	public void Aim(Vector3 aimPoint) { 
		if (equippedGun != null) {   //проверка если префаб с оружием не пустой
			equippedGun.Aim (aimPoint); //вызываем метод Aim из скрипта Gun и передаем ему значение Point 
		}
	}

	public void Reload() {
		if (equippedGun != null) {  
			equippedGun.Reload (); 
		}
	}
}
