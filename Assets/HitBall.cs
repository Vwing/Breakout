using UnityEngine;
using System.Collections;

public class HitBall : MonoBehaviour
{
    //Directly changes the velocity of the ball so that it goes at an angle relative to the
    //center of the paddle
    
    //the lower the angleMagnitude, the greater the angle the ball will go at
    //(sorry it's unintuitive)
    public float angleMagnitude = 0.25f;

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
