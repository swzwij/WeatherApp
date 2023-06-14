using SingletonBehaviour;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : SingletonBehaviour<APIManager>
{
    [SerializeField]
    private string _baseURL = "https://api.open-meteo.com/v1/forecast?";

    public void GetHourlyTemperature(double latitude, double longitude, int days, string startDate, string endDate, Action<HourlyTemperatureData> onComplete = null, Action onFailure = null)
    {
        string url = $"latitude={latitude}&longitude={longitude}&hourly=temperature_2m&forecast_days={1}&start_date={startDate}&end_date={endDate}";
        StartCoroutine(HourlyTemperatureCall(_baseURL + url, onComplete, onFailure));
    }

    private IEnumerator HourlyTemperatureCall(string url, Action<HourlyTemperatureData> onComplete, Action onFailure)
    {
        UnityWebRequest request = new(url)
        {
            downloadHandler = new DownloadHandlerBuffer()
        };

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onFailure?.Invoke();
            yield break;
        }

        HourlyTemperatureData response = JsonUtility.FromJson<HourlyTemperatureData>(request.downloadHandler.text);
        onComplete?.Invoke(response);
    }
}
