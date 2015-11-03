using UnityEngine;
using System.Collections;

public class TurnBallTowardsPlayer : UnityEngine.MonoBehaviour
{
    //How much the ball should turn (in radians) towards player position (Origin point of world)
    //when bouncing against sphere bounds
    public float turningRate = 0.1f;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Ball")
            return; //only do the following for Ball

        Vector3 ballVelocity = other.rigidbody.velocity;
        Vector3 towardsPlayer = -ballVelocity.magnitude * other.transform.position.normalized;

        ballVelocity = Vector3.RotateTowards(ballVelocity, towardsPlayer, turningRate, 0f);

        other.rigidbody.velocity = ballVelocity;
    }
}
