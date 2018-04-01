using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

	private Material material;
	private Color original_color;
	private float fade_percent;
	private float lifetime = 5f;
	private float deathtime;
	private bool fading;
	private Rigidbody rigidbody;

	void Start() {
		material = GetComponent<Renderer>().material;
		rigidbody = GetComponent<Rigidbody>();
		original_color = material.color;
		deathtime = Time.time + lifetime;
		StartCoroutine("FadeShell");
	}

	IEnumerator FadeShell() {
		while (true) {
			yield return new WaitForSeconds(0.2f);
			if (fading) {
				fade_percent += Time.deltaTime;
				material.color = Color.Lerp(original_color, Color.clear, fade_percent);
				if (fade_percent >= 1) {
					Destroy(gameObject);
				}
			} else if (Time.time > deathtime) {
				fading = true;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ground") {
			rigidbody.Sleep();
		}
	}
}