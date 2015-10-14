//Moves paddle according to the direction
//the main camera is looking. Puts it on the
//paddle box bounds.

using UnityEngine;
using System.Collections;

public class MovePaddle : MonoBehaviour
{
    public GameObject paddle;
    private Transform cameraTransform;
    private int paddleBoxLayer;
	void Start () 
	{

        paddleBoxLayer = 1 << LayerMask.NameToLayer("PaddleBox");
        cameraTransform = GetComponent<ChooseRiftOrCardboard>().TheChosenCamera.transform;
	}

    RaycastHit[] hits;
    Vector3 fwd;
	void Update ()
	{
        if (paddle == null)
        {
            paddle = GameObject.Find("Paddle");
            if (paddle == null)
                return;
        }
        fwd = cameraTransform.TransformDirection(Vector3.forward);
        hits = Physics.RaycastAll(transform.position, fwd * 15);
        RaycastHit box;
        box = hits[0];
        foreach (RaycastHit hit in hits)
            if (hit.transform.tag == "PaddleBoxTag")
            {
                box = hit;
                break;
            }
        paddle.transform.position = box.point;
        paddle.transform.rotation = box.transform.rotation;
	}
}
