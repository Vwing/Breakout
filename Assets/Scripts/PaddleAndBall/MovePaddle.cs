//Moves paddle according to the direction
//the main camera is looking. Puts it on the
//paddle box bounds.

using UnityEngine;
using System.Collections;

public class MovePaddle : UnityEngine.MonoBehaviour
{
    public LayerMask layerMask;
    private GameObject paddle;
    private Rigidbody rb;
    private Transform cameraTransform;

	void Start () 
	{
        cameraTransform = Camera.main.transform;
    }

    Vector3 fwd;
	void Update ()
	{
        //if no paddle set, find one.
        if (!paddle)
        {
            paddle = GameObject.FindGameObjectWithTag("Paddle");
            if (!paddle)
                return;
            else
                rb = paddle.GetComponent<Rigidbody>();
        }
        MovePaddleWithView();
    }
    
    RaycastHit hit;
    void MovePaddleWithView()
    {
        fwd = cameraTransform.TransformDirection(Vector3.forward);
        if(rb && Physics.Raycast(transform.position, fwd * 15, out hit, layerMask))
        {
            rb.MovePosition(hit.point);
            rb.MoveRotation(hit.transform.rotation);
        }
    }
}