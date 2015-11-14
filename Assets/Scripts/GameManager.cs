//GameManager modeled after the one in the Breakout Unity tutorial.
//Check the Learning Resources doc for a link.

using UnityEngine;
using System.Collections;

public class GameManager : UnityEngine.MonoBehaviour
{
    [Range (1,7)] public int MaxLives = 3;
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
        bricks = GameObject.FindGameObjectsWithTag("Brick").Length;
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
        if(YouWinText)
            YouWinText.SetActive(false);
        if(YouLoseText)
            YouLoseText.SetActive(false);
    }

	void Update ()
	{
        if (gameOver)
        {
            if (Input.GetButtonDown("Fire1") || Cardboard.SDK.Triggered)
            {
                Destroy(this); //This will call the OnDestroy() method below.
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

        if(YouLoseText)
            YouLoseText.SetActive(true);
        Instantiate(Explosion, Paddle.transform.position, Paddle.transform.rotation);
        Instantiate(Explosion, Ball.transform.position, Ball.transform.rotation);
        Destroy(Paddle);
        Destroy(Ball);
        gameOver = true;

        GameObject.Find("Player").GetComponent<LevelTransition>().enabled = true; //For now, level transition when you lose!
    }

    void CheckIfWon()
    {
        if (bricks > 0)
            return;
        if(YouWinText)
            YouWinText.SetActive(true);
        gameOver = true;
    }
}
