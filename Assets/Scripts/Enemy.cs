using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	public float expOnDeath;
	private Player player;

	void Start() {
		expOnDeath = 60;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	public override void Die() {
		player.AddExperience(expOnDeath);
		base.Die();
	}
}