using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class control : MonoBehaviour
{
    public static control instance;
    public bool end = false;
    public GameObject gameOver;
    public GameObject healthBar;
    public GameObject player;
    public GameObject green;
    public int score = 0;
    public Text scoreText;
    public float[,] heatMap = new float[8, 16];
    public string heatMapData = "";
    private float timeSinceEnd;
    public GameObject heatCellPrefab;
    public bool showingHeatMap=false;
    public bool spawnedHeat=false;
    public GameObject heatMapBack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (end==true&&showingHeatMap==false) {
            timeSinceEnd += Time.deltaTime;
            if (timeSinceEnd>2) {
                healthBar.SetActive(false);
                gameOver.SetActive(true);
                Destroy(player);
            }
        }
    }

    public void EndGame()
    {
        timeSinceEnd = 0;
        end = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        heatMapBack.SetActive(false);
    }

 

    public void ViewHeatMap()
    {
        green.SetActive(true);
        heatMapBack.SetActive(true);
        gameOver.SetActive(false);
        
        showingHeatMap = true;
        if (spawnedHeat == false)
        {
            for (int i = 0; i < heatMap.GetLength(0); i++)
            {
                for (int j = 0; j < heatMap.GetLength(1); j++)
                {
                    Instantiate(heatCellPrefab, new Vector2(j - 7.5f, -(i - 3.5f)), Quaternion.identity);
                    heatCellPrefab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, control.instance.heatMap[i, j]);

                }
            }
            spawnedHeat = true;
        }
    }


    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Point()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
