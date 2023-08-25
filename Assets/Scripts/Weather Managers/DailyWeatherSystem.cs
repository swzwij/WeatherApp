using System;
using UnityEngine;
using WeatherApp.API;
using WeatherApp.Utils;
using WeatherApp.Location;

namespace WeatherApp.WeatherSystem
{
    /// <summary>
    /// Class to handle the daily weather.
    /// </summary>
    public class DailyWeatherSystem : MonoBehaviour
    {
        /// <summary>
        /// Reference to the daily item prefab.
        /// </summary>
        [SerializeField]
        private DailyItem _dailyItem;

        /// <summary>
        /// Reference to the transform where the items get instantiated under.
        /// </summary>
        [SerializeField]
        private Transform _dailyContentTransform;

        /// <summary>
        /// Setting onGetLocation listener.
        /// </summary>
        private void OnEnable()
        {
            GPSManager.Instance.onGetLocation += GetDailyWeather;
            SearchButton.Instance.onSearchNewLocation += GetDailyWeather;

        }

        /// <summary>
        /// Removing onGetLocation listener.
        /// </summary>
        private void OnDisable()
        {
            GPSManager.Instance.onGetLocation -= GetDailyWeather;
            SearchButton.Instance.onSearchNewLocation -= GetDailyWeather;
        }

        /// <summary>
        /// Making the call to the API and getting the daily weather.
        /// </summary>
        /// <param name="location">The current location.</param>
        private void GetDailyWeather(LocationCoordinates location)
        {
            Action<DailyData> onComplete = (response) => HandleDailyReponse(response);
            Action onFailure = () => Debug.LogError("Failed to get daily data");

            DateTime currentDateTime = DateTime.Now;
            DateTime twoWeeksDateTime = DateTime.Now.AddDays(14);
            string today = DateUtils.FormatDate(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);
            string twoWeeks = DateUtils.FormatDate(twoWeeksDateTime.Year, twoWeeksDateTime.Month, twoWeeksDateTime.Day);

            DailyRequest request = new(location.Latitude, location.Longitude, today, twoWeeks);
            APIManager.Instance.GetCall(request, onComplete, onFailure);
        }

        /// <summary>
        /// Handle the data from the API.
        /// </summary>
        /// <param name="response">The data from the API.</param>
        private void HandleDailyReponse(DailyData response)
        {
            DailyData.DailyTemperatureData data = response.daily;

            float maxTemp = Mathf.NegativeInfinity;
            float minTemp = Mathf.Infinity;

            float minRain = Mathf.Infinity;
            float maxRain = Mathf.NegativeInfinity;

            for (int i = 0; i < data.time.Length; i++)
            {
                float newMaxtemp = data.temperature_2m_max[i];
                float newMinTemp = data.temperature_2m_min[i];

                if (newMaxtemp > maxTemp)
                    maxTemp = newMaxtemp;
                if (newMinTemp < minTemp)
                    minTemp = newMinTemp;

                float newMinRain = data.precipitation_sum[i];
                float newMaxRain = data.precipitation_sum[i];

                if (newMaxRain > maxRain)
                    maxRain = newMaxRain;
                if (newMinRain < minRain)
                    minRain = newMinRain;
            }

            for (int i = 0; i < data.time.Length; i++)
            {
                DailyItem item = Instantiate(_dailyItem, _dailyContentTransform);
                item.Init(data.time[i], data.temperature_2m_min[i], data.temperature_2m_max[i], data.precipitation_sum[i], data.windspeed_10m_max[i], data.winddirection_10m_dominant[i], maxTemp, minTemp, i, minRain, maxRain, data.weathercode[i]);
            }
        }
    }

}