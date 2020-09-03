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
    //     https://docs.google.com/spreadsheets/d/1cK1PGkBKDU-Fo6dLv8e1onFvy0m02ja3UFQIgcwgJgA/edit?usp=sharing

    public string documentId = "1SKBWUcdgmPfcCn9k0BZapoHY8JdBOi_0t8OTA4drjRg";
    public string idleModeSheetName = "Donald_Trump";
    public Cell idleModeCellRange;
    public string apiKey;

    [Multiline]
    public string resultJson;
    public string[] result;

    public void Start()
    {
        //LoadSheet();
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

        GameObject obj = new GameObject();

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
            Debug.Log("Ok: " + request.responseCode);
            try
            {
                resultJson = request.downloadHandler.text;

                Dictionary<string, string> loadedData = JsonUtility.FromJson<Dictionary<string, string>>(resultJson);

                Debug.Log(loadedData.Count);

                foreach (KeyValuePair<string, string> kvp in loadedData)
                {
                    Debug.LogFormat("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }

                //result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text, defaultJsonSerializationSetting);

            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

        }
    }

     
}
