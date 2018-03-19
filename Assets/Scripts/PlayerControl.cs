using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerControl : MonoBehaviour {

	public float rotation_speed = 1f;
	public float walk_speed = 5f;
	public float run_speed = 8f;

	private Quaternion target_rotation;
	private CharacterController control;

	void Start () {
		control = GetComponent<CharacterController> ();
	}

	void Update () {
		ControlMouse ();
	}

	void move_player (Vector3 input) {
		Vector3 motion = input;
		motion *= (Mathf.Abs (input.x)== 1 && Mathf.Abs (input.z)== 1?.707f : 1f);
		motion *= (Input.GetButton ("Run")? run_speed : walk_speed);
		motion += Vector3.up * -8;
		control.Move (motion * Time.deltaTime);
	}

	void ControlMouse () {
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		Plane playerPlane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0.0f;
		if (playerPlane.Raycast (ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint (hitdist);
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotation_speed * Time.deltaTime);
		}

		move_player (input);
	}

	void ControlWASD () {
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		if (input != Vector3.zero) {
			target_rotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, target_rotation.eulerAngles.y, rotation_speed * Time.deltaTime);
		}

		move_player (input);
	}
}