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

// ---------- END OF SCRIP T ----------



using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIAPIConnector : MonoBehaviour
{
    private string apiKey = "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";
    private string apiURL = "https://api.openai.com/v1/engines/gpt-4/completions";

    // Function to initiate communication with "Uplift Quartet Chat"
    public IEnumerator GetResponseFromUpliftQuartetChat(string prompt)
    {
        // Adjust the requestBody to include parameters specific to "Uplift Quartet Chat"
        // For example, specifying the model or persona if that's supported by your setup
        var requestBody = new
        {
            model = "your-uplift-quartet-chat-model-id", // Replace with the actual model ID if applicable
            prompt = $"[Sassy]: {prompt}", // Adjust this to how you differentiate personas within your GPT
            max_tokens = 100,
            temperature = 0.7 // Adjust based on how creative you want the responses to be
        };

        // Example of calling GetGPT3Response with a user question
        StartCoroutine(GetGPT3Response("What's your opinion on artificial intelligence?"));

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
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
}



