//Attached to the ball. Current functionality includes:
//
//1. jetisons the ball into play upon tapping.
//2. decrements lives in the game manager upon collision with paddle box bounds.

using UnityEngine;
using System.Collections;

public class Ball : UnityEngine.MonoBehaviour
{
    public float maxDistanceFromPaddle = 200f;
    Rigidbody rb;
    public float speed = 12f;
	public Material regularMaterial;
	public Material successMaterial;

    private bool ballInPlay = false;
    private Transform paddleHolder;
	private Vector3 startPosition;
	private bool triggered;
	private AudioSource aud;

    public AudioClip PowerUpRay;
    public AudioClip test; 


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        paddleHolder = transform.parent;
        startPosition = transform.localPosition;
        aud = GetComponent<AudioSource>(); //Get audio clip
    }

    bool lastTriggerState;
    void Update()
    {
		bool t = Cardboard.SDK.Triggered;
        triggered = Input.GetButtonDown("Fire1") || (t && !lastTriggerState);
        lastTriggerState = t;
        if (triggered)
            reward();
        if (!ballInPlay && triggered)
            LaunchBall();

        if (Vector3.Distance(transform.position, paddleHolder.position) > maxDistanceFromPaddle)
        {
            --GameManager.lives;
            if (GameManager.lives > 0)
                StickBall();
        }
	}

    void FixedUpdate()
    {
        if (ballInPlay)
            rb.velocity = rb.velocity.normalized * speed;
    }

    void LaunchBall()
    {
        transform.parent = null;
        rb.isKinematic = false;
        rb.velocity = transform.forward * speed;
        ballInPlay = true;
    }

    public void StickBall()
    {
		ballInPlay = false;
        transform.parent = paddleHolder;
        transform.localPosition = startPosition;
        rb.isKinematic = true;
    }

    public void MoveBall(float x, float y, float z)
    {
        Vector3 pos = new Vector3(x, y, z);
        transform.position = pos;
    }

    //Had to do this on a delay b/c Cardboard checks for triggers later than frame update.
    IEnumerator SwitchBallInPlay()
    {
        for (float i = 0f; i < 0.1f; i += Time.deltaTime)
            yield return new WaitForEndOfFrame();
        ballInPlay = !ballInPlay;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Paddle" || other.transform.tag == "Wall") //On collision with paddle or wall...
        {
            //Play collision sound
            aud.Play();
			reward (); // flash the ball
        }


        if (other.transform.tag == "ZombieCat")
        {
            Debug.Log("ball hit zombie");
            Debug.Log(other.rigidbody.velocity);
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
    float speedBoostTime = 6; 

    void OnTriggerEnter(Collider other)   //called when player obj first touches a trigger collider
    {                         //other is reference to collider we have touched
                              // Destroy(other.gameObject);   //then destroys other's game object
        if (other.gameObject.CompareTag("TestCube"))    //if collides with "pickup" collider 
        {
            other.gameObject.SetActive(false);      //then sets other object to false
                                                    //transform.localScale = new Vector3(5F, 5F, 5F); //increases XyZ axis of ball when this happens by factor of 5

            aud.PlayOneShot(PowerUpRay, 1F);
            paddle.transform.localScale = new Vector3(6F, 6F, 1F);     //increase size of paddle

            /*

                        while (speedBoostTime > 0)
                            {
                                speedBoostTime -= Time.deltaTime;
                                if (speedBoostTime <= 0) speed /= 2;

                            paddle.transform.localScale = new Vector3(2F, 2F, 1F);
                        }


                */

            Invoke("resizeIt", 10);      //runs resizeIt function after 4 seconds


        }

    }

    void resizeIt()
    {

        paddle.transform.localScale = new Vector3(3F, 3F, 1F);     //transforms paddle back to original size



    }

	public bool isInPlay() {
		return ballInPlay;
	}
}
