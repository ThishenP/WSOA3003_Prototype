using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject particlePrefab;
    private GameObject explosion;
    
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
        explosion.transform.position = transform.position;
        explosion.GetComponent<ParticleSystem>().Play();
        transform.position = poolPos;
        control.instance.Point();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            transform.position = poolPos;
            
        }
    }
}
