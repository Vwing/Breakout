using UnityEngine;
using System.Collections;

public class CeilingLightScript : MonoBehaviour {

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
		if (enabled) {
			float emission = 0.1f + 0.8f * Mathf.PerlinNoise (Time.time * 0.5f, 0f); // Range between .1 - .9
			Color emissionColor = normalEmissionColor * Mathf.LinearToGammaSpace (emission);
			r.material.SetColor ("_EmissionColor", emissionColor);
		}
	}

	void onCollisionEnter(Collision other) {
		if (other.gameObject.name.Equals ("Ball")) {
			Color emissionColor = Color.black * Mathf.LinearToGammaSpace (0f);
			GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColor);
			enabled = false;
		}
	}
}
