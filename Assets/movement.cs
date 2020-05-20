using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{   public float accelerationSpeed=3;
    public Vector2 horVel;
    public Vector2 vertVel;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        horVel = Input.GetAxis("Horizontal") * (new Vector2(1, 0)) * accelerationSpeed;
        vertVel = Input.GetAxis("Vertical") * (new Vector2(0, 1)) * accelerationSpeed;
       





        //horVel = Input.GetAxis("Horizontal") * (new Vector2(1, 0)) * accelerationSpeed;
        //vertVel = Input.GetAxis("Vertical") * (new Vector2(0, 1)) * accelerationSpeed;


        //    if (horVel != null && vertVel != null)
        //    {
        //        rb.velocity = (horVel + vertVel) / 2;
        //    }
        //    else
        //    {
        //        rb.velocity = horVel + vertVel;
        //    }



    }
}
