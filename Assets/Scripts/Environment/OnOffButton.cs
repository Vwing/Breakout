using UnityEngine;
using System.Collections;

public class OnOffButton : MonoBehaviour {

	public GameObject[] controlledObjects;
	public Material onMaterial;
	public Material offMaterial;
	public AudioClip onSound;
	public AudioClip offSound;

	private bool isOn;

	// Use this for initialization
	void Start () {
		isOn = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If the ball hits this computer, we'll shut the computer screens off
	// and set every GameObject in controlledObjects[] inactive
	// (useful for shutting off force fields, enemies, etc.)
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.transform.tag != "Ball")
			return;

		// If on, toggle off:
		if (isOn) {
			isOn = false;
			GetComponent<Renderer> ().material = offMaterial;
			GetComponent<AudioSource>().PlayOneShot(offSound);
			foreach (GameObject g in controlledObjects) {
				g.SetActive (false);
			}
		} else {

		// Toggle back on:
			isOn = true;
			GetComponent<Renderer>().material = onMaterial;
			GetComponent<AudioSource>().PlayOneShot(onSound);
			foreach (GameObject g in controlledObjects) {
				g.SetActive (true);
			}
		}
	}                         
}
