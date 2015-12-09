using UnityEngine;
using System.Collections;

public class LaunchCats : MonoBehaviour
{
    public float launchForce = 3f;
	// Use this for initialization
	void Start () {
        Transform player = GameObject.Find("Player").transform;
        int ranIndex = (int)(Random.value * transform.childCount);
        Transform chosenChild = transform.GetChild(ranIndex);
        chosenChild.rotation = Quaternion.LookRotation(chosenChild.transform.position - player.position);
        chosenChild.gameObject.SetActive(true);

	    for (int i = 0; i < transform.childCount; ++i)
	    {
	        transform.GetChild(i).GetComponent<Rigidbody>().velocity = Random.insideUnitSphere * launchForce;
	    }
	}
	
}
