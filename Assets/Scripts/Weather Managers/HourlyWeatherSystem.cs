using System;
using System.Collections.Generic;
using UnityEngine;
using WeatherApp.Utils;
using WeatherApp.API;

namespace WeatherApp.WeatherSystem
{
    public class HourlyWeatherSystem : MonoBehaviour
    {
        [SerializeField]
        private HourlyItem _hourlyTemperatureItem;

        [SerializeField]
        private HourlyItem _hourlyRainItem;

        [SerializeField]
        private Transform _contentTransform;

        private List<HourlyItem> rainItems = new();

        private List<HourlyItem> temperatureItems = new();

        public Action onGetHourlyRain;

        private void OnEnable()
        {
            GPSManager.Instance.onGetLocation += GetHourlyTemperature;
            GPSManager.Instance.onGetLocation += GetHourlyRain;
        }

        private void OnDisable() 
        { 
            GPSManager.Instance.onGetLocation -= GetHourlyTemperature;
            GPSManager.Instance.onGetLocation -= GetHourlyRain;
        }

        private void GetHourlyTemperature(LocationCoordinates location)
        {
            Action<HourlyTemperatureData> onComplete = (response) => HandleHourlyTemperatureResponse(response);
            Action onFailure = () => Debug.LogError("Failed to get hourly temperature");

            DateTime currentDateTime = DateTime.Now;
            DateTime TomorrowDateTime = DateTime.Now.AddDays(1);
            string today = DateUtils.FormatDate(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);
            string tomorrow = DateUtils.FormatDate(TomorrowDateTime.Year, TomorrowDateTime.Month, TomorrowDateTime.Day);

            HourlyTemperatureRequest request = new(location.Latitude, location.Longitude, today, tomorrow);
            APIManager.Instance.GetCall(request, onComplete, onFailure);
        }

        private void GetHourlyRain(LocationCoordinates location)
        {
            Action<HourlyRainData> onComplete = (response) => HandleHourlyRainResponse(response);
            Action onFailure = () => Debug.LogError("Failed to get hourly temperature");

            DateTime currentDateTime = DateTime.Now;
            DateTime TomorrowDateTime = DateTime.Now.AddDays(1);
            string today = DateUtils.FormatDate(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);
            string tomorrow = DateUtils.FormatDate(TomorrowDateTime.Year, TomorrowDateTime.Month, TomorrowDateTime.Day);

            HourlyRainRequest request  = new(location.Latitude, location.Longitude, today, tomorrow);
            APIManager.Instance.GetCall(request, onComplete, onFailure);
        }

        private void HandleHourlyTemperatureResponse(HourlyTemperatureData response)
        {
            DateTime currentDateTime = DateTime.Now;

            float maxTemp = Mathf.NegativeInfinity;
            float minTemp = Mathf.Infinity;

            HourlyTemperatureData.HourlyData data = response.hourly;

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

                if (dayInt == currentDateTime.Day && hour < currentDateTime.Hour || dayInt != currentDateTime.Day && hour > currentDateTime.Hour)
                    continue;

                float temp = data.temperature_2m[i];

                HourlyItem item = Instantiate(_hourlyTemperatureItem, _contentTransform);
                item.Init(time, temp, data.weathercode[i], minTemp, maxTemp, i);
                temperatureItems.Add(item);
            }
        }

        private void HandleHourlyRainResponse(HourlyRainData response)
        {
            DateTime currentDateTime = DateTime.Now;

            float maxTemp = Mathf.NegativeInfinity;
            float minTemp = Mathf.Infinity;

            HourlyRainData.HourlyData data = response.hourly;

            for (int i = 0; i < data.time.Length; i++)
            {
                float temp = data.precipitation[i];

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

                if (dayInt == currentDateTime.Day && hour < currentDateTime.Hour || dayInt != currentDateTime.Day && hour > currentDateTime.Hour)
                    continue;

                float temp = data.precipitation[i];

                HourlyItem item = Instantiate(_hourlyRainItem, _contentTransform);
                item.Init(time, temp, data.weathercode[i], minTemp, maxTemp, i);
                rainItems.Add(item);

                onGetHourlyRain?.Invoke();
            }
        }

        public void DisableTempItems(bool disable)
        {
            for (int i = 0; i < temperatureItems.Count; i++)
            {
                temperatureItems[i].gameObject.SetActive(!disable);
            }

            for (int i = 0; i < rainItems.Count; i++)
            {
                rainItems[i].gameObject.SetActive(disable);
            }
        }
    }
}