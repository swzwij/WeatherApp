using DTT.UI.ProceduralUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeatherApp.Utils;

namespace WeatherApp.WeatherSystem
{
    /// <summary>
    /// Class to handle the daily item.
    /// </summary>
    public class DailyItem : MonoBehaviour
    {
        /// <summary>
        /// reference to the min temperature rect.
        /// </summary>
        [SerializeField]
        private RectTransform minTemperatureRect;

        /// <summary>
        /// Reference to the max temperature rect.
        /// </summary>
        [SerializeField]
        private RectTransform maxTemperatureRect;

        /// <summary>
        /// Reference to the rain rect.
        /// </summary>
        [SerializeField]
        private RectTransform rainRect;

        /// <summary>
        /// Reference to the text displaying the min temperature.
        /// </summary>
        [SerializeField]
        private TMP_Text minTemperatureText;

        /// <summary>
        /// Reference to the text displaying the max temperature.
        /// </summary>
        [SerializeField]
        private TMP_Text maxTemperatureText;

        /// <summary>
        /// Reference to the background.
        /// </summary>
        [SerializeField]
        private RoundedImage background;

        /// <summary>
        /// The base color of the item.
        /// </summary>
        [SerializeField]
        private Color baseColor;

        /// <summary>
        /// The off color of the item.
        /// </summary>
        [SerializeField]
        private Color offColor;

        /// <summary>
        /// Reference to the displaying the rain precipitation.
        /// </summary>
        [SerializeField]
        private TMP_Text precipitaionSumText;

        /// <summary>
        /// Reference to teh temperature bar.
        /// </summary>
        [SerializeField]
        private RectTransform tempBar;

        /// <summary>
        /// Rerference to the text displaying the date.
        /// </summary>
        [SerializeField]
        private TMP_Text timeText;

        /// <summary>
        /// Reference to the weather icon.
        /// </summary>
        [SerializeField]
        private Image weatherIcon;

        /// <summary>
        /// Initialize the daily item.
        /// </summary>
        /// <param name="date">The date of the item.</param>
        /// <param name="minTemp">The min temperature of the day.</param>
        /// <param name="maxTemp">The max temperautre of the day.</param>
        /// <param name="precipitaionSum">The amount of rain that will fall that day.</param>
        /// <param name="windspeed">The max windspeed that day.</param>
        /// <param name="windDirection">The wind direction that day.</param>
        /// <param name="overallMaxTemp">The overall max temp of the next week.</param>
        /// <param name="overallMinTemp">The overall min temp of the next week.</param>
        /// <param name="genNumber">What number item this is.</param>
        /// <param name="precipitaionSumMin">The overall min rain of the next week.</param>
        /// <param name="precipitaionSumMax">The overall max rain of the next week.</param>
        public void Init(string date, float minTemp, float maxTemp, float precipitaionSum, float windspeed, int windDirection, float overallMaxTemp, float overallMinTemp, int genNumber, float precipitaionSumMin, float precipitaionSumMax, int weatherCode)
        {
            minTemperatureText.text = $"{minTemp}";
            maxTemperatureText.text = $"{maxTemp}";

            SetMinMaxTemperatureHeight(minTemperatureRect, minTemp, overallMinTemp, overallMaxTemp);
            SetMinMaxTemperatureHeight(maxTemperatureRect, maxTemp, overallMinTemp, overallMaxTemp);

            SetRainBarHeight(rainRect, precipitaionSum, precipitaionSumMin, precipitaionSumMax);

            SetTemperatureBar();

            background.color = genNumber % 2 == 0 ? baseColor : offColor;

            precipitaionSumText.text = $"{precipitaionSum}mm";

            timeText.text = DateUtils.GetDayOfWeek(date);

            weatherIcon.sprite = WeatherCodeLibrary.Instance.GetIcon(weatherCode);
        }

        /// <summary>
        /// Setting the min max temperature height.
        /// </summary>
        /// <param name="temperatureTransform">The rect transform.</param>
        /// <param name="temp">The temperature.</param>
        /// <param name="minTemp">The minimum temp.</param>
        /// <param name="maxTemp">The maximum temp.</param>
        private void SetMinMaxTemperatureHeight(RectTransform temperatureTransform, float temp, float minTemp, float maxTemp)
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

        /// <summary>
        /// Setting the rain bar height.
        /// </summary>
        /// <param name="rainTransform">The transform.</param>
        /// <param name="rain">The rain amount.</param>
        /// <param name="minRain">The min rain.</param>
        /// <param name="maxRain">The max rain.</param>
        private void SetRainBarHeight(RectTransform rainTransform, float rain, float minRain, float maxRain)
        {
            float range = maxRain - minRain;
            float pos = rain - minRain;
            float newPos = (pos / range) * 1;

            Vector2 anchorMax = rainTransform.anchorMax;

            if (newPos == 0)
                newPos = -1;

            anchorMax.y = newPos;

            rainTransform.anchorMax = anchorMax;
        }

        /// <summary>
        /// Setting the temp bar.
        /// </summary>
        private void SetTemperatureBar()
        {
            Vector2 anchorMin = tempBar.anchorMin;
            Vector2 anchorMax = tempBar.anchorMax;

            anchorMin.y = minTemperatureRect.anchorMin.y;
            anchorMax.y = maxTemperatureRect.anchorMax.y;

            tempBar.anchorMin = anchorMin;
            tempBar.anchorMax = anchorMax;
        }
    }
}