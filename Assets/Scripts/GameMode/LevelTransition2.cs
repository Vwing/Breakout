using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTransition2 : MonoBehaviour
{
    public GameObject[] levels;
    public Material[] skies;
    public float timeToTransition = 10f;
    private Rigidbody rb;
    private int maxLives;
    private Ball ball;
    private GameObject paddle;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        maxLives = GameManager.lives;
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        paddle = GameObject.Find("Paddle");
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
        paddle.GetComponent<Renderer>().enabled = false;
        ball.MoveBall(1000f, 0f, 0f);
        Vector3 from = levels[a].transform.position;
        Vector3 to = levels[b].transform.position;
        Fade fadeA = levels[a].GetComponentInChildren<Fade>();
        Fade fadeB = levels[b].GetComponentInChildren<Fade>();

        if (fadeA)
        {
            fadeA.In();
            while (fadeA.fading)
                yield return new WaitForSeconds(0.5f);
            RenderSettings.skybox = skies[0];
        }
        
        List<MoveWall> walls = GetWalls(from, to);
        foreach (MoveWall wall in walls)
            wall.Up(4f);
        yield return new WaitForSeconds(4f);

        for (float i = 0f; i <= timeToTransition; i += Time.fixedDeltaTime )
        {
            rb.MovePosition(Vector3.Lerp(from, to, i / timeToTransition));
            yield return new WaitForFixedUpdate();
        }
        foreach (MoveWall wall in walls)
            wall.Down(4f);
        if (walls[0])
            while (walls[0].moving)
                yield return new WaitForSeconds(0.5f);

        RenderSettings.skybox = skies[b];
        if (fadeB)
        {
            fadeB.Out();
            while (fadeB.fading)
                yield return new WaitForSeconds(0.5f);
        }

        paddle.GetComponent<Renderer>().enabled = true;
        ball.StickBall();
        GameManager.lives = maxLives;
        ++GameManager.currentLevel;
        GameManager.transitioning = false;
    }

    List<MoveWall> GetWalls(Vector3 from, Vector3 to)
    {
        List<MoveWall> walls = new List<MoveWall>();
        RaycastHit[] hits = Physics.RaycastAll(from, to - from, Vector3.Distance(from, to));
        foreach (RaycastHit hit in hits)
            if (hit.transform.tag == "Wall")
                walls.Add(hit.transform.gameObject.GetComponent<MoveWall>());
        return walls;
    }

    void SetWallsActive(RaycastHit[] hits, bool active)
    {
        foreach (RaycastHit hit in hits)
            if (hit.transform.tag == "Wall")
                hit.transform.gameObject.SetActive(active);
    }
}
