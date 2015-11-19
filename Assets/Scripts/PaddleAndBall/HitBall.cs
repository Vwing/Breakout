/*
 * Directly changes the velocity of the ball so that it goes at an angle relative to the
 * center of the paddle

 * the lower the angleMagnitude, the greater the angle the ball will go at
 * (sorry it's unintuitive)
 */

using UnityEngine;
using System.Collections;

public class HitBall : UnityEngine.MonoBehaviour
{
    //public float midAngleMagnitude = 0.5f;
    //public float edgeAngleMagnitude = 0.15f;
    private Collider col;
    private float paddleRadius;

    void Awake()
    {
        col = GetComponent<Collider>();
        paddleRadius = col.bounds.extents.x;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Ball")
            return;
        //Find average contact point
        Vector3 contactPoint = Vector3.zero;
        foreach (ContactPoint contact in other.contacts)
        {
            contactPoint += contact.point;
        }
        contactPoint /= other.contacts.Length;
        float distFromMid = Vector3.Distance(contactPoint, col.bounds.center);
        //Debug.DrawLine(contactPoint, col.bounds.center, Color.red, 2f);
        Vector3 ballDirection = contactPoint - col.bounds.center; //90 degrees in direction of contactPoint
        if (distFromMid < paddleRadius * 0.95)
            ballDirection = Vector3.RotateTowards(ballDirection, transform.forward, paddleRadius / distFromMid / 3.2f, 0f);
        other.rigidbody.velocity = ballDirection * other.rigidbody.velocity.magnitude; //set ball velocity to new direction, same speed.
    }

    Vector3 GetBallDirection(Vector3 ballPos, Vector3 ballVel)
    {
        Vector3 ballDirection = new Vector3();
        

        return ballDirection;
    }
}
