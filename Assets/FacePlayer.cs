using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour {
    Transform player;
    bool quad;
	// Use this for initialization
	void Start () {
        GameObject p = GameObject.Find("Player");
        if (p)
            player = p.transform;
        MeshFilter m = GetComponent<MeshFilter>();
        if (m && m.mesh.name == "Quad Instance")
            quad = true;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (quad)
            transform.rotation = Quaternion.LookRotation(transform.position - player.position);
        else
            transform.LookAt(player);
	}
}
