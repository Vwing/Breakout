//GameManager modeled after the one in the Breakout Unity tutorial.
//Check the Learning Resources doc for a link.

using UnityEngine;
using System.Collections;

public class GameManager : UnityEngine.MonoBehaviour
{
    [Range (1,7)] public int MaxLives = 3;
    public int MaxBricks = 20;
    public int haveBricks;
    public static int lives = 1;
    public static int bricks = 1;
    public static GameManager instance = null;
    public GameObject YouWinText;
    public GameObject YouLoseText;
    public GameObject Paddle;
    public GameObject Ball;
    public GameObject Explosion;
    private bool gameOver = false;

	void Awake () 
	{
        lives = MaxLives;
        bricks = MaxBricks;
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        Setup();
	}

    //void OnLevelWasLoaded(int level)
    //{
    //    lives = MaxLives;
    //    bricks = MaxBricks;
    //}

    void Setup()
    {
        YouWinText.SetActive(false);
        YouLoseText.SetActive(false);
    }

	void Update ()
	{

		haveBricks = bricks;
        if (gameOver)
        {
            if (Input.GetButtonDown("Fire1") || Cardboard.SDK.Triggered)
            {
                Destroy(this);
            }
            return;
        }
        CheckIfGameover();
        CheckIfWon();
	}

    void OnDestroy()
    {
        Application.LoadLevel(0);
    }

    void CheckIfGameover()
    {
        if (lives > 0)
            return;

        YouLoseText.SetActive(true);
        Instantiate(Explosion, Paddle.transform.position, Paddle.transform.rotation);
        Instantiate(Explosion, Ball.transform.position, Ball.transform.rotation);
        Destroy(Paddle);
        Destroy(Ball);
        gameOver = true;
    }

    void CheckIfWon()
    {
        if (bricks > 0)
            return;
        YouWinText.SetActive(true);
        gameOver = true;
    }
}
