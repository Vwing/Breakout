using UnityEngine;
using System.Collections;

public class BallMultiplier : MonoBehaviour {

	public Transform helperBallPrefab;
	public int newBallsPerHit;

	void Update() {
		transform.Rotate (new Vector3 (0f, Time.deltaTime * 60f, 0f), Space.World);
	}

	void OnCollisionEnter(Collision c) {

        Debug.Log("Collision with BallMultiplier");
		// If we collide with a Ball, and it's not a helper ball, generate some helpers
		if (c.collider.transform.tag == "Ball" && c.collider.GetComponent<HelperBall>() == null) {
			// Show 'reward' for hit
            StartCoroutine("FlashReward");

			Vector3 pos = c.transform.position;
			Ball ball = c.collider.GetComponent<Ball>();

			// Instantiate and launch the new 'helper' balls
			Transform t; 
			HelperBall h;
			for (int i = 0; i < newBallsPerHit; i++) {
				// Instantiate
				t = (Transform) Instantiate (helperBallPrefab, pos, Quaternion.identity);
				h = t.GetComponent<HelperBall>();
				h.speed = ball.speed;
				t.localScale = ball.transform.localScale;

				// Perturb the new ball's rotation slightly, so it goes in a different direction
				float maxDegrees = 30f;
				Vector3 perturb = new Vector3(Random.Range (-maxDegrees, maxDegrees), 
				                              Random.Range (-maxDegrees, maxDegrees),
				                              Random.Range (-maxDegrees, maxDegrees));
				t.Rotate (perturb);

				// Launch the ball
				h.LaunchBall();

                //Destroy this powerup
                Destroy(gameObject);
			}
		}
	}

	IEnumerator FlashReward() {
		Renderer r = GetComponent<Renderer> ();		
		Vector3 origSize = transform.localScale;
		Vector3 bigSize = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
		float startTime = Time.time;
		float smooth = 0f;
		
		while (smooth <= 1.0f) {
			float t = (Time.time - startTime) / 0.333f; 
			smooth = Mathf.SmoothStep(0f, 1.0f, t);
			transform.localScale = Vector3.Lerp (bigSize, origSize, smooth);
			yield return null;	
		}
	}
}
