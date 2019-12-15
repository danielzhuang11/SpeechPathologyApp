using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class CSVDownloader
{
    private const string k_googleSheetDocID = "16hUFM5vrmSGMeb5hK3v0sFZpmMMOWpsEy9__-Mm78BE";
    private const string url = "https://docs.google.com/spreadsheets/d/" + k_googleSheetDocID + "/export?format=csv";
    internal static IEnumerator DownloadData(System.Action<string> onCompleted)
    {
        yield return new WaitForEndOfFrame();

        string downloadData = null;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Download Error: " + webRequest.error);
                downloadData = PlayerPrefs.GetString("LastDataDownloaded", null);
                string versionText = PlayerPrefs.GetString("LastDataDownloaded", null);
                Debug.Log("Using stale data version: " + versionText);
            }
            else
            {
                Debug.Log("Download success");
                Debug.Log("Data: " + webRequest.downloadHandler.text);

                // First term will be preceeded by version number, e.g. "100=English"
                string versionSection = webRequest.downloadHandler.text.Substring(0, 5);
                int equalsIndex = versionSection.IndexOf('=');
                UnityEngine.Assertions.Assert.IsFalse(equalsIndex == -1, "Could not find a '=' at the start of the CVS");

                string versionText = webRequest.downloadHandler.text.Substring(0, equalsIndex);
                Debug.Log("Downloaded data version: " + versionText);

                PlayerPrefs.SetString("LastDataDownloaded", webRequest.downloadHandler.text);
                PlayerPrefs.SetString("LastDataDownloadedVersion", versionText);

                downloadData = webRequest.downloadHandler.text.Substring(equalsIndex + 1);
            }
        }

        onCompleted(downloadData);
    }
}