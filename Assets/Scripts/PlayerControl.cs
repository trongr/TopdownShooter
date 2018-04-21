using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour {

	public float rotation_speed = 10f;
	public float walk_speed = 5f;
	public float run_speed = 8f;
	public float acceleration = 5f; // This doesn't seem to do anything.

	public Gun gun;

	private Quaternion target_rotation;
	private Vector3 currentVelocityMod;
	private CharacterController control;

	void Start() {
		control = GetComponent<CharacterController>();
	}

	void Update() {
		// ControlWASD();
		ControlMouse();
		if (Input.GetButtonDown("Shoot")) {
			gun.Shoot();
		} else if (Input.GetButton("Shoot")) {
			gun.ShootContinuous();
		}
	}

	void move_player(Vector3 input) {
		// currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
		// Vector3 motion = currentVelocityMod;
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1 ? 0.707f : 1f);
		motion *= (Input.GetButton("Run") ? run_speed : walk_speed);
		motion += Vector3.up * -8;
		control.Move(motion * Time.deltaTime);
	}

	void ControlMouse() {
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Plane playerPlane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitdist = 0.0f;
		if (playerPlane.Raycast(ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint(hitdist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotation_speed * Time.deltaTime);
		}
		move_player(input);
	}

	void ControlWASD() {
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		if (input != Vector3.zero) {
			target_rotation = Quaternion.LookRotation(input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, target_rotation.eulerAngles.y, rotation_speed * Time.deltaTime);
		}
		move_player(input);
	}
}