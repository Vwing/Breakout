//Moves paddle according to the direction
//the main camera is looking. Puts it on the
//paddle box bounds.

using UnityEngine;
using System.Collections;

public class MovePaddle : UnityEngine.MonoBehaviour
{
    public LayerMask layerMask;
    private GameObject paddle;
    private Transform cameraTransform;

	void Start () 
	{
        cameraTransform = Camera.main.transform;

		//if no paddle set, find one.
		if (!paddle)
		{
			paddle = GameObject.FindGameObjectWithTag("Paddle");
		}
	}
    
	void Update ()
	{
		MovePaddleWithView();
    }    
    
    void MovePaddleWithView()
    {
		RaycastHit hit;
		Vector3 fwd = cameraTransform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, fwd * 15, out hit, layerMask))
        {
            paddle.transform.position = hit.point;
			paddle.transform.rotation = hit.transform.rotation;
        }
    }
}