using UnityEngine;
using System.Collections;

public class ShootDemo : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 25;
    public float timer;
    public int counter = 0;

    public Ball _Ball;

    

    // Update is called once per frame

    void Start()
    {

        timer = 0;



    }


    void Update()
    {
        timer += Time.deltaTime;

        if (counter < 75 && _Ball.GunPowerup == 1)
        {

            if(timer > 0.25F)
            {



                Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));



                timer = 0;
                counter++;
            }

           

        }



    }
    public void laser()
    {

        counter = 0;

        if (_Ball.GunPowerup == 1)
        {


            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));


        }
    }


}