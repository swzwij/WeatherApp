using DTT.UI.ProceduralUI;
using System;
using TMPro;
using UnityEngine;
using WeatherApp.API;
using WeatherApp.Utils;

namespace WeatherApp.WeatherSystem
{
    public class DaylightCycle : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _sunRect;

        [SerializeField]
        private RectTransform _circleRect;

        [SerializeField]
        private RectTransform _sunriseRect;

        [SerializeField]
        private RectTransform _sunsetRect;

        [SerializeField]
        private TMP_Text _sunriseText;

        [SerializeField]
        private TMP_Text _sunsetText;

        [SerializeField]
        private Color _dayColor;

        [SerializeField]
        private Color _nightColor;

        [SerializeField]
        private RoundedImage _sunImage;

        private const float CIRCLE_RADIUS = 362.5f;

        private float _sunriseValue;

        private float _sunsetValue;

        private void OnEnable() => GPSManager.Instance.onGetLocation += GetDaylightData;  

        private void OnDisable() => GPSManager.Instance.onGetLocation -= GetDaylightData;
        
        private void GetDaylightData(LocationCoordinates location)
        {
            Action<DaylightCycleData> onComplete = (response) =>
            {
                float sunriseValue = CovertTimeToCircleValue(response.daily.sunrise[0]);
                float sunsetValue = CovertTimeToCircleValue(response.daily.sunset[0]);

                _sunriseValue = (sunriseValue / 1440) * 100;
                _sunsetValue = (sunsetValue / 1440) * 100;

                SetUIObjectPosition(_sunriseValue, _circleRect, _sunriseRect, CIRCLE_RADIUS + 50f);
                SetUIObjectPosition(_sunsetValue, _circleRect, _sunsetRect, CIRCLE_RADIUS + 50f);

                _sunriseText.text = GetSunTime(response.daily.sunrise[0]);
                _sunsetText.text = GetSunTime(response.daily.sunset[0]);

                SetSunPosition(_sunRect, _circleRect);
            };

            Action onFailure = () => Debug.LogError("Failed to get daily data");

            DaylightCycleRequest request = new(location.Latitude, location.Longitude);
            APIManager.Instance.GetCall(request, onComplete, onFailure);
        }

        private int CovertTimeToCircleValue(string dateTimeString)
        {
            DateTime dateTime = DateTime.Parse(dateTimeString);

            int hour = dateTime.Hour;
            int minute = dateTime.Minute;

            return (hour * 60) + minute;
        }

        private string GetSunTime(string dateTimeString)
        {
            DateTime dateTime = DateTime.Parse(dateTimeString);

            int hour = dateTime.Hour;
            int minute = dateTime.Minute;

            string hourString = FixTimeFormat(hour);
            string minuteString = FixTimeFormat(minute);

            return $"{hourString}:{minuteString}";
        }

        private string FixTimeFormat(int value)
        {
            string String = $"{value}";

            if (String.Length == 1)
                String = $"0{value}";

            return String;
        }

        private void SetSunPosition(RectTransform sunTransform, RectTransform circleTransform)
        {
            DateTime currentTime = DateTime.Now;

            int totalMinutes = (currentTime.Hour * 60) + currentTime.Minute;
            float newMinutes = ((float)totalMinutes / 1440) * 100;

            _sunImage.color = newMinutes > _sunsetValue && newMinutes < _sunriseValue ? _nightColor : _dayColor;

            SetUIObjectPosition(newMinutes, circleTransform, sunTransform, CIRCLE_RADIUS);
        }

        public void SetUIObjectPosition(float value, RectTransform circleTransform, RectTransform objectTransform, float radius)
        {
            float angle = Mathf.Lerp(0f, 360f, value / 100f);
            float x = circleTransform.anchoredPosition.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = circleTransform.anchoredPosition.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            objectTransform.anchoredPosition = new Vector2(x, y);
        }
    }
}
