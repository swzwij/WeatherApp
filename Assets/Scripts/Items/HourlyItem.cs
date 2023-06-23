using DTT.UI.ProceduralUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class HourlyItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _hourText;

    [SerializeField]
    private TMP_Text _valueText;

    [Space]
    [SerializeField]
    private RectTransform _valueTransform;

    [SerializeField]
    private string _valueSuffix;

    [SerializeField]
    private Image _weatherImage;

    [SerializeField]
    private RoundedImage _background;

    [Space]
    [SerializeField]
    private Color _baseColor;

    [SerializeField]
    private Color _offColor;

    protected virtual void SetValue(float value, float minValue, float maxValue)
    {
        float range = maxValue - minValue;
        float pos = value - minValue;
        float newPos = (pos / range) * 1;

        Vector2 anchorMin = _valueTransform.anchorMin;
        Vector2 anchorMax = _valueTransform.anchorMax;

        anchorMin.y = newPos;
        anchorMax.y = newPos;

        _valueTransform.anchorMin = anchorMin;
        _valueTransform.anchorMax = anchorMax;
    }

    public void Init(string hour, float value, int weatherCode, float minValue, float maxValue, int genNumber)
    {
        _hourText.text = hour;
        _valueText.text = $"{value}{_valueSuffix}";

        SetValue(value, minValue, maxValue);

        _background.color = genNumber % 2 == 0 ? _baseColor : _offColor;
    }
}
