using UnityEngine;
using System.Collections;

public class laserGun : MonoBehaviour {
    public GameObject laserBeam;
    public float shootSpeed = 0.5f;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = shootSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject.Instantiate(laserBeam, transform.position, transform.rotation);
            timer = shootSpeed;
        }
	}
}
