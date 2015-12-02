using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour {

	public Color energyColor;
	[Range(0,1)] public float visibility;
	private Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().material;
		mat.SetColor ("_EnergyColor", energyColor);
		mat.SetFloat ("_Visibility", visibility);
		mat.SetFloat ("_CollisionTime", -99f); // To ensure that animation is already "over" by the time we start.
	}

	// Set the collision time in the shader
	void OnCollisionEnter(Collision collision) {		
		// Mark the start of the animation
		mat.SetFloat ("_CollisionTime", Time.time);

		// Find and mark the texture coords of the impact point (Raycast workaround is necessary)
		RaycastHit hit = new RaycastHit ();
		Ray ray = new Ray (collision.contacts [0].point - collision.contacts [0].normal, collision.contacts [0].normal);
		if (Physics.Raycast (ray, out hit)) {
			Vector2 tex = hit.textureCoord;
			mat.SetVector ("_CollisionPoint", new Vector4 (tex.x, tex.y, 0, 0)); // Property is a Vector4
		}

		// Play the zap sound
		GetComponent<AudioSource> ().Play ();
	}
}
