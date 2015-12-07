using UnityEngine;
using System.Collections;

public class ShrapnelDestructor : MonoBehaviour {

    private float timeToLive, counter;

    // Use this for initialization

    void Start()
    {
        timeToLive = 0.3f;
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        counter += Time.deltaTime;
        if (counter > timeToLive)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Brick")
        {
            Destroy(gameObject);
        }
    }
}

