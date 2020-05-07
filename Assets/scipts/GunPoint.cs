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
    private float timeSinceShot;
    
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
            timeSinceShot += Time.deltaTime;
            if (Input.GetMouseButtonDown(1)&&timeSinceShot>0.4)
            {
                Shoot();
            }
            if (Input.GetMouseButtonDown(0))
            {
                particles.Play();
            }
         
            if (Input.GetMouseButtonUp(0))
            {
                particles.Stop();
            }
        }
    }

    void FixedUpdate()
    {

        if (Input.GetMouseButton(0)&& control.instance.end == false)
        {
            playerRigid.AddForce(gunDir.normalized * speed);
        }
    }
    void Shoot()
    {
        timeSinceShot = 0;
        shootRotate.transform.up = -gunDir;
        shootParticle.Play();
        
        playerRigid.AddForce(gunDir.normalized * shootForce);
    }

}
