using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeatherOverviewSystem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _temperatureText;

    [SerializeField]
    private TMP_Text _apperentTemperatureText;

    [SerializeField]
    private TMP_Text _precipitationProbabilityText;

    [SerializeField]
    private TMP_Text _precipitationText;

    [SerializeField]
    private TMP_Text _humidityText;

    [SerializeField]
    private TMP_Text _windspeedText;

    public void InitOverview(string apperentTemperature, string precipitationProbability, string precipitation, string humidity, string windSpeed)
    {
        _apperentTemperatureText.text = $"Feels like: {apperentTemperature}";
        _precipitationProbabilityText.text = precipitationProbability;
        _precipitationText.text = precipitation;
        _humidityText.text = humidity;
        _windspeedText.text = windSpeed;
    }

    public void InitTemperature(float temperature)
    {
        _temperatureText.text = $"{temperature}°";
    }
}
