using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackPlayer : MonoBehaviour
{
    public float startEnemySpeed=2;
    public float enemySpeedRate=0.1f;
    private Vector2 poolPos = new Vector2(-40,-40);
    private Transform playerPos;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startEnemySpeed += Time.deltaTime * enemySpeedRate;
        if (transform.position.y>-30 && control.instance.end == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, startEnemySpeed * Time.deltaTime);
        }

    }
}
