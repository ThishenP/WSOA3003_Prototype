using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WriteToFile : MonoBehaviour
{
    public InputField Name;
    public InputField q1In;
    public InputField q2In;
    public InputField q3In;
    public InputField q4In;
    public InputField q5In;
    public InputField q6In;
    public InputField q7In;
    public InputField q8In;
    public ToggleGroup q1Tog;
    public ToggleGroup q2Tog;
    public ToggleGroup q3Tog;
    public ToggleGroup q4Tog;
    public ToggleGroup q5Tog;
    public ToggleGroup q6Tog;
    public GameObject form;
    private bool sent = false;
    // readonly string feedbackPostURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/unity_post_handler.php";
    //readonly string heatMapPostURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/heat_map_post_handler.php";
    // readonly string getURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/unity_post_handler.php";
    readonly string feedbackPostURL = "localhost:8000/unity_post_handler.php";
    readonly string heatMapPostURL = "localhost:8000/heat_map_post_handler.php";
    // Start is called before the first frame update
    void Start()
    {
        if (q1Tog == null) q1Tog = GetComponent<ToggleGroup>();
        if (q2Tog == null) q2Tog = GetComponent<ToggleGroup>();
        if (q3Tog == null) q3Tog = GetComponent<ToggleGroup>();
        if (q4Tog == null) q4Tog = GetComponent<ToggleGroup>();
        if (q5Tog == null) q5Tog = GetComponent<ToggleGroup>();
        if (q6Tog == null) q6Tog = GetComponent<ToggleGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        if (control.instance.end==true&&sent==false)
        {
            StartCoroutine(SendHeatMap(control.instance.heatMapData));
            sent = true;
        }
    }

    public void ReturnSurvey(){
        string feedback = "";
        int q1 = int.Parse(q1Tog.ActiveToggles().FirstOrDefault().name);
        int q2 = int.Parse(q2Tog.ActiveToggles().FirstOrDefault().name);
        int q3 = int.Parse(q3Tog.ActiveToggles().FirstOrDefault().name);
        int q4 = int.Parse(q4Tog.ActiveToggles().FirstOrDefault().name);
        int q5 = int.Parse(q5Tog.ActiveToggles().FirstOrDefault().name);
        int q6 = int.Parse(q6Tog.ActiveToggles().FirstOrDefault().name);
        feedback += "Q1: " + q1 +" comment: "+ q1In.text +"\n";
        feedback += "Q2: " + q2 + " comment: " + q2In.text + "\n";
        feedback += "Q3: " + q3 + " comment: " + q3In.text + "\n";
        feedback += "Q4: " + q4 + " comment: " + q4In.text + "\n";
        feedback += "Q5: " + q5 + " comment: " + q5In.text + "\n";
        feedback += "Q6: " + q6 + " comment: " + q6In.text + "\n";
        feedback += "Q7: comment: " + q7In.text + "\n";
        feedback += "Q8: comment: " + q8In.text + "\n\n";
       
        StartCoroutine(SendToFile(Name.text,feedback));
        form.SetActive(false);
    }

    IEnumerator SendToFile(string name, string feedback)
    {
        bool success = true;
        List<IMultipartFormSection> survey = new List<IMultipartFormSection>();
        survey.Add(new MultipartFormDataSection("name", name+" : \n"));
        survey.Add(new MultipartFormDataSection("feedback", feedback+"\n"));
        Debug.Log(feedbackPostURL);
        UnityWebRequest www =  UnityWebRequest.Post(feedbackPostURL,survey);

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

    IEnumerator SendHeatMap(string heatmap)
    {
        bool success = true;
        List<IMultipartFormSection> survey = new List<IMultipartFormSection>();
        survey.Add(new MultipartFormDataSection("heatmap", heatmap));
        Debug.Log(heatMapPostURL);
        UnityWebRequest www = UnityWebRequest.Post(heatMapPostURL, survey);

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
