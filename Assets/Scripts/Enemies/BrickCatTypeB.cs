using UnityEngine;
using System.Collections;

public class BrickCatTypeB : Brick
{
 
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "Ball")
            return;
        //Afaik, we do not need to decrement the number of bricks here since these are special "enemy" bricks.

        GameObject child = transform.GetChild(0).gameObject; //Save the cat...for now

        child.AddComponent<Rigidbody>(); //Add the rigidbody component to the cat within
        child.GetComponent<Rigidbody>().useGravity = true; //Allow the cat to use gravity so it will fall to its death
        transform.DetachChildren(); //Detach the cat from its prison (i.e. the brick)

        GameObject paddle = GameObject.FindGameObjectWithTag("Paddle");

        //Vector from brick to paddle is the vector <-transform.position.x, -transform.position.y, 11.4f - transform.position.z>.
        Vector3 vecCatToPaddle = new Vector3(paddle.transform.position.x - child.transform.position.x, paddle.transform.position.y - child.transform.position.y, paddle.transform.position.z - child.transform.position.z);

        float by = Mathf.Abs(vecCatToPaddle.y); //distance to paddle in y direction
        float bx = Mathf.Sqrt(Mathf.Pow(vecCatToPaddle.magnitude, 2) -  Mathf.Pow(by, 2)); //distance to paddle in x-z direction
        float vx = bx / Mathf.Sqrt(2 * Mathf.Abs(bx - by) / 9.8f); //Note: vx = vy

        Vector3 xzComponent = new Vector3(paddle.transform.position.x - child.transform.position.x, 0f, paddle.transform.position.z - child.transform.position.z); //Directional vector for x-z
        Vector3 yComponent = new Vector3(0f, vx, 0f); //Directional vector for y

        xzComponent.Normalize(); //Set the magnitude of the xz component to 1.

        xzComponent = xzComponent * 1000000f; //Set the magnitude of the xz component to the highest number possible (not really but high enough).

        Debug.Log("xzComponent");
        Debug.Log(xzComponent);

        Vector3 vecFinal = Vector3.ClampMagnitude(xzComponent, vx) + yComponent; //Add the vectors with the appropriate magnitudes of vx each.

        Debug.Log("vecFinal");
        Debug.Log(vecFinal);

        child.GetComponent<Rigidbody>().mass = 10000f;
        child.GetComponent<Rigidbody>().velocity = vecFinal; //Set velocity vector of freed cat

        GameObject.Instantiate(SpawnUponDestruction, transform.position, transform.rotation); //Explosion effect at position of the brick

        //Implement delayed cat death later...

        Destroy(gameObject); //Destroy brick (but not cat...yet)
    }
}