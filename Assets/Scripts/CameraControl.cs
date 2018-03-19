using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private Transform player;
	private Vector3 offset;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;	
		offset = transform.position - player.transform.position;
	}
	
	void Update () {
		Vector3 target = player.position + offset;
		transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime * 8);
	}
}
