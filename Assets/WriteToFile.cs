using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WriteToFile : MonoBehaviour
{
    public InputField Name;
    public InputField Feedback;
    public GameObject form;
    readonly string postURL = "http://localhost:80/unity_post_handler.php";
    readonly string getURL = "http://localhost:80/unity_post_handler.php";
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnSurvey(){

        StartCoroutine(SendToFile(Name.text,Feedback.text));
        form.SetActive(false);
    }

    IEnumerator SendToFile(string name, string feedback)
    {
        bool success = true;
        List<IMultipartFormSection> survey = new List<IMultipartFormSection>();
        survey.Add(new MultipartFormDataSection("name", name+" : "));
        survey.Add(new MultipartFormDataSection("feedback", feedback+", "));
      
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
