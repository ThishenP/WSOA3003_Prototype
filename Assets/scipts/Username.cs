﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Username : MonoBehaviour
{
   // readonly string getURL = "localhost:8000/scores_get.php";
   // readonly string scoresPostURL = "localhost:8000/username_scores_post_handler.php";
    readonly string getURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/scores_get.php";
    readonly string scoresPostURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/username_scores_post_handler.php";
    
    public string text;
    public InputField username;
    public Text errorText;
    public GameObject error;
    public GameObject menu;
    public GameObject userInput;
    public Text signedIn;
    private bool loggedIn=false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetScores());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetScores()
    {
        UnityWebRequest www = UnityWebRequest.Get(getURL);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            text = www.downloadHandler.text;
        }
    }

    IEnumerator user(string addType,string scores, string user)
    {
        bool success = true;
        List<IMultipartFormSection> survey = new List<IMultipartFormSection>();
        survey.Add(new MultipartFormDataSection("scores", scores));
        survey.Add(new MultipartFormDataSection("user", user));
        survey.Add(new MultipartFormDataSection("AddType", addType));

        UnityWebRequest www = UnityWebRequest.Post(scoresPostURL, survey);

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


    public void Play()
    {
        if (loggedIn==true)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            errorText.text = "please login or create an account before playing";
            error.SetActive(true);
            menu.SetActive(false);
        }
    }
    public void login()
    {
        if (text!=null)
        {
            if (username.text!="")
            {
                if (checkForUser(username.text) == true)
                {
                    StartCoroutine(user("overwrite", "--none--", username.text));
                    userInput.SetActive(false);
                    signedIn.text = "signed in as: " + username.text;
                    loggedIn = true;
                }
                else
                {
                    errorText.text = "username does not exist";
                    error.SetActive(true);
                    menu.SetActive(false);
                }
            }
            else
            {
                errorText.text = "please enter a valid username";
                error.SetActive(true);
                menu.SetActive(false);
            }
           
        }
        else
        {
            errorText.text = "data has not loaded yet please try again";
            error.SetActive(true);
            menu.SetActive(false);
        }
    }

    public void createUser()
    {
        if (text != null)
        {
            if (username.text != "")
            {
                if (checkForUser(username.text) == true)
                {
                    errorText.text = "username already exists";
                    error.SetActive(true);
                    menu.SetActive(false);
                }
                else
                {
                    Debug.Log("user created");

                    StartCoroutine(user("append", username.text + ",0|", username.text));
                    userInput.SetActive(false);
                    signedIn.text = "signed in as: " + username.text;
                    loggedIn = true;
                }
            }
            else
            {
                errorText.text = "please enter a valid username";
                error.SetActive(true);
                menu.SetActive(false);
            }
        }
        else
        {
            errorText.text = "data has not loaded yet please try again";
            error.SetActive(true);
            menu.SetActive(false);
        }
    }

    public bool checkForUser(string username)
    {
        string[] userData = text.Split('|');
        foreach (var user in userData)
        {
            string[] entry = user.Split(',');
            if (entry[0] == username)
            {
                return true;
            }
        }
        return false;
        
    }

    public void back()
    {
        error.SetActive(false);
        menu.SetActive(true);
    }
}