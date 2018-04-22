﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public float health = 100f;

	public virtual void TakeDamage(float dmg) {
		health -= dmg;
		Debug.Log(health);
		if (health < 0) {
			Die();
		}
	}

	public virtual void Die() {
		Debug.Log("You died!");
		Destroy(gameObject);
	}
}