using UnityEngine;
using System.Collections;

public class FlickeringLightSource : MonoBehaviour {

	private Light l;
	private float normalIntensity;
	private bool enabled;

	// Use this for initialization
	void Start () {
		l = GetComponent<Light> ();
		normalIntensity = l.intensity;
		enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled) {
			float multiplier = 0.8f + Mathf.PingPong (Time.time, 1.0f);
			l.intensity = normalIntensity * multiplier;
		}
	}

	public void setEnabled(bool e) {
		enabled = e;
	}
}