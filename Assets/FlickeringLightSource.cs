using UnityEngine;
using System.Collections;

public class FlickeringLightSource : MonoBehaviour {

	private Light l;
	private float normalRange;

	// Use this for initialization
	void Start () {
		l = GetComponent<Light> ();
		normalRange = l.range;
	}
	
	// Update is called once per frame
	void Update () {
		float multiplier = 0.5f + 0.5f * Mathf.PerlinNoise (Time.time, 0f); // Range between .8 - 1.1
		l.range = normalRange * multiplier;
	}
}
