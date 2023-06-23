using SingletonBehaviour;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace WeatherApp.API
{
    public class new_APIManager : SingletonBehaviour<new_APIManager>
    {
        public void GetCall<T>(APIRequest request, Action<T> onComplete, Action onFailure) =>
            StartCoroutine(WebRequest(request, onComplete, onFailure));
        
        private IEnumerator WebRequest<T>(APIRequest request, Action<T> onComplete, Action onFailure)
        {
            UnityWebRequest webRequest = new(request.URL)
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                onFailure?.Invoke();
                yield break;
            }

            T response = JsonUtility.FromJson<T>(webRequest.downloadHandler.text);
            onComplete?.Invoke(response);
        }
    }
}