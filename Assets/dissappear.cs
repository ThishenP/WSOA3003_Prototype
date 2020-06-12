using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissappear : MonoBehaviour
{
    private Vector2 poolPos = new Vector2(-40, -40);
    private float timer=0;
    private bool timerSet=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x>-9.4&& transform.position.x<9.4 && transform.position.y>-5.3&&transform.position.y<5.3);
        {
            if (timerSet==false)
            {
                timer = 0;
                timerSet = true;
            }
            timer += Time.deltaTime;

            if (timer>3)
            {
                transform.position = poolPos;
                timer = 0;
            }

        }
    }
}
