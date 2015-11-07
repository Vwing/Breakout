//Attached to the ball. Current functionality includes:
//
//1. jetisons the ball into play upon tapping.
//2. decrements lives in the game manager upon collision with paddle box bounds.

using UnityEngine;
using System.Collections;

public class Ball : UnityEngine.MonoBehaviour
{
    Rigidbody rb;
    public float StartForce = 600f;
	public Material regularMaterial;
	public Material successMaterial;

    bool ballInPlay = false;
    Transform paddle;
    bool triggered;
    AudioSource audio;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        paddle = transform.parent;
        audio = GetComponent<AudioSource>(); //Get audio clip
    }

    void Update()
    {
        triggered = Input.GetButton("Fire1") || Cardboard.SDK.Triggered;
        bool released = Input.GetButtonUp("Fire1") || !Cardboard.SDK.Triggered;
        if (!ballInPlay && (triggered || released))
            LaunchBall();
	
	}

    void LaunchBall()
    {
        ballInPlay = true;
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddRelativeForce(0, 0, StartForce);
    }

    void StickBall()
    {
        ballInPlay = false;
        transform.parent = paddle;
        rb.isKinematic = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Paddle" || other.transform.tag == "Wall") //On collision with paddle or wall...
        {
            //Play collision sound
            audio.Play();
			reward (); // flash the ball
        }

		if (other.transform.tag == "Paddle") {
			PaddleAnimator pa = other.gameObject.GetComponent<PaddleAnimator>();
			pa.reward(); // Flash the paddle

		}

        if (other.transform.tag == "Paddle" && triggered)
            StickBall();
    }	


	public void reward() {
		StartCoroutine ("FlashBallReward");
	}
	
	// Flashes the ball to a "success" material (brighter color, etc.) and
	// gradually fades back
	IEnumerator FlashBallReward() {
		Renderer r = GetComponent<Renderer> ();
		
		float startTime = Time.time;
		float smooth = 0f;
		
		while (smooth < 1.0f) {
			float t = (Time.time - startTime) / 0.333f; 
			smooth = Mathf.SmoothStep(0f, 1.0f, t);
			r.material.Lerp (successMaterial, regularMaterial, smooth);
			yield return null;	
		}
	}

}
