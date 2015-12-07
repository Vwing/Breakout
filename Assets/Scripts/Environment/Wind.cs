using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour
{
    private float cooldown, counter;
    private GameObject bricks, bombs, enemies, powerups;
    private Vector3 windvec;
    public AudioSource audio;

    // Use this for initialization
    void Start()
    {
        cooldown = 120f;
        counter = 0.0f;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > cooldown)
        {
            counter = 0.0f;
            cooldown = Random.Range(15f, 30f);

            windvec = Random.onUnitSphere;

            bricks = GameObject.Find("Bricks");

            foreach (Transform child in bricks.transform)
            {
                child.gameObject.GetComponent<Rigidbody>().AddForce(windvec * Random.Range(0f, 100f));
                child.gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
            }

            bombs = GameObject.Find("Bombs");

            foreach (Transform child in bombs.transform)
            {
                child.gameObject.GetComponent<Rigidbody>().AddForce(windvec * Random.Range(0f, 100f));
                child.gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
            }

            enemies = GameObject.Find("Enemies");

            foreach (Transform child in enemies.transform)
            {
                child.gameObject.GetComponent<Rigidbody>().AddForce(windvec * Random.Range(0f, 100f));
                child.gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
            }

            powerups = GameObject.Find("Powerups");

            foreach (Transform child in powerups.transform)
            {
                child.gameObject.GetComponent<Rigidbody>().AddForce(windvec * Random.Range(0f, 100f));
                child.gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
            }

            audio.Play();
            //AudioSource.PlayClipAtPoint(clip, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
        }
    }
}
