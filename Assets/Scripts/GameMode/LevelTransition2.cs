using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTransition2 : MonoBehaviour
{
    public GameObject[] levels;
    public float timeToTransition = 10f;
    private Rigidbody rb;
    private int maxLives;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        maxLives = GameManager.lives;
    }

    public void NextLevel()
    {
        int nextLevel = (GameManager.currentLevel + 1) % levels.Length;
        GoToLevel(nextLevel);
    }

    public void GoToLevel(int nextLevel)
    {
        StartCoroutine(Transition(GameManager.currentLevel % levels.Length, nextLevel));
    }

    IEnumerator Transition(int a, int b)
    {
        Vector3 from = levels[a].transform.position;
        Vector3 to = levels[b].transform.position;
        
        levels[b].SetActive(true);
        RaycastHit[] hits = Physics.RaycastAll(from, to - from, Vector3.Distance(from, to));
        SetWallsActive(hits, false);

        for (float i = 0f; i <= timeToTransition; i += Time.fixedDeltaTime )
        {
            rb.MovePosition(Vector3.Lerp(from, to, i / timeToTransition));
            //transform.position = Vector3.Lerp(from, to, i / timeToTransition);
            yield return new WaitForFixedUpdate();
        }
        SetWallsActive(hits, true);
        levels[a].SetActive(false);

        GameObject.Find("Ball").GetComponent<Ball>().StickBall();
        GameManager.lives = maxLives;
        ++GameManager.currentLevel;
        GameManager.transitioning = false;
    }

    void SetWallsActive(RaycastHit[] hits, bool active)
    {
        foreach (RaycastHit hit in hits)
            if (hit.transform.tag == "Wall")
                hit.transform.gameObject.SetActive(active);
    }
}
