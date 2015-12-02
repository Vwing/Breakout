using UnityEngine;
using System.Collections;

public class ComputerTerminal : MonoBehaviour {

	public GameObject[] controlledObjects;
	private bool active;

	// Use this for initialization
	void Start () {
		active = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If the ball hits this computer, we'll shut the computer screens off
	// and set every GameObject in controlledObjects[] inactive
	// (useful for shutting off force fields, enemies, etc.)
	void OnCollisionEnter(Collision collision) {
		if (active && collision.collider.transform.tag == "Ball") {
			active = false;
			// Shut the computer screens off
			GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0,0,0,0) );
			// Play the power off noise
			GetComponent<AudioSource>().Play();
			// Disable controlled GameObjects
			foreach (GameObject g in controlledObjects) {
				g.SetActive(false);
			}
		}
	}
	                         
}
