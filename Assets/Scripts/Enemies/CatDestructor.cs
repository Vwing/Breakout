using UnityEngine;
using System.Collections;

public class CatDestructor : MonoBehaviour
{

    public GameObject SpawnUponDestruction;
    private float timeToLive, counter;

    // Use this for initialization

    void Start()
    {
        timeToLive = 9.0f;
        counter = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > timeToLive)
        {
            GameObject.Instantiate(SpawnUponDestruction, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "Wall")
            return;

        GameObject.Instantiate(SpawnUponDestruction, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
