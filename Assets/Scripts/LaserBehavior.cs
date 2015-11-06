using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour {
    public float speed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "brick")
            Destroy(other.gameObject);

    }
}
