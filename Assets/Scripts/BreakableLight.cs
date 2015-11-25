using UnityEngine;
using System.Collections;

public class BreakableLight : MonoBehaviour {

	public Light mainLight;
	[Range (0, 1)] public float percentLightLostOnCollision;

	private bool enabled;
	private Color normalEmissionColor;
	private Renderer r;

	// Use this for initialization
	void Start () {
		enabled = true;
		r = GetComponent<Renderer> ();
		normalEmissionColor = r.material.GetColor ("_EmissionColor"); // Fetch the normal emission color from the selected material
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision other) {
		if (enabled && other.transform.tag == "Ball") {
			mainLight.intensity *= (1-percentLightLostOnCollision);
			GetComponent<AudioSource>().Play (); // Smash sound
			Color emissionColor = Color.black * Mathf.LinearToGammaSpace (0f);
			GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColor);
			enabled = false;
		}
	}

	// Brightens and dims the light intensity smoothly but unpredictably over time
	private void flicker() {
		float emission = 0.1f + 0.8f * Mathf.PerlinNoise (Time.time * 0.5f, 0f); // Range between .1 - .9
		Color emissionColor = normalEmissionColor * Mathf.LinearToGammaSpace (emission);
		r.material.SetColor ("_EmissionColor", emissionColor);
	}
}
