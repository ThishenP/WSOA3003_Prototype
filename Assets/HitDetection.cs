using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject particlePrefab;
    private GameObject explosion;
    public AudioSource explosionSound;
  

    
    private Vector2 poolPos = new Vector2(-40,-40);

    void Start()
    {
        explosion = Instantiate(particlePrefab, poolPos, Quaternion.identity);
    }

    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        
        if (this.tag!= "Health") {
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            explosionSound.Play();
            transform.position = poolPos;
            control.instance.Point();
            
            control.instance.Shake(0.1f, 0.1f);
        }
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            if (this.tag != "Health")
            {
                explosionSound.Play();
               
               control.instance.Shake(0.1f, 0.2f);
            }
            
            transform.position = poolPos;
            
        }
    }
}
