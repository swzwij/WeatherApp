using DTT.UI.ProceduralUI;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HourlyTemperatureItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hourText;

    [SerializeField]
    private TMP_Text temperatureText;

    [SerializeField]
    private TMP_Text weatherSummaryText;

    [SerializeField]
    private RectTransform TemperatureTransform;

    [SerializeField]
    private Image weatherImage;

    [SerializeField]
    private RoundedImage background;

    [SerializeField]
    private Color baseColor;

    [SerializeField]
    private Color offColor;

    [SerializeField]
    private Sprite[] weatherIcons;

    public void Init(string hour, float temp, int weatherCode, float minTemp, float maxTemp, int genNumber)
    {
        hourText.text = hour;
        temperatureText.text = $"{temp}°";

        background.color = genNumber % 2 == 0 ? baseColor : offColor;

        SetTempHeight(temp, minTemp, maxTemp);

        SetWeatherSummary(weatherCode);
    }

    private void SetTempHeight(float temp, float minTemp, float maxTemp)
    {
        float range = maxTemp - minTemp;
        float pos = temp - minTemp;
        float newPos = (pos / range) * 1;

        Vector2 anchorMin = TemperatureTransform.anchorMin;
        Vector2 anchorMax = TemperatureTransform.anchorMax;

        anchorMin.y = newPos;
        anchorMax.y = newPos;

        TemperatureTransform.anchorMin = anchorMin;
        TemperatureTransform.anchorMax = anchorMax;
    }

    private void SetWeatherSummary(int weatherCode)
    {
        string weatherSummary = string.Empty;

        weatherImage.sprite = weatherIcons[weatherCode];

        switch (weatherCode)
        {
            case 0:
                weatherSummary = "clear";
                break;
            case 1:
                weatherSummary = "partly cloudy";
                break;
            case 2:
                weatherSummary = "cloudy";
                break;
            case 3:
                weatherSummary = "dust";
                break;
            case 4:
                weatherSummary = "fog";
                break;
            case 5:
                weatherSummary = "drizzle";
                break;
            case 6:
                weatherSummary = "rain";
                break;
            case 7:
                weatherSummary = "snow";
                break;
            case 8:
                weatherSummary = "showers";
                break;
            case 9:
                weatherSummary = "thunderstorm";
                break;
        }
    }
}