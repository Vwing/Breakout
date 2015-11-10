using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {

    private Vector3 testDest;
    private float destX, destY, destZ;
    private float compound;
    private float frames;

    // Use this for initialization
    void Start() {
        //Disable all relevant GameObjects first before..

        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            //if (!go.Equals(transform.gameObject) && (!go.Equals(transform.Find("Main Camera").gameObject)))

            if ((go.tag == "ZombieCat") || (go.tag == "Brick") || (go.tag == "Ball") || (go.tag == "Paddle"))
                go.SetActive(false);
        }
        //GameObject.Find("Player").SetActive(true);
        //GameObject.Find("Player").transform.FindChild("Main Camera").gameObject.SetActive(true);
        testDest = new Vector3(22, -12, 28);
        destX = 22;
        destY = -12;
        destZ = 28;
        testDest = testDest - GameObject.Find("Player").transform.position;
        testDest.Normalize();
        compound = 1.03f;
        frames = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if ((transform.position.x > 21.5) && (transform.position.x < 22.5) && (transform.position.y > -12.5) && (transform.position.y < -11.5) && (transform.position.z > 27.5) && (transform.position.z < 28.5))
        {
            GetComponent<LevelTransition>().enabled = false;
            return;
        }

        if (frames > 13)
        {
            testDest = testDest * compound;
        }

        transform.Translate(testDest * Time.deltaTime);
        ++frames;
        frames = Mathf.Repeat(frames, 15);
	}
}
