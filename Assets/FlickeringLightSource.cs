using UnityEngine;
using System.Collections;

public class FlickeringLightSource : MonoBehaviour {

	private Light l;
	private float normalIntensity;

	// Use this for initialization
	void Start () {
		l = GetComponent<Light> ();
		normalIntensity = l.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		float multiplier = 0.8f + Mathf.PingPong (Time.time, 1.0f);
		l.intensity = normalIntensity * multiplier;
	}
}
