//GameManager modeled after the one in the Breakout Unity tutorial.
//Check the Learning Resources doc for a link.

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static int lives = 2;
    public static int bricks = 20;
    public static GameManager instance = null;
    public GameObject YouWinText;
    public GameObject YouLoseText;
    public GameObject Paddle;
    public GameObject Ball;
    public GameObject Explosion;

	void Awake () 
	{
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
        Setup();
	}

    void Setup()
    {
        YouWinText.SetActive(false);
        YouLoseText.SetActive(false);
    }

	void Update ()
	{
        if (Paddle == null)
        {
            if (Input.GetButtonDown("Fire1") || Cardboard.SDK.Triggered)
            {
                lives = 2;
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
        Debug.Log("GameOver");
        YouLoseText.SetActive(true);
        Instantiate(Explosion, Paddle.transform.position, Paddle.transform.rotation);
        Instantiate(Explosion, Ball.transform.position, Ball.transform.rotation);
        Destroy(Paddle);
        Destroy(Ball);
    }

    void CheckIfWon()
    {
        if (bricks > 0)
            return;
        YouWinText.SetActive(true);
    }
}
