using UnityEngine;
using System.Collections;

public class ChooseRandSprite : MonoBehaviour {

    void Start()
    {
        Transform player = GameObject.Find("Player").transform;
        int ranIndex = (int)(Random.value*transform.childCount);
        Transform chosenChild = transform.GetChild(ranIndex);
        chosenChild.rotation = Quaternion.LookRotation(chosenChild.transform.position - player.position);
        chosenChild.gameObject.SetActive(true);
    }
}
