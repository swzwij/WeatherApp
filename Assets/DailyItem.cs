using TMPro;
using UnityEngine;

public class DailyItem : MonoBehaviour
{
    [SerializeField]
    private RectTransform minTemperature;

    [SerializeField]
    private RectTransform maxTemperature;

    [SerializeField]
    private TMP_Text minTemperatureText;

    [SerializeField]
    private TMP_Text maxTemperatureText;

    public void Init(string time, float minTemp, float maxTemp, float precipitaionSum, float windspeed, int windDirection, float overallMaxTemp, float overallMinTemp)
    {
        minTemperatureText.text = $"{minTemp}";
        maxTemperatureText.text = $"{maxTemp}";

        SetTempHeight(minTemperature, minTemp, overallMinTemp, overallMaxTemp);
        SetTempHeight(maxTemperature, maxTemp, overallMinTemp, overallMaxTemp);
    }

    private void SetTempHeight(RectTransform temperatureTransform ,float temp, float minTemp, float maxTemp)
    {
        float range = maxTemp - minTemp;
        float pos = temp - minTemp;
        float newPos = (pos / range) * 1;

        Vector2 anchorMin = temperatureTransform.anchorMin;
        Vector2 anchorMax = temperatureTransform.anchorMax;

        anchorMin.y = newPos;
        anchorMax.y = newPos;

        temperatureTransform.anchorMin = anchorMin;
        temperatureTransform.anchorMax = anchorMax;
    }
}
