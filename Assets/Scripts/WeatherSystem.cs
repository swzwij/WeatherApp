using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static HourlyTemperatureData;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField]
    private HourlyTemperatureItem temperatureItem;

    [SerializeField]
    private Transform hourlyContent;

    private void Awake()
    {
        GetHourlyTemperature();
    }

    private void GetHourlyTemperature()
    {
        Action<HourlyTemperatureData> onComplete = (response) => HandleHourlyRespons(response);

        Action onFailure = () => Debug.LogError("Failed to get hourly temperature");

        DateTime currentDateTime = DateTime.Now;
        DateTime TomorrowDateTime = DateTime.Now.AddDays(1);
        string today = FormatDate(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);
        string tomorrow = FormatDate(TomorrowDateTime.Year, TomorrowDateTime.Month, TomorrowDateTime.Day);

        APIManager.Instance.GetHourlyTemperature(52.64, 5.06, 1, today, tomorrow, onComplete, onFailure);
    }

    private string FormatDate(int year, int month, int day)
    {
        string monthString = $"{month}";
        string dayString = $"{day}";

        if (monthString.Length == 1) 
            monthString = $"0{monthString}";

        if (dayString.Length == 1)
            dayString = $"0{dayString}";

        return $"{year}-{monthString}-{dayString}";
    } 
    
    private void HandleHourlyRespons(HourlyTemperatureData response)
    {
        DateTime currentDateTime = DateTime.Now;

        float maxTemp = Mathf.NegativeInfinity;
        float minTemp = Mathf.Infinity;

        HourlyData data = response.hourly;

        for (int i = 0; i < data.time.Length; i++)
        {
            float temp = data.temperature_2m[i];

            if (temp > maxTemp)
                maxTemp = temp;
            if (temp < minTemp)
                minTemp = temp;
        }

        for (int i = 0; i < data.time.Length; i++)
        {
            string time = data.time[i][^5..];
            string timeHour = time[..2];
            int hour = int.Parse(timeHour);

            string dayString = data.time[i][^8..][..2];
            int dayInt = int.Parse(dayString);

            if (dayInt == currentDateTime.Day && hour < currentDateTime.Hour)
                continue;

            float temp = data.temperature_2m[i];

            //print(time + " -- " + data.temperature_2m[i]);
            HourlyTemperatureItem item = Instantiate(temperatureItem, hourlyContent);
            item.Init(time, temp, data.weathercode[i], minTemp, maxTemp, i);

            
        }
    }
}
