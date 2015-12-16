using UnityEngine;
using System.Collections;

public class FollowPaddle : MonoBehaviour 
{
    public Transform paddle;
    public Transform ball;
    Transform arrow;
    Ball ballScript;
    HitBall hitBallScript;
    void Start()
    {
        arrow = transform.GetChild(0);
        if (!ball)
            Debug.Log("Attach ball to FollowPaddle script");
        if (!paddle)
            Debug.Log("Attach paddle to FollowPaddle script");
        ballScript = ball.GetComponent<Ball>();
        hitBallScript = paddle.GetComponent<HitBall>();
    }
	// Update is called once per frame
	void Update () 
    {
        transform.position = paddle.position;
        transform.rotation = paddle.rotation;
	}
}
