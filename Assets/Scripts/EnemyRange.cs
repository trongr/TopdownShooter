using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour {

	public float followPlayerSpeed = 1f;
	private Transform playerTransform;

	void Update() {
		FollowPlayer();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			playerTransform = other.transform;
		}
	}

	private void FollowPlayer() {
		if (playerTransform != null) {
			transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, followPlayerSpeed * Time.deltaTime);
		}
	}

}