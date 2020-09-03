using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode]
public class GetGoogleShit : MonoBehaviour
{
    [System.Serializable]
    public struct Cell
    {
        public string startCell;
        public string endCell;
    }

    #region
    public static GetGoogleShit Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Instance of GetGoogleShit allready exist.");
            Destroy(gameObject);
        }
    }
    #endregion


    // tmp https://docs.google.com/spreadsheets/d/1cK1PGkBKDU-Fo6dLv8e1onFvy0m02ja3UFQIgcwgJgA/edit?usp=sharing

    public string documentId = "1SKBWUcdgmPfcCn9k0BZapoHY8JdBOi_0t8OTA4drjRg";
    public string idleModeSheetName = "Donald Trump";
    public Cell idleModeCellRange;
    public string apiKey;

    [Multiline]
    public string resultJson;
    public string[] result;

    public void Start()
    {
        LoadSheet();
    }

    [ContextMenu("GetShit")]
    public void LoadSheet()
    {
        StartCoroutine("GetShit");
    }
    IEnumerator GetShit()
    {
        string url = "https://sheets.googleapis.com/v4/spreadsheets/" + documentId + "/values/" + idleModeSheetName + "!" + idleModeCellRange.startCell + ":" + idleModeCellRange.endCell + "?majorDimension=ROWS&key=" + apiKey;

        UnityWebRequest request = new UnityWebRequest(url);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.method = UnityWebRequest.kHttpVerbGET;
        request.SetRequestHeader("content-type", "application/json; charset=utf-8");
        request.SetRequestHeader("accept", "application/json;");

        yield return request.SendWebRequest();

        List<TwitteData> twitteDatas = new List<TwitteData>();

        if (request.isNetworkError)
        {
            Debug.Log("Error");
        }
        else if (request.isHttpError)
        {
            Debug.Log("Error: " + request.responseCode);
        }
        else
        {

            try
            {
                resultJson = request.downloadHandler.text;

                SheetData sheetData = JsonConvert.DeserializeObject<SheetData>(resultJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                for (int i = sheetData.values.Count;i-->0;)
                {
                    TwitteType type = TwitteType.Normal;

                    switch (sheetData.values[i][2])
                    {
                        case "critique négative":
                            type = TwitteType.CritNegative;
                            break;
                        case "critique positive":
                            type = TwitteType.CritPositive;
                            break;
                        case "insulte":
                            type = TwitteType.Insulte;
                            break;
                        case "compliment":
                            type = TwitteType.Compliment;
                            break;
                        case "inutile":
                            type = TwitteType.Insulte;
                            break;
                    }

                    twitteDatas.Add(new TwitteData(sheetData.values[i][0], sheetData.values[i][1], type, int.Parse(sheetData.values[i][3]), int.Parse(sheetData.values[i][4])));
                }

            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

        }

        if(GameManager.Instance != null)
        GameManager.Instance.StartRound(twitteDatas);
    }

    private const string k_googleSheetDocID = "";
    private const string purl = "https://docs.google.com/d/" + k_googleSheetDocID+"?export?format=csv";

    /*
    internal static IEnumerator DownloadData(System.Action<string> onCompleted)
    {
        yield return new WaitForEndOfFrame();

        string downloadData = null;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(purl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Download Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Download success");
                Debug.Log("Data: " + webRequest.downloadHandler.text);

                string versionSelection = webRequest.downloadHandler.text.Substring(0, 5);
                int equalsIndex = versionSelection.IndexOf('=');
                UnityEngine.Assertions.Assert.IsFalse(equalsIndex == -1, "Could not found '='");

                string versionText = webRequest.downloadHandler.text.Substring(0, equalsIndex);
                Debug.Log("Downloaded data version " + versionText);

                //downloadData = webRequest.downloadHandler.text.Substring(equalsInedx + 1);
            }
        }
    }
    */
}

[System.Serializable]
public class SheetData
{
    public string range;
    public string majorDimension;
    public List<List<string>> values;
}