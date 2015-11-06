using UnityEngine;
using System.Collections;

public class PaddleAlert : MonoBehaviour {

	public Material alertMaterial;
	private Material regularMaterial;

	void Start () {
		regularMaterial = GetComponent<Material> ();	
	}

	public void alert() {
		StartCoroutine ("FlashAlert");
	}

	// Flashes the paddle to an alert material twice, and leaves 
	// the alert material permanently if game lost
	IEnumerator FlashAlert() {
		float starttime = Time.time;
		Renderer r = GetComponent<Renderer> ();
	
		r.material = alertMaterial;

		// Wait 1 sec
		while (Time.time < starttime + 1.0f) {
			yield return null;
		}

		r.material = regularMaterial;
		starttime = Time.time;

		// Wait 0.5 sec
		while (Time.time < starttime + 0.5f) {
			yield return null;
		}

		r.material = alertMaterial;
		starttime = Time.time;

		// Wait 1 sec
		while (Time.time < starttime + 1.0f) {
			yield return null;
		}

		// Return to regular material, unless game lost
		if (GameManager.lives > 0) {
			r.material = regularMaterial;
		}
	}
}
