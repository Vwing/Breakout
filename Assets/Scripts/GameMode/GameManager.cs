//GameManager modeled after the one in the Breakout Unity tutorial.
//Check the Learning Resources doc for a link.

using UnityEngine;
using System.Collections;

public class GameManager : UnityEngine.MonoBehaviour
{
    
    [Range (1,7)] public int MaxLives = 3;
    public static int lives = 1;
    public static int bricks = 1;
    public static int currentLevel = 0;
    public static GameManager instance = null;
    public static bool transitioning = false;
    //private bool gameOver = false;
    private LevelTransition2 transitionScript;

	void Awake () 
	{
        lives = MaxLives;
        bricks = GameObject.FindGameObjectsWithTag("Brick").Length;
	    GameObject paddlebox = GameObject.Find("PlayerPaddleBoxAmalgum");
        if(paddlebox)
            transitionScript = paddlebox.GetComponent<LevelTransition2>();
        else
            Debug.Log("Cannot transition due to missing paddleboxAmalgum. See GameManager.cs line 23");
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        //Setup();
	}
    void Update()
    {
        if (!transitioning && lives <= 0)
            Lose();
        else if (!transitioning && bricks <= 0)
            Win();
    }

    void Win()
    {
        transitioning = true;
        if(transitionScript)
            transitionScript.NextLevel();
    }

    void Lose()
    {
        transitioning = true;
        if (transitionScript)
            transitionScript.NextLevel();
    }
}
/* Old Methods */
//    public GameObject YouWinText;
//    public GameObject YouLoseText;
//    public GameObject Paddle;
//    public GameObject Ball;
//    public GameObject Explosion;
//void Setup()
//{
//    if(YouWinText)
//        YouWinText.SetActive(false);
//    if(YouLoseText)
//        YouLoseText.SetActive(false);
//}

//void Update ()
//{
//if (gameOver)
//{
//    if (Input.GetButtonDown("Fire1") || Cardboard.SDK.Triggered)
//    {
//        Destroy(this); //This will call the OnDestroy() method below.
//    }
//    return;
//}
//    CheckIfGameover();
//    CheckIfWon();
//}

//void OnDestroy()
//{
//    Application.LoadLevel(Application.loadedLevel);
//}

//void CheckIfGameover()
//{
//    if (lives > 0)
//        return;
//if(YouLoseText)
//    YouLoseText.SetActive(true);
//Instantiate(Explosion, Paddle.transform.position, Paddle.transform.rotation);
//Instantiate(Explosion, Ball.transform.position, Ball.transform.rotation);
//Destroy(Paddle);
//Destroy(Ball);
//    gameOver = true;
//}

//void CheckIfWon()
//{
//    if (bricks > 0)
//        return;
//    if(YouWinText)
//        YouWinText.SetActive(true);
//    gameOver = true;
//}