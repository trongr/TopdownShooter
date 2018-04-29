using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public float health = 100f;

	public virtual void TakeDamage(float dmg) {
		health -= dmg;
		if (health < 0) Die();
	}

	public virtual void Die() {
		Destroy(gameObject);
	}
}