using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WriteToFile : MonoBehaviour
{
    readonly string postURL = "http://localhost:8000/unity_post_handler.php";
    readonly string getURL = "http://localhost:8000/unity_get_handler.php";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SendToFile());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReturnSurvey(){
        StartCoroutine(SendToFile());
    }

    IEnumerator SendToFile()
    {
        bool success = true;
        List<IMultipartFormSection> survey = new List<IMultipartFormSection>();
        survey.Add(new MultipartFormDataSection("name", "TEST"));
        survey.Add(new MultipartFormDataSection("answer1", "Answer1Test"));
        survey.Add(new MultipartFormDataSection("answer2", "Answer2Test"));
      
        UnityWebRequest www =  UnityWebRequest.Post(postURL,survey);

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
