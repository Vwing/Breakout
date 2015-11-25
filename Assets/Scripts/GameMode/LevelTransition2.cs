using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTransition2 : MonoBehaviour
{
    public GameObject purgatory;
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
        GameObject aBox = purgatory.transform.FindChild(levels[a].name).gameObject;
        GameObject bBox = purgatory.transform.FindChild(levels[b].name).gameObject;
        Fade fadeA = aBox.GetComponent<Fade>();
        Fade fadeB = bBox.GetComponent<Fade>();
        //set brick box active, so it can fade in.
        aBox.SetActive(true);

        //fade in the Brick Box
        if (fadeA)
        {
            fadeA.In();
            while (fadeA.fading)
                yield return new WaitForSeconds(0.5f);
            RenderSettings.skybox = skies[0];
        }
        levels[a].SetActive(false);
        SetChildrenActive(purgatory.transform, true);
        
        //move the walls up
        List<MoveWall> walls = GetWalls(from, to);
        foreach (MoveWall wall in walls)
            wall.Up(4f);
        yield return new WaitForSeconds(4f);

        //move player to next location
        for (float i = 0f; i <= timeToTransition; i += Time.fixedDeltaTime )
        {
            rb.MovePosition(Vector3.Lerp(from, to, i / timeToTransition));
            yield return new WaitForFixedUpdate();
        }

        //move the walls down
        foreach (MoveWall wall in walls)
            wall.Down(4f);
        if (walls[0])
            while (walls[0].moving)
                yield return new WaitForSeconds(0.5f);

        //hide the other boxes before fading out
        SetChildrenActiveExcept(purgatory.transform, false, levels[b].name);

        //activate level before fading out
        levels[b].SetActive(true);
        GameManager.bricks = GameObject.FindGameObjectsWithTag("Brick").Length;

        //change the skybox and fade out
        RenderSettings.skybox = skies[b];
        if (fadeB)
        {
            fadeB.Out();
            while (fadeB.fading)
                yield return new WaitForSeconds(0.5f);
        }
        bBox.SetActive(false);
        paddle.GetComponent<Renderer>().enabled = true;
        ball.StickBall();
        GameManager.lives = maxLives;
        ++GameManager.currentLevel;
        GameManager.transitioning = false;
    }

    void SetChildrenActiveExcept(Transform parentTransform, bool active, string name)
    {
        for (int i = 0; i < parentTransform.childCount; ++i)
            if (parentTransform.GetChild(i).name != name)
                parentTransform.GetChild(i).gameObject.SetActive(active);
    }

    void SetChildrenActive(Transform parentTransform, bool active)
    {
        for (int i = 0; i < parentTransform.childCount; ++i)
                parentTransform.GetChild(i).gameObject.SetActive(active);
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
