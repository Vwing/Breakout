﻿using UnityEngine;
using System.Collections;

public class Laserz : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision other)
    {
        this.gameObject.SetActive(false);
    }
}