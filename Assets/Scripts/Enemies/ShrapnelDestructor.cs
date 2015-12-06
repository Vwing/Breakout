using UnityEngine;
using System.Collections;

public class ShrapnelDestructor : MonoBehaviour {

    private float timeToLive, counter;

    // Use this for initialization

    void Start()
    {
        timeToLive = 0.2f;
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
}

