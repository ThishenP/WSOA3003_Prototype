using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;
using UnityEngine.Networking;

public class Pause : MonoBehaviour
{
    
    public GameObject pauseMenu;
    public AudioSource music;
    private float vol;
    public AudioSource[] sounds;
    private float[] FXvolumes =new float[10];
    public Text muteMusic;
    public Text muteFX;
    private string preferences;
    readonly string prefsGetURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/get_player_prefs.php";
    readonly string prefsPostURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/post_player_prefs.php";
    //readonly string prefsGetURL = "localhost:8000/get_player_prefs.php";
    //readonly string prefsPostURL = "localhost:8000/post_player_prefs.php";
   
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        vol = music.volume;
        for (int i=0;i<sounds.Length;i++)
        {
            FXvolumes[i] = sounds[i].volume;
        }
        StartCoroutine(GetPrefs());
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
        if (!control.instance.mutedMusic)
        {
            music.volume = 0.01f;
        }
        
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        control.instance.paused = false;
        Time.timeScale = 1f;
        if (!control.instance.mutedMusic)
        {
            music.volume = vol;
        }
    }

    public void MuteMusic()
    {
        
        if (control.instance.mutedMusic==true)
        {
            music.volume = vol;
            muteMusic.text = "MUTE\nMUSIC";
            control.instance.mutedMusic = false;
        }
        else
        {
            music.volume = 0;
            muteMusic.text = "UNMUTE\nMUSIC";
            control.instance.mutedMusic = true;
        }
        SendPref();
    }


    public void MuteSounds()
    {
        if(control.instance.mutedFX == true)
        {

            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].volume = FXvolumes[i];
            }
            muteFX.text = "MUTE\nFX";
           
            control.instance.mutedFX = false;
        }
        else
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].volume = 0;
            }
            muteFX.text = "UNMUTE\nFX";
           
            control.instance.mutedFX = true;
        }
        SendPref();
    }
    IEnumerator GetPrefs()
    {
        UnityWebRequest www = UnityWebRequest.Get(prefsGetURL);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            preferences = www.downloadHandler.text;
     
            CheckMutes();
        }
    }
    private void CheckMutes()
    {
        if (preferences != "" && preferences != null)
        {
            string[] prefers = preferences.Split(',');
           
            if (prefers[0] == "True")
            {
                music.volume = 0;
                muteMusic.text = "UNMUTE\nMUSIC";
                control.instance.mutedMusic = true;
            }
            else
            {
                music.volume = vol;
                muteMusic.text = "MUTE\nMUSIC";
                control.instance.mutedMusic = false;
            }

            if (prefers[1] == "True")
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].volume = 0;
                }
                muteFX.text = "UNMUTE\nFX";

                control.instance.mutedFX = true;
            }
            else
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].volume = FXvolumes[i];
                }
                muteFX.text = "MUTE\nFX";

                control.instance.mutedFX = false;
            }


        }

        
    }

    public void SendPref()
    {
        string pref = control.instance.mutedMusic + "," + control.instance.mutedFX;
        StartCoroutine(SendPrefs(pref));
    }
    IEnumerator SendPrefs(string prefs)
    {
        bool success = true;
        List<IMultipartFormSection> survey = new List<IMultipartFormSection>();
        survey.Add(new MultipartFormDataSection("prefs", prefs));
        Debug.Log(prefsPostURL);
        UnityWebRequest www = UnityWebRequest.Post(prefsPostURL, survey);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }



}
