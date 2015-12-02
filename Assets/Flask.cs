using UnityEngine;
using System.Collections;

public class Flask : MonoBehaviour {

	private Vector3 initPosition;

	// Use this for initialization
	void Start () {
		initPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, 60 * Time.deltaTime);
		transform.position = initPosition + new Vector3 (0, Mathf.PingPong (Time.time * 0.05f, 0.025f), 0);
	}
}
