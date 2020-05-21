using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeatmapManipulation : MonoBehaviour
{
    private int i, j;
    private float value;
    
    readonly string getURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/stats_get.php";
    //readonly string getURL = "localhost:8000/stats_get.php";
    private string text;
    private float highest = 0;
    private float[,] heatMapArray = new float[8, 16];
    void Start()
    {
        StartCoroutine(GetStats()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetStats()
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
            GetHeatmapFromText(text);
        }
    }
    
    void GetHeatmapFromText(string text)
    {
       // Debug.Log(text);
        string[] HeatMaps = text.Split('|');
        foreach(var HeatMap in HeatMaps)
        {
            string[] entries = HeatMap.Split('\n');
            
            foreach(var entry in entries)
            {
               

                if ( entry != "")
                {
                       
                    i = int.Parse(entry.Substring(1, 1));
                    if (entry.Substring(4,1)==")")
                    {
                        j = int.Parse(entry.Substring(3, 1));
                        value = float.Parse(entry.Substring(6));
                    }
                    else
                    {
                        j = int.Parse(entry.Substring(3, 2));
                        value = float.Parse(entry.Substring(7));
                    }

                    heatMapArray[i, j] += value;
                  
                }
            }
        }
        for (int i = 0; i < heatMapArray.GetLength(0); i++)
        {
            for (int j = 0; j < heatMapArray.GetLength(1); j++)
            {
                if (highest < heatMapArray[i, j])
                {
                    highest = heatMapArray[i, j];
                }
            }
        }

        for (int i = 0; i < heatMapArray.GetLength(0); i++)
        {
            for (int j = 0; j < heatMapArray.GetLength(1); j++)
            {
                heatMapArray[i, j] = heatMapArray[i, j] / highest;
                
            }
        }
        control.instance.overAllHeatMap = heatMapArray;

    }


}
