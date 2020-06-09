using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    readonly string getURL = "localhost:8000/scores_get.php";
    readonly string scoresPostURL = "localhost:8000/username_scores_post_handler.php";
    public string text;
    public InputField username;
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


    public void login()
    {
        if (text!=null)
        {
            if (checkForUser(username.text) == true)
            {
                StartCoroutine(user("overwrite","--none--", username.text));
            }
            else
            {
                Debug.Log("username does not exists");
            }
        }
        else
        {
            Debug.Log("data not yet loaded please try again");
        }
    }

    public void createUser()
    {
        if (text != null)
        {
            if (checkForUser(username.text) == true)
            {
                Debug.Log("username already exists");
            }
            else
            {
                Debug.Log("user created");
                StartCoroutine(user("append",username.text+",0\n", username.text));
            }
        }
        else
        {
            Debug.Log("data not yet loaded please try again");
        }
    }

    public bool checkForUser(string username)
    {
        string[] userData = text.Split('\n');
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
}
