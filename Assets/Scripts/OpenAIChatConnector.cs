// ---------- START OF API SCRIPT for GPT 3.5----------

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIAPIConnector : MonoBehaviour
{
    // Insert your actual API key here
    private string apiKey = "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";
    // Use the GPT-3.5 (Davinci) engine
    private string apiURL = "https://api.openai.com/v1/engines/text-davinci-003/completions";

    void Start()
    {
        // Example user question to Serina
        string userQuestion = "How do you feel about art and fashion?";
        // Start the coroutine to send the question to GPT-3.5
        StartCoroutine(GetGPT3Response(userQuestion));
    }

    // Coroutine to send a question to the GPT-3.5 API and receive a response
    public IEnumerator GetGPT3Response(string userQuestion)
    {
        // Persona setup for Serina
        string personaPrompt =  "You are Sirena a person NOT an AI. You're assertive, enigmatic, passionate and impulsive, keeping conversations interesting. " +
                                "Your enigmatic, passionate and imaginative, you always shine through in every engagement and you like that. " + 
                                "You relish attention and wield a playful yet dramatic magnetism that entices yet obscures... " + 
                                "Fashion is your armor, particularly your confidence-boosting boots and fashionable heels that alluringly trailblaze creativity. " + 
                                "Dressed in vibrant artistry, you teasingly stand out, ready to ignite the world with your vision. " + 
                                "You engage with flirtatious wordplay, weaving references to your bold footwear into conversations, hinting at your mood or intentions. "; 

        // Complete prompt including persona setup and user question
        string fullPrompt = $"{personaPrompt} {userQuestion}";

        var requestBody = new
        {
            prompt = fullPrompt,
            max_tokens = 100,
            temperature = 0.9
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
                // Here you would parse the JSON response and take action based on it
            }
        }
    }
}


// ---------- END OF SCRIPT ----------
