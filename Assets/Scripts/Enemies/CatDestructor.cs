using UnityEngine;
using System.Collections;

public class CatDestructor : MonoBehaviour {

    public GameObject SpawnUponDestruction;

    // Use this for initialization

	void Start () {
	}
	/*
	// Update is called once per frame
	void Update () {
	
	}
    */
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Down")
        {
            GameObject.Instantiate(SpawnUponDestruction, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
