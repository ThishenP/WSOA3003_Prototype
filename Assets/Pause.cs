using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public GameObject pauseMenu;
    public AudioSource music;
    private float vol;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        vol = music.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){

            if (control.instance.paused == false)
            {
                PauseGame();
                
            }
            else
            {
                UnPause();
            }
        }
    }


    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        control.instance.paused = true;
        Time.timeScale = 0f;
        music.volume = 0.01f;
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        control.instance.paused = false;
        Time.timeScale = 1f;
        music.volume = vol;
    }

}
