using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public GameObject SpawnUponDestruction, Shrap;

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "Ball" && other.transform.tag != "Shrapnel")
            return;


        GameObject.Instantiate(SpawnUponDestruction, transform.position, transform.rotation);
        GameObject temp;
        for (int i = 0; i < 64; ++i)
        {
            temp = GameObject.Instantiate(Shrap, transform.position + Random.insideUnitSphere * 0.5f, Random.rotationUniform) as GameObject;
            //temp.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
            temp.GetComponent<Rigidbody>().velocity = Random.onUnitSphere * Random.Range(10f, 20f);
            temp.GetComponent<MeshRenderer>().enabled = false;
        }
        if (tag == "Brick")
            GameManager.bricks--;
        Destroy(gameObject);
    }
}
