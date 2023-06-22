using DTT.UI.ProceduralUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HourlyRainItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hourText;

    [SerializeField]
    private TMP_Text rainText;

    [SerializeField]
    private TMP_Text weatherSummaryText;

    [SerializeField]
    private RectTransform RainTransform;

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

    public void Init(string hour, float rain, int weatherCode, float minRain, float maxRain, int genNumber)
    {
        hourText.text = hour;
        rainText.text = $"{rain}mm";

        background.color = genNumber % 2 == 0 ? baseColor : offColor;

        SetRainHeight(rain, minRain, maxRain);
    }

    private void SetRainHeight(float rain, float minRain, float maxRain)
    {
        float range = maxRain - minRain;
        float pos = rain - minRain;
        float newPos = (pos / range) * 1;

        Vector2 anchorMin = RainTransform.anchorMin;
        Vector2 anchorMax = RainTransform.anchorMax;

        anchorMin.y = newPos;
        anchorMax.y = newPos;

        RainTransform.anchorMin = anchorMin;
        RainTransform.anchorMax = anchorMax;
    }
}
