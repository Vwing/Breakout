//Attached to the ball. Current functionality includes:
//
//1. jetisons the ball into play upon tapping.
//2. decrements lives in the game manager upon collision with paddle box bounds.

using UnityEngine;
using System.Collections;

public class Ball : UnityEngine.MonoBehaviour
{
    Rigidbody rb;
    public float speed = 12f;
    public Material regularMaterial;
    public Material successMaterial;

    private bool ballInPlay = false;
    private Transform paddleHolder;
    private Vector3 startPosition;
    private bool triggered;
    private AudioSource aud;

    private Vector3 originalPaddleScale;
    private GameObject paddle;

    public AudioClip PowerUpRay;
    public AudioClip test;

    public AudioClip gunshot;
    [HideInInspector]
    public Quaternion arrowRot;
   

    bool usingCardboard = false;

    public ShootDemo _ShootDemo;
    public int GunPowerup = 0;
    public float timer;

    public int counter = 0;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        paddleHolder = transform.parent;
        startPosition = transform.localPosition;
        paddle = GameObject.FindGameObjectWithTag("Paddle");
        originalPaddleScale = paddle.transform.localScale;

        aud = GetComponent<AudioSource>(); //Get audio clip
        usingCardboard = Cardboard.SDK;
    }

    bool lastTriggerState;
    float exitTimer = 0;
    void Update()
    {
        bool t = false;
        if (usingCardboard)
            t = Cardboard.SDK.Triggered;

        //Load menu if holding down trigger for 3 seconds
        if (t && lastTriggerState)
            exitTimer += Time.deltaTime;
        else
            exitTimer = 0;
        if (exitTimer > 3f)
            Application.LoadLevel("Menu");

        triggered = Input.GetButtonDown("Fire1") || (t && !lastTriggerState);

        lastTriggerState = t;
        if (triggered)
            reward();
        if (!ballInPlay && triggered)
            LaunchBall();
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
        transform.position = new Vector3(x, y, z);
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
            reward(); // flash the ball
        }

        if (other.transform.tag == "Paddle")
        {
            PaddleAnimator pa = other.gameObject.GetComponent<PaddleAnimator>();
            pa.reward(); // Flash the paddle
        }
    }


    public void reward()
    {
        StartCoroutine("FlashBallReward");
    }

    // Flashes the ball to a "success" material (brighter color, etc.) and
    // gradually fades back
    IEnumerator FlashBallReward()
    {
        Renderer r = GetComponent<Renderer>();

        float startTime = Time.time;
        float smooth = 0f;

        while (smooth < 1.0f)
        {
            float t = (Time.time - startTime) / 0.333f;
            smooth = Mathf.SmoothStep(0f, 1.0f, t);
            r.material.Lerp(successMaterial, regularMaterial, smooth);
            yield return null;
        }
    }
    //   float speedBoostTime = 6; 

    void OnTriggerEnter(Collider other)   //called when player obj first touches a trigger collider
    {                         //other is reference to collider we have touched
                              // Destroy(other.gameObject);   //then destroys other's game object
        if (other.tag == "TestCube")    //if collides with "pickup" collider 
        {
            other.gameObject.SetActive(false);      //then sets other object to false
                                                    //transform.localScale = new Vector3(5F, 5F, 5F); //increases XyZ axis of ball when this happens by factor of 5

            aud.PlayOneShot(PowerUpRay, 1F);
            paddle.transform.localScale = originalPaddleScale * 2f;     //increase size of paddle

            //runs resizeIt function after 4 seconds
            /*

                        while (speedBoostTime > 0)
                            {
                                speedBoostTime -= Time.deltaTime;
                                if (speedBoostTime <= 0) speed /= 2;

                            paddle.transform.localScale = new Vector3(2F, 2F, 1F);
                        }


                */
            Invoke("resizeIt", 4);
        }

        if (other.tag == "GunPowerup")
        {
            GunPowerup = 1;
            aud.PlayOneShot(gunshot, 1F);
            other.gameObject.SetActive(false);

        }
    }

    void resizeIt()
    {
        paddle.transform.localScale = new Vector3(0.5F, 0.5F, 1F);
        paddle.transform.localScale = originalPaddleScale;     //transforms paddle back to original size
                                                               // paddle.transform.localScale = originalPaddleScale * 4f;
    }

    public bool isInPlay()
    {
        return ballInPlay;
    }

}