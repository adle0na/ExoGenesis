using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SunoAIClient : MonoBehaviour
{
    private const string API_URL = "https://api.aimlapi.com/v2/generate/audio/suno-ai/clip";
    private const string API_KEY = "Bearer b80711fcf274427bba5c2e28f1254759";

    private void Start()
    {
        GenerateTestClip();
    }

    // 클립 생성 요청 메서드
    public IEnumerator GenerateClip(string descriptionPrompt)
    {
        // 요청 데이터 준비
        var requestData = new
        {
            gpt_description_prompt = descriptionPrompt
        };

        string jsonData = JsonUtility.ToJson(requestData);

        using (UnityWebRequest request = new UnityWebRequest(API_URL, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 헤더 설정
            request.SetRequestHeader("Authorization", $"Bearer {API_KEY}");
            request.SetRequestHeader("Content-Type", "application/json");

            // 요청 전송
            yield return request.SendWebRequest();

            // 결과 처리
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Generated clip ids: " + request.downloadHandler.text);
                ProcessClipIds(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error generating clip: " + request.error);
            }
        }
    }

    private void ProcessClipIds(string jsonResponse)
    {
        // 클립 ID 추출
        Debug.Log("Clip Response: " + jsonResponse);
        // JSON 파싱 후 데이터 활용 가능
    }

    // 테스트 함수
    public void GenerateTestClip()
    {
        StartCoroutine(GenerateClip("A story about frogs."));
    }
}
