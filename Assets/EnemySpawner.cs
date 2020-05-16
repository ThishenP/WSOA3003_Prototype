using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    private int amountOfPrefabs = 10;
    private GameObject[] enemy;
    private Vector2 poolPos = new Vector2(-40,-40);
    private Vector2 spawnPos;
    private float timeSinceSpawn=0;
    private int enemyIndex=0;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = new GameObject[amountOfPrefabs];
        for (int i = 0; i < amountOfPrefabs; i++)
        {
           enemy[i] = Instantiate(enemyPrefab, poolPos, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn>2 && control.instance.end==false)
        {
            spawnPos = RandomEnemySpawnPoint();
      
            enemy[enemyIndex].transform.position = spawnPos;
            enemyIndex++;
            timeSinceSpawn = 0;
            if (enemyIndex>amountOfPrefabs-1)
            {
                enemyIndex = 0;
            }
        }
    }

    Vector2 RandomEnemySpawnPoint()
    {
        Vector2 spawnPoint= new Vector2(0,0);
        int side = Random.Range(1, 5);
        //1-top, 2-right, 3-bottom, 4-left
        float sidePos = Random.Range(1, 100);
        int posNeg = -1; // Random.Range(0, 2) * 2 - 1;//either negative/positive 1

        switch (side)
        {
            case 1:spawnPoint = new Vector2(((posNeg)*(sidePos / 100 * 9)), 6);
                break;
            case 2:spawnPoint = new Vector2(9.5f, ((posNeg) * (sidePos / 100 * 4.5f)));
                break;
            case 3:spawnPoint = new Vector2(((posNeg)*(sidePos / 100 * 9)), -6);
                break;
            case 4:spawnPoint = new Vector2(-9.5f, ((posNeg) * (sidePos / 100 * 4.5f)));
                break;
        }
            
        return spawnPoint;
        
    }
}
