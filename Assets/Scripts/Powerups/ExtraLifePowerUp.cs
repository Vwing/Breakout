using UnityEngine;
using System.Collections;

public class ExtraLifePowerUp : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ball") {

			// Increment lives
			if (GameManager.lives < 7) {
				GameManager.lives++;
			}

			// Play sound and go away
			if (GetComponent<AudioSource>()) 
				GetComponent<AudioSource>().Play ();

			// Disable renderer and collider now...
			GetComponent<Renderer>().enabled = false;
			GetComponent<BoxCollider>().enabled = false;

			// Destroy object in 5 seconds (after audio is done playing)
			Destroy (gameObject, 5f);
		}
	}
}


