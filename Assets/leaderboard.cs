using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class leaderboard : MonoBehaviour
{
    readonly string getURL = "localhost:8000/scores_get.php";
    private string text;
    // Start is called before the first frame update
    void Start()
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
            CheckScores(text);
        }
    }

    void CheckScores(string text)
    {

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
