//Moves paddle according to the direction
//the main camera is looking. Puts it on the
//paddle box bounds.

using UnityEngine;
using System.Collections;

public class MovePaddle : UnityEngine.MonoBehaviour
{
    public LayerMask layerMask;
    private GameObject paddleHolder;
    private Transform cameraTransform;

	void Start () 
	{
        cameraTransform = Camera.main.transform;
		paddleHolder = GameObject.FindGameObjectWithTag("PaddleHolder");
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
			paddleHolder.transform.position = hit.point;
			paddleHolder.transform.rotation = hit.transform.rotation;
		}
    }
}