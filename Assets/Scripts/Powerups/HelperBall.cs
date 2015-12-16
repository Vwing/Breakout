using UnityEngine;
using System.Collections;

public class HelperBall : UnityEngine.MonoBehaviour
{
    public float maxDistanceFromPaddle = 200f;
    private Rigidbody rb;
    public float speed = 12f;
    public float TimeTilDestructible = 10f;
	public Material regularMaterial;
	public Material successMaterial;
    float timer = 0f;

	private Vector3 startPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
	    rb.velocity = rb.velocity.normalized * speed;
        timer += Time.fixedDeltaTime;
    }

    public void LaunchBall()
    {
        rb.isKinematic = false;
        rb.velocity = transform.forward * speed;
    }

  
    void OnCollisionEnter(Collision other)
    {

		if (other.transform.tag == "Wall" || other.transform.tag == "Paddle") //On collision with paddle or wall...
        {
			reward (); // flash the ball
        }

        else if (other.transform.tag == "ZombieCat")
        {
            Debug.Log("ball hit zombie");
            Debug.Log(other.rigidbody.velocity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PaddleBox" && timer > TimeTilDestructible)
        {
            Destroy(this.gameObject);
        }
    }

	public void reward() 
	{
		StartCoroutine ("FlashBallReward");
	}
	
	// Flashes the ball to a "success" material (brighter color, etc.) and
	// gradually fades back
	IEnumerator FlashBallReward() 
	{
		Renderer r = GetComponent<Renderer> ();
		
		float startTime = Time.time;
		float smooth = 0f;
		
		while (smooth < 1.0f) 
		{
			float t = (Time.time - startTime) / 0.333f; 
			smooth = Mathf.SmoothStep(0f, 1.0f, t);
			r.material.Lerp (successMaterial, regularMaterial, smooth);
			yield return null;	
		}
	} 
}
