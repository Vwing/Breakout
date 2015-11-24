﻿//Moves paddle according to the direction
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
        }
        MovePaddleWithView();
    }
    
    RaycastHit hit;
    void MovePaddleWithView()
    {
        fwd = cameraTransform.TransformDirection(Vector3.forward);
        if(paddle && Physics.Raycast(transform.position, fwd * 15, out hit, layerMask))
        {
            paddle.transform.position = hit.point;
            paddle.transform.rotation = hit.transform.rotation;
            //paddle.transform.rotation = Quaternion.LookRotation(-1 * getInterpolatedNormal(hit), Vector3.up);
        }
    }

    Vector3 getInterpolatedNormal(RaycastHit hit)
    {
        Mesh mesh = ((MeshCollider)hit.collider).sharedMesh;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;
        Vector3 n0 = normals[triangles[hit.triangleIndex * 3 + 0]];
        Vector3 n1 = normals[triangles[hit.triangleIndex * 3 + 1]];
        Vector3 n2 = normals[triangles[hit.triangleIndex * 3 + 2]];
        Vector3 baryCenter = hit.barycentricCoordinate;
        Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
        interpolatedNormal = interpolatedNormal.normalized;
        Transform hitTransform = hit.collider.transform;
        interpolatedNormal = hitTransform.TransformDirection(interpolatedNormal);
        return interpolatedNormal;
    }
}