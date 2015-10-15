//Attached to the ball. Current functionality includes:
//
//1. jetisons the ball into play upon tapping.
//2. decrements lives in the game manager upon collision with paddle box bounds.

using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float StartForce = 600f;
    bool ballInPlay = false;
	void Awake () 
	{
        rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		if(!ballInPlay && (Input.GetButtonDown("Fire1") || Cardboard.SDK.Triggered))
        {
            ballInPlay = true;
            transform.parent = null;
            rb.isKinematic = false;
            rb.AddRelativeForce(0, 0, StartForce);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PaddleBox")
        {
            GameManager.lives -= 1;
        }
    }
}
