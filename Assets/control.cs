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
    public int score = 0;
    public Text scoreText;
    private float timeSinceEnd;
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
        if (end==true) {
            timeSinceEnd += Time.deltaTime;
            if (timeSinceEnd>2) {
                healthBar.SetActive(false);
                gameOver.SetActive(true);
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
