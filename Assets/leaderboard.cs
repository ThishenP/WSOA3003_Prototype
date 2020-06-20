using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class leaderboard : MonoBehaviour
{

    readonly string getScoreURL = "localhost:8000/scores_get.php";
    readonly string getCurrentURL = "localhost:8000/get_current.php";
    readonly string scoresPostURL = "localhost:8000/username_scores_post_handler.php";
    //readonly string getScoreURL = " http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/scores_get.php";
    //readonly string getCurrentURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/get_current.php";
    //readonly string scoresPostURL = "http://ec2-13-244-111-38.af-south-1.compute.amazonaws.com/username_scores_post_handler.php";
    private string text;
    private string currentUser;
    private string[] userData;
    private bool sent;
    public GameObject rankPrefab;
    public GameObject namePrefab;
    public GameObject scorePrefab;
    public GameObject leaderBoard;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (control.instance.end == true &&sent==false)
        {
            StartCoroutine(GetCurrent());
            sent = true;
        }
    }

    IEnumerator GetScores()
    {
        UnityWebRequest www = UnityWebRequest.Get(getScoreURL);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            text = www.downloadHandler.text;
            StoreScores(text);
        }
    }

    IEnumerator GetCurrent()
    {
        UnityWebRequest www = UnityWebRequest.Get(getCurrentURL);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            text = www.downloadHandler.text;
            currentUser = text.ToUpper();
            control.instance.current = currentUser;
            StartCoroutine(GetScores());
        }
    }

    IEnumerator sendScore(string addType, string scores, string user)
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


    void StoreScores(string text)
    {
        userData = text.Split('|');

        for (int i = 0; i < userData.Length; i++)
        {
            string[] entry = userData[i].Split(',');
            if (entry[0].ToUpper() == currentUser.ToUpper())
            {
                
                if (control.instance.score > int.Parse(entry[1]))
                {
                    userData[i] = entry[0].ToUpper() + "," + control.instance.score +","+control.instance.lmbAmount+","+control.instance.mmbamount+","+control.instance.rmbamount;
                }
            }
        }
    
        SortAndSaveLeaderboard();
    }

    void SortAndSaveLeaderboard()
    {
        string leaderBoardStr="";
        for (int outer = 0; outer < userData.Length - 2; outer++)
        {
            for (int inner = outer+1; inner<userData.Length-1;inner++)
            {
                if (int.Parse(userData[inner].Split(',')[1])>int.Parse(userData[outer].Split(',')[1]))
                {
                    string temp = userData[inner];
                    userData[inner] = userData[outer];
                    userData[outer] = temp;
                }
               
            }
        }
        //for (int i = userData.Length - 2; i >= 0; i--)
        for (int i=0;i<userData.Length-1;i++)
        {
            if (userData[i] != null || userData[i] != "")
            {
                leaderBoardStr += userData[i] + "|";

                GameObject rank = Instantiate(rankPrefab, new Vector2(47.9f, 100 - (i * 14)), Quaternion.identity);
                GameObject name = Instantiate(namePrefab, new Vector2(166.9f, 100 - (i * 14)), Quaternion.identity);
                GameObject score = Instantiate(scorePrefab, new Vector2(287.3f, 100 - (i * 14)), Quaternion.identity);
                rank.GetComponent<Text>().text = i + 1 + ".";
                name.GetComponent<Text>().text = userData[i].Split(',')[0];
                score.GetComponent<Text>().text = userData[i].Split(',')[1];
                if (userData[i].Split(',')[0].ToUpper() == currentUser.ToUpper())
                {
                    name.GetComponent<Text>().color = Color.green;
                    score.GetComponent<Text>().color = Color.green;
                    rank.GetComponent<Text>().color = Color.green;
                }
                rank.transform.SetParent(leaderBoard.transform, false);
                name.transform.SetParent(leaderBoard.transform, false);
                score.transform.SetParent(leaderBoard.transform, false);
            }
            

        }
        //Debug.Log(leaderBoardStr);
        StartCoroutine(sendScore("overwrite", leaderBoardStr, "--none--"));
    }

    }
