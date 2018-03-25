using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Transform spawn;
	public enum GunType { Semi, Burst, Auto }
	public GunType gun_type;

	public void Shoot() {
		Ray ray = new Ray(spawn.position, spawn.forward);
		RaycastHit hit;
		float shot_distance = 20;
		if (Physics.Raycast(ray, out hit, shot_distance)) {
			shot_distance = hit.distance;
		}
		Debug.DrawRay(ray.origin, ray.direction * shot_distance, Color.red, 1);
	}

	public void ShootContinuous() {
		if (gun_type == GunType.Auto) {
			Shoot();
		} // TODO shoot burst
	}
}