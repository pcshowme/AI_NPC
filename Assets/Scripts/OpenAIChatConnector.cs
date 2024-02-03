using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIAPIConnector : MonoBehaviour
{
    private string apiKey = "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";
    private string apiURL = "https://api.openai.com/v1/engines/gpt-4/completions";

    // Function to send a prompt to the GPT-3 API and receive a response
    public IEnumerator GetGPT3Response(string prompt)
    {
        var requestBody = new
        {
            prompt = prompt,
            max_tokens = 100
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

