using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class control : MonoBehaviour
{
    public static control instance;
    public bool end = false;
    public GameObject gameOver;
    public GameObject healthBar;
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
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
