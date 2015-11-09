using UnityEngine;
using System.Collections;

public class LifeIndicator : MonoBehaviour {

	public GameObject light1;
	public GameObject light2;
	public GameObject light3;
	public GameObject light4;
	public GameObject light5;
	public GameObject light6;
	public GameObject light7;

	private GameObject[] lights;
	private int numLightsOn;
	
	void Start () {

		// Setup lights array
		lights = new GameObject[7];
		lights [0] = light1;
		lights [1] = light2;
		lights [2] = light3;
		lights [3] = light4;
		lights [4] = light5;
		lights [5] = light6;
		lights [6] = light7;
	}
	
	// Update is called once per frame
	void Update () {
		if (numLightsOn != GameManager.lives) {
			setLights (GameManager.lives);
		}
	}
	
	// Activates the given number of lights
	private void setLights(int n) {
		for (int i = 0; i < 7; i++) {
			if (i < n)
				lights[i].SetActive (true);
			else
				lights[i].SetActive (false);
		}
		numLightsOn = n;
	}
}

