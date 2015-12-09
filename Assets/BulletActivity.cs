using UnityEngine;
using System.Collections;

public class BulletActivity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnCollisionEnter(Collision other)
    {
        this.gameObject.SetActive(false);
    }
}
