using UnityEngine;
using System.Collections;

public class HitBall : MonoBehaviour
{
    public float angleMagnitude = 10f;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Ball")
            return;
        //Find average contact point
        Vector3 middleContactPoint = Vector3.zero;
        foreach (ContactPoint contact in other.contacts)
        {
            middleContactPoint += contact.point;
        }
        middleContactPoint /= other.contacts.Length;

        middleContactPoint = transform.InverseTransformPoint(middleContactPoint); //converts to local space
        Vector3 ballDirection = middleContactPoint + Vector3.forward * angleMagnitude; //set direction to hit the ball
        ballDirection = Vector3.Normalize(ballDirection); //normalize it
        other.rigidbody.velocity = transform.TransformDirection(ballDirection) * other.rigidbody.velocity.magnitude; //set ball velocity to new direction, same speed.
    }
}
