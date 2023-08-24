using System;
using System.Collections.Generic;
using UnityEngine;
using WeatherApp.Utils;
using WeatherApp.API;
using WeatherApp.Location;

namespace WeatherApp.WeatherSystem
{
    /// <summary>
    /// Class to handle the hourly weather.
    /// </summary>
    public class HourlyWeatherSystem : MonoBehaviour
    {
        /// <summary>
        /// Reference to the hourly item prefab.
        /// </summary>
        [SerializeField]
        private HourlyItem _hourlyTemperatureItem;

        /// <summary>
        /// Reference to the hourly rain item prefab.
        /// </summary>
        [SerializeField]
        private HourlyItem _hourlyRainItem;

        /// <summary>
        /// Reference to the transform where the items get instantiated under.
        /// </summary>
        [SerializeField]
        private Transform _contentTransform;

        /// <summary>
        /// List of rain items.
        /// </summary>
        private List<HourlyItem> _rainItems = new();

        /// <summary>
        /// List of temperature items.
        /// </summary>
        private List<HourlyItem> _temperatureItems = new();

        /// <summary>
        /// Event for when the item have been gotten.
        /// </summary>
        public Action onGetHourlyRain;

        /// <summary>
        /// Setting the onGetLocation references.
        /// </summary>
        private void OnEnable()
        {
            GPSManager.Instance.onGetLocation += GetHourlyTemperature;
            GPSManager.Instance.onGetLocation += GetHourlyRain;

            SearchButton.Instance.onSearchNewLocation += GetHourlyTemperature;
            SearchButton.Instance.onSearchNewLocation += GetHourlyRain;
        }

        /// <summary>
        /// Removing the onGetLocation references.
        /// </summary>
        private void OnDisable() 
        { 
            GPSManager.Instance.onGetLocation -= GetHourlyTemperature;
            GPSManager.Instance.onGetLocation -= GetHourlyRain;

            SearchButton.Instance.onSearchNewLocation -= GetHourlyTemperature;
            SearchButton.Instance.onSearchNewLocation -= GetHourlyRain;
        }

        /// <summary>
        /// Making the API call to get the temperature data.
        /// </summary>
        /// <param name="location">The current location.</param>
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

        /// <summary>
        /// Making the API call to get the rain data.
        /// </summary>
        /// <param name="location">The current location.</param>
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

        /// <summary>
        /// Handle the hourly temperature data.
        /// </summary>
        /// <param name="response">The API data.</param>
        private void HandleHourlyTemperatureResponse(HourlyTemperatureData response)
        {
            DateTime currentDateTime = DateTime.Now;

            float maxTemp = Mathf.NegativeInfinity;
            float minTemp = Mathf.Infinity;

            HourlyTemperatureData.HourlyData data = response.hourly;

            for (int i = 0; i < data.time.Length; i++)
            {
                float temp = data.temperature_2m[i];

                string time = data.time[i][^5..];
                if (IsPastDate(currentDateTime, time, data.time[i][^8..][..2]))
                    continue;

                if (temp > maxTemp)
                    maxTemp = temp;
                if (temp < minTemp)
                    minTemp = temp;
            }

            for (int i = 0; i < data.time.Length; i++)
            {
                string time = data.time[i][^5..];
                if (IsPastDate(currentDateTime, time, data.time[i][^8..][..2]))
                    continue;

                float temp = data.temperature_2m[i];

                HourlyItem item = Instantiate(_hourlyTemperatureItem, _contentTransform);
                item.Init(time, temp, data.weathercode[i], minTemp, maxTemp, i);
                _temperatureItems.Add(item);
            }
        }

        private bool IsPastDate(DateTime currentDateTime, string time, string day)
        {
            string timeHour = time[..2];
            int hour = int.Parse(timeHour);

            int dayInt = int.Parse(day);

            return dayInt == currentDateTime.Day && hour < currentDateTime.Hour || dayInt != currentDateTime.Day && hour > currentDateTime.Hour;
        }

        /// <summary>
        /// Handle the rain temperature data.
        /// </summary>
        /// <param name="response">The API data.</param>
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
                _rainItems.Add(item);

                onGetHourlyRain?.Invoke();
            }
        }

        /// <summary>
        /// Disabling the temperature items.
        /// </summary>
        /// <param name="disable">Whether or not it should be disabled.</param>
        public void DisableTempItems(bool disable)
        {
            for (int i = 0; i < _temperatureItems.Count; i++)
                _temperatureItems[i].gameObject.SetActive(!disable);

            for (int i = 0; i < _rainItems.Count; i++)
                _rainItems[i].gameObject.SetActive(disable);
        }
    }
}