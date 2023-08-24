using System;
using TMPro;
using UnityEngine;
using WeatherApp.API;
using WeatherApp.Location;
using WeatherApp.Utils;

public class CurrentWeatherSystem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _humidityText;

    [SerializeField]
    private TMP_Text _dewpointText;

    [SerializeField]
    private TMP_Text _apperentTemperatureText;

    [SerializeField]
    private TMP_Text _precipitationProbabilityText;

    [SerializeField]
    private TMP_Text _precipitationText;

    [SerializeField]
    private TMP_Text _cloudcoverText;

    [SerializeField]
    private TMP_Text _visibilityText;

    [SerializeField]
    private TMP_Text _windSpeedText;

    [SerializeField]
    private TMP_Text _windDirectionText;

    [SerializeField]
    private WeatherOverviewSystem _weatherOverviewSystem;

    private void OnEnable()
    {
        GPSManager.Instance.onGetLocation += GetCurrentWeather;
        SearchButton.Instance.onSearchNewLocation += GetCurrentWeather;
    }

    private void OnDisable()
    {
        GPSManager.Instance.onGetLocation -= GetCurrentWeather;
        SearchButton.Instance.onSearchNewLocation -= GetCurrentWeather;
    }

    private void GetCurrentWeather(LocationCoordinates location)
    {
        Action<CurrentWeatherData> onComplete = (response) => HandleCurrentWeatherResponse(response);
        Action onFailure = () => Debug.LogError("Failed to get current weather");

        CurrentWeatherRequest request = new(location.Latitude, location.Longitude);
        APIManager.Instance.GetCall(request, onComplete, onFailure);
    }

    private void HandleCurrentWeatherResponse(CurrentWeatherData response)
    {
        CurrentWeatherData.HourlyData data = response.hourly;

        DateTime currentTime = DateTime.Now;
        int currentHour = currentTime.Hour;

        for (int i = 0; i < data.time.Length; i++)
        {
            string[] split = data.time[i].Split('T');
            string[] timeParts = split[1].Split(':');
            string hourString = timeParts[0];
            int hourInt = int.Parse(hourString);

            if (hourInt != currentHour)
                continue;

            string apperentTemperature = $"{data.apparent_temperature[i]}°";
            string precipitationProbability = $"{data.precipitation_probability[i]}%";
            string precipitation = $"{data.precipitation[i]}mm";
            string humidity = $"{data.relativehumidity_2m[i]}%";
            string windSpeed = $"{data.windspeed_10m[i]}km/h";

            _humidityText.text = humidity;
            _dewpointText.text = $"{data.dewpoint_2m[i]}°";
            _apperentTemperatureText.text = apperentTemperature;
            _precipitationProbabilityText.text = precipitationProbability;
            _precipitationText.text = precipitation;
            _cloudcoverText.text = $"{data.cloudcover[i]}%";
            _visibilityText.text = $"{data.visibility[i]}km";
            _windSpeedText.text = windSpeed;
            _windDirectionText.text = $"{data.winddirection_10m[i]}°";

            _weatherOverviewSystem.InitOverview(apperentTemperature, precipitationProbability, precipitation, humidity, windSpeed);
        }
    }
}
