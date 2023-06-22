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
}