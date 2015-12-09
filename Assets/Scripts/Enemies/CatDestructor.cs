using UnityEngine;
using System.Collections;

public class CatDestructor : MonoBehaviour
{

    public GameObject SpawnUponDestruction;
    public bool launched = false;
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
        if (!launched)
            return;
        transform.GetChild(0).GetComponent<ChooseRandSprite>().TurnTowardsPlayer();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PaddleBox")
            return;
        GameManager.lives--;
        GameObject.Instantiate(SpawnUponDestruction, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
