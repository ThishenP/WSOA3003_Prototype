using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public  Image healthBar;
    public bool invincibility = false;
    private float health = 100;
    private Vector2 poolPos = new Vector2(-40, -40);
    public GameObject particlePrefab;
    public GameObject smallExplostionPrefab;
    private GameObject smallExplosion;
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 1;
        explosion = Instantiate(particlePrefab, poolPos, Quaternion.identity);
        smallExplosion = Instantiate(smallExplostionPrefab, poolPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {

            if (invincibility == false) { health -= 10; }
            healthBar.fillAmount = health / 100;
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (invincibility == false) { health -= 20; }
            healthBar.fillAmount = health / 100;
            smallExplosion.transform.position = transform.position;
            smallExplosion.GetComponent<ParticleSystem>().Play();
        }

        if (health <= 0)
        {
            control.instance.EndGame();
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            transform.position = new Vector2(30, 30);
        }
    
    }


}
