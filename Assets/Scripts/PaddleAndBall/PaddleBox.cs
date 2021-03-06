﻿using UnityEngine;
using System.Collections;

public class PaddleBox : UnityEngine.MonoBehaviour
{
    public Material PaddleBoxHit;
    public Material paddleBoxReg;

    void OnTriggerEnter(Collider other)
    {        
		// Only cause damage if 1) it's a ball; 2) that's in play; and 3) it's not a 'helper' ball
        Ball ball = other.GetComponent<Ball>();
		bool damaged = (other.transform.tag == "Ball" && ball && ball.isInPlay () 
			&& !other.GetComponent<HelperBall> ());

		if (damaged) {
			StartCoroutine (Damaged (other));
		}
    }

    IEnumerator Damaged(Collider ball){
        GameManager.lives -= 1;

		// Play whoosh! sound
		GetComponent<AudioSource> ().Play ();

		// Flash alert material on the paddle box

		float starttime = Time.time;

		setMaterial(PaddleBoxHit);

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}
		
		setMaterial(paddleBoxReg);
		starttime = Time.time;
		

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}

		setMaterial(PaddleBoxHit);

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}
		
		setMaterial(paddleBoxReg);
		starttime = Time.time;
		

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}
		
		setMaterial(PaddleBoxHit);
		starttime = Time.time;
		
		while (Time.time < starttime + 0.2f) {
			yield return null;
		}
		
		// Return to regular material, unless game lost
//		if (GameManager.lives > 0)
		setMaterial(paddleBoxReg);
	}

	// Sets the material on all walls of the paddle box
	void setMaterial(Material m) {
		Renderer[] renderers = GetComponentsInChildren <Renderer> ();
		foreach (Renderer r in renderers) {
			r.material = m;
		}
	}
}
