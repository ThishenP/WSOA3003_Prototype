using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public  Image healthBar;
    private float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            health -= 10;
            healthBar.fillAmount = health / 100;
        }
    
    }


}
