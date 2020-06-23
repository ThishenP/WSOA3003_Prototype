
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Username : MonoBehaviour
{
    //readonly string getURL = "localhost:8000/scores_get.php";
    //readonly string scoresPostURL = "localhost:8000/username_scores_post_handler.php";
    //readonly string prefsPostURL = "localhost:8000/post_player_prefs.php";
    readonly string prefsPostURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/post_player_prefs.php";
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
    public GameObject play;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetScores());
        StartCoroutine(SendPrefs("False,False"));
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

    public void HowTo()
    {
        errorText.text = "How To Play\n\nPrimary Fire - Left Mouse Button(LMB)\nSecondary Fire - Right Mouse Button(RMB)" +
            "\n Bomb - Middle Mouse Button(MMB)\nPause - Space(sound settings in pause menu)\nAvoid or kill incoming red dots\nAvoid bright red walls"; 
        error.SetActive(true);
        menu.SetActive(false);
    }
    public void Credits()
    {
        errorText.text = "Credits\n\nFont: Ibram Syah\n\nMusic: '8 Bit Win!'\nBy HeatleyBros https://youtu.be/Bok8nLviThg";
        error.SetActive(true);
        menu.SetActive(false);
    }

    public void Play()
    {
        if (loggedIn==true)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            errorText.text = "please login or create an\n account before playing";
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
                    StartCoroutine(user("overwrite", "--none--", username.text.ToUpper()));
                    userInput.SetActive(false);
                    signedIn.text = "signed in as: " + username.text;
                    loggedIn = true;
                    play.SetActive(true);
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
                    StartCoroutine(user("append", username.text.ToUpper() + ",0|", username.text.ToUpper()));
                    userInput.SetActive(false);
                    signedIn.text = "signed in as: " + username.text;
                    loggedIn = true;
                    play.SetActive(true);
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
            if (entry[0].ToUpper() == username.ToUpper())
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
