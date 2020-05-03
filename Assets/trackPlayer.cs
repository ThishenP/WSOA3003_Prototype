using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackPlayer : MonoBehaviour
{
    public float enemySpeed=5;
    private Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, enemySpeed * Time.deltaTime);
    }
}
