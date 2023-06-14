using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static HourlyTemperatureData;

public class WeatherSystem : MonoBehaviour
{
    private void Awake()
    {
        GetHourlyTemperature();
    }

    private void GetHourlyTemperature()
    {
        Action<HourlyTemperatureData> onComplete = (response) =>
        {
            DateTime currentDateTime = DateTime.Now;

            HourlyData data = response.hourly;
            for (int i = 0; i < data.time.Length; i++)
            {
                string time = data.time[i][^5..];
                string timeHour = time[..2];
                int hour = int.Parse(timeHour);

                string dayString = data.time[i][^8..][..2];
                int dayInt = int.Parse(dayString);

                if(dayInt == currentDateTime.Day && hour < currentDateTime.Hour)
                    continue;

                print(time + " -- " + data.temperature_2m[i]);
            }
        };

        Action onFailure = () => Debug.LogError("Failed to get hourly temperature");
        

        APIManager.Instance.GetHourlyTemperature(52.64, 5.06, 1, "2023-06-14", "2023-06-15", onComplete, onFailure);
    }
}
