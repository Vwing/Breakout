using UnityEngine;
using System.Collections;

public class laserGun : MonoBehaviour {
    public GameObject laserBeam;
    public float shootSpeed = 0.5f;
    private float timer;
    // Use this for initialization
    private float timer2 = 10f; 

	void Start () {
        timer = shootSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        timer2 -= Time.deltaTime;    //counts down from 10 seconds

        if (timer <= 0)
        {
            GameObject.Instantiate(laserBeam, transform.position, transform.rotation);
            timer = shootSpeed;
        }

        if (timer2 <= 0)
            this.enabled = false;
	}
}
