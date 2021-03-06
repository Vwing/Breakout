﻿using UnityEngine;
using System.Collections;

public class WeirdMachine_EnergyBeam : MonoBehaviour {

	public Light nuclearLight;
	public ParticleSystem particle;
	private bool meltedDown;

	void Start() {
		meltedDown = false;
	}

	// Meltdown happens if the ball collides with the energy beam collider
	void OnTriggerEnter(Collider other) {
		if (!meltedDown && other.tag == "Ball" && !other.GetComponent<HelperBall>()) {
			// Reduce lives to zero
			GameManager.lives = 0;

			// Kill the ball
			//Destroy (other.gameObject);

			// Start meltdown
			meltedDown = true;
			particle.Stop();
			StartCoroutine("meltdown");
		}
	}
	
	// Play meltdown lighting sequence
	IEnumerator meltdown() {

		// Play meltdown sound
		GetComponent<AudioSource> ().Play ();

		// Suspend existing light animation
		if (nuclearLight.GetComponent<FlickeringLightSource> ()) {
			nuclearLight.GetComponent<FlickeringLightSource> ().setEnabled (false);
		}

		// Blow out the rooom with overpowering light source
		float startRange = nuclearLight.range;
		float startIntensity = nuclearLight.intensity;
		float t0 = Time.time;
		while (Time.time - t0 < 2f) {
			nuclearLight.range = Mathf.Lerp(startRange, 100f, (Time.time - t0) / 2);
			nuclearLight.intensity = Mathf.Lerp(startIntensity, 8f, Time.time - t0);
			yield return null;
		}
	}
}
