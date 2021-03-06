﻿using UnityEngine;
using System.Collections;

public class Brick : UnityEngine.MonoBehaviour
{
    public GameObject SpawnUponDestruction;

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "Ball" && other.transform.tag != "Shrapnel" && other.transform.tag != "Weapon" && other.transform.tag != "Bullet" && other.transform.tag != "Laser")
            return;
        GameManager.bricks--;
        GameObject.Instantiate(SpawnUponDestruction, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
