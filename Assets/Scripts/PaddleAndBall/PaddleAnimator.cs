using UnityEngine;
using System.Collections;

public class PaddleAnimator : MonoBehaviour {

	public Material successMaterial;
	public Material regularMaterial;

	void Start () {
	}

	public void reward() {
		StartCoroutine ("FlashPaddleReward");
	}

	// Flashes the paddle to a "success" material (brighter color, etc.) and
	// gradually fades back
	IEnumerator FlashPaddleReward() {
		Renderer r = GetComponent<Renderer> ();
	
		Vector3 origSize = transform.localScale;
		Vector3 bigSize = transform.localScale + new Vector3(0.1f, 0.1f, 0f);
		float startTime = Time.time;
		float smooth = 0f;

		while (smooth <= 1.0f) {
			float t = (Time.time - startTime) / 0.333f; 
			smooth = Mathf.SmoothStep(0f, 1.0f, t);
			r.material.Lerp (successMaterial, regularMaterial, smooth);
			transform.localScale = Vector3.Lerp (bigSize, origSize, smooth);
			yield return null;	
		}
	}
}
