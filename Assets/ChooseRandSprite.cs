using UnityEngine;
using System.Collections;

public class ChooseRandSprite : MonoBehaviour {

    void Awake()
    {
        int ranIndex = (int)(Random.value*transform.childCount);
        Transform chosenChild = transform.GetChild(ranIndex);
        chosenChild.gameObject.SetActive(true);
    }
}
