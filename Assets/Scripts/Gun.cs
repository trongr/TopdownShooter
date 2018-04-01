using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Transform spawn;
	public enum GunType { Semi, Burst, Auto }
	public GunType gun_type;
	public float rpm;
	public AudioSource gun_sound;
	public Transform shell_out;
	public Rigidbody shell;

	private float seconds_between_shots;
	private float next_possible_shoot_time;
	private LineRenderer tracer;

	void Start() {
		seconds_between_shots = 60 / rpm;
		gun_sound = GetComponent<AudioSource>();
		if (GetComponent<LineRenderer>()) {
			tracer = GetComponent<LineRenderer>();
		}
	}

	public void Shoot() {
		if (CanShoot()) {
			Ray ray = new Ray(spawn.position, spawn.forward);
			RaycastHit hit;
			float shot_distance = 20;
			if (Physics.Raycast(ray, out hit, shot_distance)) {
				shot_distance = hit.distance;
			}
			next_possible_shoot_time = Time.time + seconds_between_shots;
			gun_sound.Play();

			if (tracer) StartCoroutine("RenderTracer", ray.direction * shot_distance);

			Rigidbody nshell = Instantiate(shell, shell_out.position, Quaternion.identity) as Rigidbody;
			nshell.AddForce(shell_out.forward * Random.Range(150f, 200f) + spawn.forward * Random.Range(-10f, 10f));
		}
	}

	public void ShootContinuous() {
		if (gun_type == GunType.Auto) {
			Shoot();
		} // TODO shoot burst
	}

	private bool CanShoot() {
		bool can_shoot = true;
		if (Time.time < next_possible_shoot_time) {
			can_shoot = false;
		}
		return can_shoot;
	}

	IEnumerator RenderTracer(Vector3 hitpoint) {
		tracer.enabled = true;
		tracer.SetPosition(0, spawn.position);
		tracer.SetPosition(1, spawn.position + hitpoint);
		yield return null;
		tracer.enabled = false;
	}
}