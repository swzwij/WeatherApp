using DTT.UI.ProceduralUI;
using System;
using TMPro;
using UnityEngine;
using WeatherApp.API;
using WeatherApp.Utils;
using WeatherApp.Location;

namespace WeatherApp.WeatherSystem
{
    /// <summary>
    /// Class to handle the daylight cycle.
    /// </summary>
    public class DaylightCycle : MonoBehaviour
    {
        /// <summary>
        /// Reference to the sun rect.
        /// </summary>
        [SerializeField]
        private RectTransform _sunRect;

        /// <summary>
        /// Reference to the circle rect.
        /// </summary>
        [SerializeField]
        private RectTransform _circleRect;

        /// <summary>
        /// Reference to the sunrise rect.
        /// </summary>
        [SerializeField]
        private RectTransform _sunriseRect;

        /// <summary>
        /// Reference to the sunset rect.
        /// </summary>
        [SerializeField]
        private RectTransform _sunsetRect;

        /// <summary>
        /// Reference to teh sunrise text.
        /// </summary>
        [SerializeField]
        private TMP_Text _sunriseText;

        /// <summary>
        /// Reference to the sunset text.
        /// </summary>
        [SerializeField]
        private TMP_Text _sunsetText;

        /// <summary>
        /// The color of the day.
        /// </summary>
        [SerializeField]
        private Color _dayColor;

        /// <summary>
        /// The color of the night.
        /// </summary>
        [SerializeField]
        private Color _nightColor;

        /// <summary>
        /// Reference to the sun image.
        /// </summary>
        [SerializeField]
        private RoundedImage _sunImage;

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        private const float CIRCLE_RADIUS = 362.5f;

        /// <summary>
        /// The value of the sunrise position.
        /// </summary>
        private float _sunriseValue;

        /// <summary>
        /// The value of the sunset position.
        /// </summary>
        private float _sunsetValue;

        /// <summary>
        /// Setting the onGetLocation listener.
        /// </summary>
        private void OnEnable() => GPSManager.Instance.onGetLocation += GetDaylightData;

        /// <summary>
        /// Remove the onGetLocation listener.
        /// </summary>
        private void OnDisable() => GPSManager.Instance.onGetLocation -= GetDaylightData;
        
        /// <summary>
        /// Making the daylight API call and setting the data.
        /// </summary>
        /// <param name="location">The current location.</param>
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

        /// <summary>
        /// Convert the time into a circle position value.
        /// </summary>
        /// <param name="dateTimeString">The current date and time.</param>
        /// <returns>The circle position value.</returns>
        private int CovertTimeToCircleValue(string dateTimeString)
        {
            DateTime dateTime = DateTime.Parse(dateTimeString);

            int hour = dateTime.Hour;
            int minute = dateTime.Minute;

            return (hour * 60) + minute;
        }

        /// <summary>
        /// Set the sun time.
        /// </summary>
        /// <param name="dateTimeString">The current dat and time.</param>
        /// <returns>The time string.</returns>
        private string GetSunTime(string dateTimeString)
        {
            DateTime dateTime = DateTime.Parse(dateTimeString);

            int hour = dateTime.Hour;
            int minute = dateTime.Minute;

            string hourString = FixTimeFormat(hour);
            string minuteString = FixTimeFormat(minute);

            return $"{hourString}:{minuteString}";
        }

        /// <summary>
        /// Ficing the time format.
        /// </summary>
        /// <param name="value">Time int.</param>
        /// <returns>The string of the correct value.</returns>
        private string FixTimeFormat(int value)
        {
            string String = $"{value}";

            if (String.Length == 1)
                String = $"0{value}";

            return String;
        }

        /// <summary>
        /// Set the postion of the sun on the circle.
        /// </summary>
        /// <param name="sunTransform">The sun rect.</param>
        /// <param name="circleTransform">The circle rect.</param>
        private void SetSunPosition(RectTransform sunTransform, RectTransform circleTransform)
        {
            DateTime currentTime = DateTime.Now;

            int totalMinutes = (currentTime.Hour * 60) + currentTime.Minute;
            float newMinutes = ((float)totalMinutes / 1440) * 100;

            _sunImage.color = newMinutes > _sunsetValue && newMinutes < _sunriseValue ? _nightColor : _dayColor;

            SetUIObjectPosition(newMinutes, circleTransform, sunTransform, CIRCLE_RADIUS);
        }

        /// <summary>
        /// Set a UI object on the circle.
        /// </summary>
        /// <param name="value">The value of where the object is going 0-100.</param>
        /// <param name="circleTransform">The circle rect.</param>
        /// <param name="objectTransform">The object rect.</param>
        /// <param name="radius">The radius of the circle.</param>
        public void SetUIObjectPosition(float value, RectTransform circleTransform, RectTransform objectTransform, float radius)
        {
            float angle = Mathf.Lerp(0f, 360f, value / 100f);
            float x = circleTransform.anchoredPosition.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = circleTransform.anchoredPosition.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            objectTransform.anchoredPosition = new Vector2(x, y);
        }
    }
}
