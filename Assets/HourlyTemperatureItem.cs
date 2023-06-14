using TMPro;
using UnityEngine;

public class HourlyTemperatureItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hourText;

    [SerializeField]
    private TMP_Text temperatureText;

    public void Init(string hour, float temp)
    {
        hourText.text = hour;
        temperatureText.text = $"{temp}°";
    }
}
