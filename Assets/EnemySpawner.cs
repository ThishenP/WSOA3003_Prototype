using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject damageWallPrefab;
    public GameObject wallPrefab;
    public float wallThresholdRate;
    
    private int amountOfPrefabs = 10;
    private GameObject[] enemy;
    private GameObject[] damageWall;
    private GameObject[] wall;
    private Vector2 poolPos = new Vector2(-40,-40);
    private Vector2 spawnPos;
    private float timeSinceSpawn=0;
    private float wallChangeTime = 0;
    private int enemyIndex=0;
    private float wallThreshold=100;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = new GameObject[amountOfPrefabs];
        for (int i = 0; i < amountOfPrefabs; i++)
        {
           enemy[i] = Instantiate(enemyPrefab, poolPos, Quaternion.identity);
        }
        

        damageWall = new GameObject[4];
        damageWall[0] = Instantiate(damageWallPrefab, new Vector2(9.9f, 0), Quaternion.identity);
        damageWall[1] = Instantiate(damageWallPrefab, new Vector2(-9.9f, 0), Quaternion.identity);
        damageWall[2] = Instantiate(damageWallPrefab, new Vector2(0, 6), Quaternion.identity);
        damageWall[3] = Instantiate(damageWallPrefab, new Vector2(0, -6), Quaternion.identity);
        damageWall[2].transform.Rotate(new Vector3(0,0,90));
        damageWall[3].transform.Rotate(new Vector3(0, 0, 90));
        for(int i =0; i < 4; i++)
        {
            damageWall[i].SetActive(false);
        }

        wall = new GameObject[4];
        wall[0] = Instantiate(wallPrefab, new Vector2(9.9f, 0), Quaternion.identity);
        wall[1] = Instantiate(wallPrefab, new Vector2(-9.9f, 0), Quaternion.identity);
        wall[2] = Instantiate(wallPrefab, new Vector2(0, 6), Quaternion.identity);
        wall[3] = Instantiate(wallPrefab, new Vector2(0, -6), Quaternion.identity);
        wall[2].transform.Rotate(new Vector3(0, 0, 90));
        wall[3].transform.Rotate(new Vector3(0, 0, 90));

    }

    // Update is called once per frame
    void Update()
    {
        if (wallThreshold>45)
        {
            wallThreshold -= Time.deltaTime * wallThresholdRate;
        }
        
        timeSinceSpawn += Time.deltaTime;
        wallChangeTime += Time.deltaTime;
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
        if (wallChangeTime>4 && control.instance.end==false)
        {
            Debug.Log(wallThreshold);
            float wall0 = Random.Range(1, 101);
            float wall1 = Random.Range(1, 101);
            float wall2 = Random.Range(1, 101);
            float wall3 = Random.Range(1, 101);
            Debug.Log(wall0);
            if (wall0 > wallThreshold)
            {
                wall[0].SetActive(false);
                damageWall[0].SetActive(true);
            }
            else
            {
                wall[0].SetActive(true);
                damageWall[0].SetActive(false);
            }
            if (wall1 > wallThreshold)
            {
                wall[1].SetActive(false);
                damageWall[1].SetActive(true);
            }
            else
            {
                wall[1].SetActive(true);
                damageWall[1].SetActive(false);
            }
            if (wall2 > wallThreshold)
            {
                wall[2].SetActive(false);
                damageWall[2].SetActive(true);
            }
            else
            {
                wall[2].SetActive(true);
                damageWall[2].SetActive(false);
            }
            if (wall3 > wallThreshold)
            {
                wall[3].SetActive(false);
                damageWall[3].SetActive(true);
            }
            else
            {
                wall[3].SetActive(true);
                damageWall[3].SetActive(false);
            }
            wallChangeTime = 0;
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
