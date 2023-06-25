using SingletonBehaviour;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace WeatherApp.API
{
    /// <summary>
    /// Class to make API calls.
    /// </summary>
    public class APIManager : SingletonBehaviour<APIManager>
    {
        /// <summary>
        /// Sending info to an API and getting something back.
        /// </summary>
        /// <typeparam name="T">The data that the API sends back.</typeparam>
        /// <param name="request">The data that gets send to the API.</param>
        /// <param name="onComplete">Event for when the API call is complete.</param>
        /// <param name="onFailure">Event for when the API call fails.</param>
        public void GetCall<T>(APIRequest request, Action<T> onComplete, Action onFailure) =>
            StartCoroutine(WebRequest(request, onComplete, onFailure));

        /// <summary>
        /// Making the call to the API call.
        /// </summary>
        /// <typeparam name="T">The data that the API sends back.</typeparam>
        /// <param name="request">The data that gets send to the API.</param>
        /// <param name="onComplete">Event for when the API call is complete.</param>
        /// <param name="onFailure">Event for when the API call fails.</param>
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