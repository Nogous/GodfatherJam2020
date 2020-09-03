using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetGoogleShit : MonoBehaviour
{
    struct Cell
    {
        public string startCell;
        public string endCell;
    }

    string documentId;
    string idleModeSheetName;
    Cell idleModeCellRange;
    string apiKey;

    void GetShit()
    {
        /*
        string url = "https://sheets.googleapis.com/v4/spreadsheets/" + documentId + "/values/" + idleModeSheetName + "!" + idleModeCellRange.startCell + ":" + idleModeCellRange.endCell + "?majorDimension=ROWS&key=" + apiKey;
        UnityWebRequest request = new UnityWebRequest(url);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.method = UnityWebRequest.kHttpVerbGET;
        request.SetRequestHeader("content-type", "application/json; charset=utf-8");
        request.SetRequestHeader("accept", "application/json;");

        GameObject obj = new GameObject();
        NetworkHelper networkHelper = obj.AddComponent<NetworkHelper>();
        networkHelper.ExecuteRequest<BaseResponse>(request, (result) => {
            if (result.error == null)
            {
                SheetData sheetData = JsonConvert.DeserializeObject<SheetData>(result.rawResponseData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                foreach (List<string> entry in sheetData.values)
                {
                    id = int.Parse(entry[0]);
                    //...
                }

                string json = JsonConvert.SerializeObject(idleModeLootData, new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                string folder = Application.dataPath + BackendExporter.exportedJsoPath;

                if (Directory.Exists(folder) == false)
                {
                    Directory.CreateDirectory(folder);
                }

                File.WriteAllText(folder + "/IdleModeLootData.json", json);
            }
            else
            {
                EditorUtility.DisplayDialog("Idle Mode Loot Data Import", "Error!", result.error);
            }
        */
        }
}
