using UnityEngine;
using System.Collections;

public class ChooseRandSprite : MonoBehaviour
{
    public Transform chosenChild;
    private Transform player;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        int ranIndex = (int)(Random.value*transform.childCount);
        chosenChild = transform.GetChild(ranIndex);
        TurnTowardsPlayer();
        chosenChild.gameObject.SetActive(true);
    }

    public void TurnTowardsPlayer()
    {
        chosenChild.rotation = Quaternion.LookRotation(chosenChild.transform.position - player.position);
    }
}
