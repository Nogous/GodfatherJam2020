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

    // https://docs.google.com/spreadsheets/d/1SKBWUcdgmPfcCn9k0BZapoHY8JdBOi_0t8OTA4drjRg/edit?usp=sharing

    // tmp https://docs.google.com/spreadsheets/d/1cK1PGkBKDU-Fo6dLv8e1onFvy0m02ja3UFQIgcwgJgA/edit?usp=sharing

    public string documentId = "1SKBWUcdgmPfcCn9k0BZapoHY8JdBOi_0t8OTA4drjRg";
    public string idleModeSheetName = "Donald_Trump";
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
            #region test

            string firstSelection = request.downloadHandler.text.Substring(0,5);
            int index = firstSelection.IndexOf('=');
            Debug.Log(index);

            #endregion

            Debug.Log("Ok: " + request.responseCode);
            try
            {
                resultJson = request.downloadHandler.text;

                //Dictionary<string, string> loadedData = JsonUtility.FromJson<Dictionary<string, string>>(resultJson);

                //GoogleSheetData data = new GoogleSheetData();
                //data.values = new string[10, 10];

                //string json = JsonUtility.ToJson(data);
                //Debug.Log(json);

                SheetData sheetData = JsonConvert.DeserializeObject<SheetData>(resultJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                //result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text, defaultJsonSerializationSetting);

            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

        }
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