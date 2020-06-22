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
    public float healthThreshold;
    private float timeSinceHealth;
    public GameObject healthPickupPrefab;
    private GameObject[] healthPickup;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 1;
        explosion = Instantiate(particlePrefab, poolPos, Quaternion.identity);
        smallExplosion = Instantiate(smallExplostionPrefab, poolPos, Quaternion.identity);

        healthPickup = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            healthPickup[i] = Instantiate(healthPickupPrefab, poolPos, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
       
        timeSinceHealth += Time.deltaTime;
        if (timeSinceHealth > 15)
        {
            if (Random.Range(1, 99) > healthThreshold)
            {
                int i = Random.Range(0, 8);
                int j = Random.Range(0, 15);
                Vector2 randPos = new Vector2(j - 7.5f, -i + 3.5f);
                healthPickup[count].transform.position=randPos;
                count++;
                if (count >= 3)
                {
                    count = 0;
                }
                timeSinceHealth = 0;
            }
        }
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

        if (collision.gameObject.tag == "Health")
        {
            if (health <= 90)
            {
                health = health + 10;
                healthBar.fillAmount = health / 100;
               
            }
        }

        if (health > 50)
        {
            healthBar.color = new Color(0.001088489f, 0.8584906f, 0);
        }else if (health < 30)
        {
            
            healthBar.color = new Color(0.809f, 0, 0.05092573f);
        }
        else
        {
            healthBar.color = new Color(0.8962264f, 0.5053604f,0.1817818f);
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
