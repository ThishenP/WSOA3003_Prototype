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
    public float[,] overAllHeatMap = new float[8, 16];
    public string heatMapData = "";
    private float timeSinceEnd;
    public GameObject heatCellPrefab;
    public bool showingHeatMap=false;
    public bool spawnedHeat=false;
    public bool spawnedOverallHeat = false;
    public GameObject heatMapBack;
    private GameObject[] heatCells;
    private int heatCount=0;
    public int lmbAmount=0;
    public int mmbamount=0;
    public int rmbamount=0;
    public string current;
    public GameObject surveyFeedbackObj;
    public GameObject gameOverObj;
    public screenShake screenShake;
    public bool paused = false;


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
    void Start()
    {
        heatCells = new GameObject[130];
        for(int i=0; i < 130; i++)
        {
            heatCells[i]= Instantiate(heatCellPrefab, new Vector2(-40,40), Quaternion.identity);
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
                    heatCells[heatCount].transform.position = new Vector2(j - 7.5f, -i + 3.5f);
                    heatCells[heatCount].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, control.instance.heatMap[i, j]);
                    
                    heatCount++;
                    if (heatCount > 128)
                    {
                        heatCount = 0;
                    }
                }
            }
            spawnedHeat = true;
            spawnedOverallHeat = false;
        }
    }

    public void ViewOverallHeatMap()
    {
        green.SetActive(true);
        heatMapBack.SetActive(true);
        gameOver.SetActive(false);

        showingHeatMap = true;
        if (spawnedOverallHeat == false)
        {
            for (int i = 0; i < heatMap.GetLength(0); i++)
            {
                for (int j = 0; j < heatMap.GetLength(1); j++)
                {
                    heatCells[heatCount].transform.position = new Vector2(j - 7.5f, -i + 3.5f);
                    heatCells[heatCount].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, control.instance.overAllHeatMap[i, j]);
                    heatCount++;
                    if (heatCount > 128)
                    {
                        heatCount = 0;
                    }
                }
            }
            spawnedHeat = false;
            spawnedOverallHeat = true;
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

    public void Feedback()
    {
        surveyFeedbackObj.SetActive(true);
        gameOverObj.SetActive(false);
    }

    public void Back()
    {
        surveyFeedbackObj.SetActive(false);
        gameOverObj.SetActive(true);
    }

    public void Shake(float duration, float amountOfShake)
    {
        StartCoroutine(screenShake.Shake(duration, amountOfShake));
    }
}
