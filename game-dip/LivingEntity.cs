using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable {

	public float startingHealth;   //стартовое значение здоровья
	public float health {get; protected set;}       //здоровье
	protected bool dead;           // умер/жив

	public event System.Action OnDeath;

	protected virtual void Start() {
		health = startingHealth;        //на старте здоровье равно стартовому
	}

	public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection) {
		//do some stuff here with hit var
		TakeDamage (damage);
	}

	public virtual void TakeDamage (float damage) {
		health -= damage;
		
		if (health <= 0 && !dead) {
			Die();
		}

	}

	[ContextMenu("Self Distruct")] // появилось свойство у плеера для вызова через инспектор
	public virtual void Die() {               //вызывается при смерти врага
		dead = true;
		if (OnDeath != null) {           //проверка всех ли убилиы
			OnDeath();
		}
		GameObject.Destroy (gameObject);


	}
}
