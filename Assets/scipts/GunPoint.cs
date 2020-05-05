using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    public GameObject player;
    public GameObject shootRotate;
    public ParticleSystem particles;
    public ParticleSystem shootParticle;
    private Rigidbody2D playerRigid;
    public float speed=1;
    public float shootForce=10;
    private Vector2 mousePos;
    private Vector2 gunDir;
    
    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (control.instance.end == false)
        {
            transform.position = player.transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gunDir = new Vector2(transform.position.x, transform.position.y) - mousePos;
            transform.up = -gunDir;
            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
            }
            if (Input.GetMouseButtonDown(0))
            {
                particles.Play();
            }
            if (Input.GetMouseButton(0))
            {
                playerRigid.AddForce(gunDir.normalized * speed);
            }
            if (Input.GetMouseButtonUp(0))
            {
                particles.Stop();
            }
        }
       
       
       
    }

    void Shoot()
    {
        shootRotate.transform.up = -gunDir;
        shootParticle.Play();
        
        playerRigid.AddForce(gunDir.normalized * shootForce);
    }

}
