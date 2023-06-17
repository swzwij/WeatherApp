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
        string url = $"latitude={latitude}&longitude={longitude}&hourly=temperature_2m,weathercode&forecast_days={1}&start_date={startDate}&end_date={endDate}";
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

    public void GetDailyData(double latitude, double longitude, string startDate, string endDate, Action<DailyData> onComplete = null, Action onFailure = null)
    {
        string url = $"latitude={latitude}&longitude={longitude}&daily=weathercode,temperature_2m_max,temperature_2m_min,precipitation_sum,windspeed_10m_max,winddirection_10m_dominant&start_date={startDate}&end_date={endDate}&timezone=Europe%2FBerlin";
        StartCoroutine(DailyDataCall(_baseURL + url, onComplete, onFailure));
    }

    private IEnumerator DailyDataCall(string url, Action<DailyData> onComplete, Action onFailure)
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

        DailyData response = JsonUtility.FromJson<DailyData>(request.downloadHandler.text);
        onComplete?.Invoke(response);
    }

    public void GetLocationFromCoordinates(LocationCoordinates location, Action<LocationData> onComplete, Action onFailure)
    {
        string url = $"https://geocode.maps.co/reverse?lat={location.Latitude}&lon={location.Longitude}";
        StartCoroutine(LocationFromCoordinatesCall(url, onComplete, onFailure));
    }

    private IEnumerator LocationFromCoordinatesCall(string url, Action<LocationData> onComplete, Action onFailure)
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

        LocationData response = JsonUtility.FromJson<LocationData>(request.downloadHandler.text);
        onComplete?.Invoke(response);
    }
}
