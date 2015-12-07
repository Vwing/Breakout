using UnityEngine;
using System.Collections;

public class WeirdMachine_Brick : UnityEngine.MonoBehaviour
{
    public GameObject SpawnUponDestruction;

    void OnCollisionEnter(Collision col)
    {
		GameObject other = col.collider.gameObject;
		if (other.transform.tag != "Ball" && other.transform.tag != "Shrapnel" && other.transform.tag != "Weapon")
            return;
        GameManager.bricks--;
        GameObject.Instantiate(SpawnUponDestruction, col.transform.position, col.transform.rotation);
        
		// Destroy the entire WeirdMachine subtree
		foreach (Transform child in transform.parent.GetComponentsInChildren<Transform>()) {
			Destroy(child.gameObject);
		}
    }
}
