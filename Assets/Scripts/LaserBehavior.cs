using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour {
    public float speed = 2f;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody>();
        //rb.velocity = Vector3.right * 1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);


    }

    void OnCollisionEnter(Collision other)
    {
    }
}
