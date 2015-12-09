using UnityEngine;
using System.Collections;

public class BrickCatTypeB : Brick
{

    public float rotationSpeed = 5f;
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0, Space.Self);
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("outofbounds");
        Debug.Log(name);
        Debug.Log(other.transform.name);
        if (other.transform.tag != "Ball" && other.transform.tag != "Shrapnel")
            return;
        //Afaik, we do not need to decrement the number of bricks here since these are special "enemy" bricks.

        GameObject child = transform.GetChild(0).gameObject; //Save the cat...for now

        child.AddComponent<Rigidbody>(); //Add the rigidbody component to the cat within
        child.GetComponent<Rigidbody>().useGravity = true; //Allow the cat to use gravity so it will fall to its death
        transform.DetachChildren(); //Detach the cat from its prison (i.e. the brick)

        GameObject paddle = GameObject.FindGameObjectWithTag("Paddle");

        //Vector from brick to paddle is the vector <-transform.position.x, -transform.position.y, 11.4f - transform.position.z>.
        Vector3 vecCatToPaddle = new Vector3(paddle.transform.position.x - child.transform.position.x, paddle.transform.position.y - child.transform.position.y, paddle.transform.position.z - child.transform.position.z);
        //Vector3 projXZ = Vector3.ProjectOnPlane(vecCatToPaddle, new Vector3(0, 1, 0)); //Projection of the vector from cat to paddle onto the xz plane

        float by = vecCatToPaddle.y; //distance to paddle in y direction
        float bx = Mathf.Sqrt(Mathf.Pow(vecCatToPaddle.magnitude, 2) -  Mathf.Pow(by, 2)); //distance to paddle in x-z direction
        float theta; //arbitrary angle between min launch angle and 90 degree angle, inclusive
        float vi; //initial speed at launch angle
        float vx, vy; //launch speeds in the xz and y directions

        /*Old code start*/
        //float vx = bx / Mathf.Sqrt(2 * Mathf.Abs(bx - by) / 9.8f); //Note: vx = vy
        /*Old code end*/

        if (child.transform.position.y < paddle.transform.position.y) //if cat is below paddle
        {
            float minLaunchAngle = Mathf.Atan(by/bx); //get minimum launch angle to possibly hit paddle
            theta = Random.Range(minLaunchAngle, Mathf.PI/2); //pick arbitrary angle between min launch angle and 90 degree angle, inclusive

            Debug.Log("minLaunchAngle");
            Debug.Log(minLaunchAngle);
        }
        else //if cat is above or level with paddle
        {
            theta = Random.Range(0f, Mathf.PI / 2); //pick arbitrary angle between 0 degree angle and 90 degree angle, inclusive
        }
        
        Debug.Log("theta");
        Debug.Log(theta);
        
        vi = Mathf.Sqrt((9.8f * Mathf.Pow(bx, 2)) / (2 * bx * Mathf.Sin(theta) * Mathf.Cos(theta) - 2 * by * Mathf.Pow(Mathf.Cos(theta), 2)));
        vx = vi * Mathf.Cos(theta);
        vy = vi * Mathf.Sin(theta);

        Vector3 xzComponent = new Vector3(paddle.transform.position.x - child.transform.position.x, 0f, paddle.transform.position.z - child.transform.position.z); //Directional vector for x-z
        Vector3 yComponent = new Vector3(0f, 1f, 0f); //Directional vector for y
        xzComponent.Normalize(); //Set the magnitude of the xz component to 1.
        xzComponent = xzComponent * 1000000f; //Set the magnitude of the xz component to the highest number possible (not really but high enough).

        Vector3 vecFinal = Vector3.ClampMagnitude(xzComponent, vx) + yComponent * vy; //Add the vectors with the appropriate magnitudes


        /*Old code start
        Vector3 xzComponent = new Vector3(paddle.transform.position.x - child.transform.position.x, 0f, paddle.transform.position.z - child.transform.position.z); //Directional vector for x-z
        Vector3 yComponent = new Vector3(0f, vx, 0f); //Directional vector for y

        xzComponent.Normalize(); //Set the magnitude of the xz component to 1.

        xzComponent = xzComponent * 1000000f; //Set the magnitude of the xz component to the highest number possible (not really but high enough).

        Debug.Log("xzComponent");
        Debug.Log(xzComponent);

        Vector3 vecFinal = Vector3.ClampMagnitude(xzComponent, vx) + yComponent; //Add the vectors with the appropriate magnitudes of vx each.

        Debug.Log("vecFinal");
        Debug.Log(vecFinal);
        Old code finish*/

        child.GetComponent<Rigidbody>().mass = 1000000f; //Fat cat
        child.GetComponent<Rigidbody>().velocity = vecFinal; //Set velocity vector of freed cat
        child.GetComponent<CatDestructor>().launched = true;

        GameObject.Instantiate(SpawnUponDestruction, transform.position, transform.rotation); //Explosion effect at position of the brick

        Destroy(gameObject); //Destroy brick (but not cat...yet)
    }
}