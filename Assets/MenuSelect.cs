using UnityEngine;
using System.Collections;

public class MenuSelect : MonoBehaviour
{
    public Transform hitThing;
    private Transform cam;
	// Use this for initialization
	void Start ()
	{
	    cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    RaycastHit hit;
	    bool hitSomething = Physics.Raycast(Vector3.zero, cam.forward, out hit, 100f);

        if (hitSomething)
        {
            hitThing = hit.transform;
            //hit.transform.GetChild(0).gameObject.SetActive(true);
        }
	}
}
