//Moves paddle according to the direction
//the main camera is looking. Puts it on the
//paddle box bounds.

using UnityEngine;
using System.Collections;

public class MovePaddle : UnityEngine.MonoBehaviour
{
    private LayerMask layerMask;
    private GameObject paddle;
    private Rigidbody rb;
    private Transform cameraTransform;

	void Start () 
	{
        cameraTransform = Camera.main.transform;
        layerMask = 1 << LayerMask.NameToLayer("PaddleBox");
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
	}

    void FixedUpdate()
    {
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
//hits = Physics.RaycastAll(transform.position, fwd * 15, layerMask);
//RaycastHit box = hits[0];

//foreach (RaycastHit hit in hits)
//    if (hit.transform.tag == "PaddleBoxTag")
//    {
//        box = hit;
//        break;
//    }
//paddle.transform.position = hit.point;
//paddle.transform.rotation = hit.transform.rotation;