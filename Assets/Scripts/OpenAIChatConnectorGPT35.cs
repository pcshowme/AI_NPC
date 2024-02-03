// ---------- START OF API SCRIPT for GPT 3.5----------

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIAPIConnector : MonoBehaviour
{
    private string apiKey = "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";
    private string apiURL = "https://api.openai.com/v1/engines/text-davinci-003/completions"; // URL for GPT-3.5

    // Function to send a prompt to the GPT-3.5 API and receive a response
    public IEnumerator GetGPT3Response(string prompt)
    {
        var requestBody = new
        {
            prompt = prompt,
            max_tokens = 100,
            temperature = 0.9 // 0 is the most consistant setting / 1 is the most random/creativity
        };

        string jsonBody = JsonUtility.ToJson(requestBody);
        
        using (UnityWebRequest webRequest = new UnityWebRequest(apiURL, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                // Process the response
                Debug.Log(webRequest.downloadHandler.text);
                // Parse the JSON response and take action based on it
            }
        }
    }
}

// ---------- END OF SCRIPT ----------