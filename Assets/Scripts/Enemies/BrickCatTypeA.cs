using UnityEngine;
using System.Collections;

public class BrickCatTypeA : Brick
{

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "Ball" && other.transform.tag != "Shrapnel")
            return;
        //Afaik, we do not need to decrement the number of bricks here since these are special "enemy" bricks.

        GameObject child = transform.GetChild(0).gameObject; //Save the cat...for now

        child.SetActive(true);
        child.AddComponent<Rigidbody>(); //Add the rigidbody component to the cat within
        child.GetComponent<Rigidbody>().useGravity = true; //Allow the cat to use gravity so it will fall to its death
        transform.DetachChildren(); //Detach the cat from it's prison (i.e. the brick)
        GameObject.Instantiate(SpawnUponDestruction, transform.position, transform.rotation); //Explosion effect at position of the brick

        //Implement delayed cat death later...
        
        Destroy(gameObject); //Destroy brick (but not cat...yet)
    }
}