using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIAPIConnector : MonoBehaviour
{
    private string apiKey = "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";
    private string apiURL = "https://api.openai.com/v1/chat/completions";

    void Start()
    {
        // Example user question to Serina
        string userQuestion = "Tell me a joke.";
        // Start the coroutine to send the question to GPT-3.5
        StartCoroutine(GetGPT3Response(userQuestion));
    }

    public IEnumerator GetGPT3Response(string userQuestion)
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = userQuestion }
            }
        };

        string jsonBody = JsonUtility.ToJson(requestBody, true);

        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(apiURL, jsonBody))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonBody));
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Process the response and take action based on it
            }
        }
    }
}