using UnityEngine;
using System.Collections;

public class PaddleBox : UnityEngine.MonoBehaviour
{
    public Material PaddleBoxHit;
    public Material paddleBoxReg;

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

    IEnumerator Damaged(Collider ball)
    {
        GameManager.lives -= 1;


        float t = 0f;
        while(t < 0.3f)
        {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //mesh.material = paddleBoxReg;
    }
}
