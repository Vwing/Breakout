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
        }


        if (other.transform.tag == "Paddle" && triggered)
            StickBall();
    }
}
