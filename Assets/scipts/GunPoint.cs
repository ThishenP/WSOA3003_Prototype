using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRigid;
    public float speed=1;
    private Vector2 mousePos;
    private Vector2 gunDir;
    
    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gunDir = new Vector2(transform.position.x, transform.position.y) - mousePos;
        transform.up = -gunDir;
        if (Input.GetMouseButtonDown(0))
        {
            ShootJump();
        }
       
    }

    void ShootJump()
    {
        playerRigid.velocity = Vector2.zero;
        playerRigid.AddForce(gunDir.normalized * speed);
    }

}
