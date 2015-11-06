using UnityEngine;
using System.Collections;

public class PaddleBox : UnityEngine.MonoBehaviour
{
    public Material PaddleBoxHit;
    public Material paddleBoxReg;
	public GameObject south;
	public GameObject east;
	public GameObject west;
	public GameObject up;
	public GameObject down;

	void Start () 
	{

	}

	void Update ()
	{
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Ball")
        {
            Debug.Log("Hit");
            StartCoroutine(Damaged(other));
        }
    }

    IEnumerator Damaged(Collider ball){
        GameManager.lives -= 1;

		// Play whoosh! sound
		GetComponent<AudioSource> ().Play ();

		// Flash alert material on the paddle box

		float starttime = Time.time;
		Renderer r = GetComponent<Renderer> ();

		setAllMaterials(PaddleBoxHit);

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}
		
		setAllMaterials(paddleBoxReg);
		starttime = Time.time;
		

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}

		setAllMaterials(PaddleBoxHit);

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}
		
		setAllMaterials(paddleBoxReg);
		starttime = Time.time;
		

		while (Time.time < starttime + 0.1f) {
			yield return null;
		}
		
		setAllMaterials(PaddleBoxHit);
		starttime = Time.time;
		
		while (Time.time < starttime + 0.2f) {
			yield return null;
		}
		
		// Return to regular material, unless game lost
		if (GameManager.lives > 0) {
			setAllMaterials(paddleBoxReg);
		}
	}

	// Sets the material on all walls of the paddle box
	void setAllMaterials(Material m) {
		this.GetComponent<Renderer> ().material = m;
		south.GetComponent<Renderer> ().material = m;
		east.GetComponent<Renderer> ().material = m;
		west.GetComponent<Renderer> ().material = m;
		up.GetComponent<Renderer> ().material = m;
		down.GetComponent<Renderer> ().material = m;
	}


}
