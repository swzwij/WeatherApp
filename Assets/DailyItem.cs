using DTT.UI.ProceduralUI;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DailyItem : MonoBehaviour
{
    [SerializeField]
    private RectTransform minTemperature;

    [SerializeField]
    private RectTransform maxTemperature;

    [SerializeField]
    private RectTransform rainRect;

    [SerializeField]
    private TMP_Text minTemperatureText;

    [SerializeField]
    private TMP_Text maxTemperatureText;

    [SerializeField]
    private RoundedImage background;

    [SerializeField]
    private Color baseColor;

    [SerializeField]
    private Color offColor;

    [SerializeField]
    private TMP_Text precipitaionSumText;

    [SerializeField]
    private RectTransform tempBar;

    [SerializeField]
    private TMP_Text timeText;

    public void Init(string time, float minTemp, float maxTemp, float precipitaionSum, float windspeed, int windDirection, float overallMaxTemp, float overallMinTemp, int genNumber, float precipitaionSumMin, float precipitaionSumMax)
    {
        minTemperatureText.text = $"{minTemp}";
        maxTemperatureText.text = $"{maxTemp}";

        SetBarHeight(minTemperature, minTemp, overallMinTemp, overallMaxTemp);
        SetBarHeight(maxTemperature, maxTemp, overallMinTemp, overallMaxTemp);

        SetBarHeight(rainRect, precipitaionSum, precipitaionSumMin, precipitaionSumMax);

        SetTempBar();

        background.color = genNumber % 2 == 0 ? baseColor : offColor;

        precipitaionSumText.text = $"{precipitaionSum}";

        timeText.text = time[^5..];
    }

    private void SetBarHeight(RectTransform temperatureTransform ,float temp, float minTemp, float maxTemp)
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

    private void SetTempBar()
    {
        Vector2 anchorMin = tempBar.anchorMin;
        Vector2 anchorMax = tempBar.anchorMax;

        anchorMin.y = minTemperature.anchorMin.y;
        anchorMax.y = maxTemperature.anchorMax.y;

        tempBar.anchorMin = anchorMin;
        tempBar.anchorMax = anchorMax;
    }
}
