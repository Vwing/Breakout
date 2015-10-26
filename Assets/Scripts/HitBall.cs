/*
 * Directly changes the velocity of the ball so that it goes at an angle relative to the
 * center of the paddle

 * the lower the angleMagnitude, the greater the angle the ball will go at
 * (sorry it's unintuitive)
 */

using UnityEngine;
using System.Collections;

public class HitBall : MonoBehaviour
{
    public float midAngleMagnitude = 0.5f;
    public float edgeAngleMagnitude = 0.15f;

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
        contactPoint = transform.InverseTransformPoint(contactPoint); //converts to local space
        float paddleRadius = GetComponent<Collider>().bounds.extents.x;
        float distFromMid = Vector3.Distance(contactPoint, transform.localPosition);
        float angleMag = distFromMid < paddleRadius * 0.8f ? midAngleMagnitude : edgeAngleMagnitude;

        Vector3 ballDirection = contactPoint + Vector3.forward * angleMag; //set direction to hit the ball
        ballDirection = Vector3.Normalize(ballDirection); //normalize it
        other.rigidbody.velocity = transform.TransformDirection(ballDirection) * other.rigidbody.velocity.magnitude; //set ball velocity to new direction, same speed.
    }
}
