using DTT.UI.ProceduralUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WeatherApp.WeatherSystem
{
    /// <summary>
    /// Class to handle a hourly item.
    /// </summary>
    public class HourlyItem : MonoBehaviour
    {
        /// <summary>
        /// Reference to the hour text.
        /// </summary>
        [SerializeField]
        private TMP_Text _hourText;

        /// <summary>
        /// Reference to the value text.
        /// </summary>
        [SerializeField]
        private TMP_Text _valueText;

        /// <summary>
        /// Reference to the rect holding the value text.
        /// </summary>
        [Space]
        [SerializeField]
        private RectTransform _valueTransform;

        /// <summary>
        /// The text behind the value.
        /// </summary>
        [SerializeField]
        private string _valueSuffix;

        /// <summary>
        /// Reference to the weather image.
        /// </summary>
        [SerializeField]
        private Image _weatherImage;

        /// <summary>
        /// Reference to the background.
        /// </summary>
        [SerializeField]
        private RoundedImage _background;

        /// <summary>
        /// The base color of the item.
        /// </summary>
        [Space]
        [SerializeField]
        private Color _baseColor;

        /// <summary>
        /// The off color of the item.
        /// </summary>
        [SerializeField]
        private Color _offColor;

        /// <summary>
        /// Set the height of the value rect.
        /// </summary>
        /// <param name="value">The value of this item.</param>
        /// <param name="minValue">The min of the value.</param>
        /// <param name="maxValue">The max of the value.</param>
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

        /// <summary>
        /// Set the texts and colors.
        /// </summary>
        /// <param name="hour">The time of day.</param>
        /// <param name="value">The value.</param>
        /// <param name="weatherCode">The weather code.</param>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <param name="genNumber">The number of item this is.</param>
        public void Init(string hour, float value, int weatherCode, float minValue, float maxValue, int genNumber)
        {
            _hourText.text = hour;
            _valueText.text = $"{value}{_valueSuffix}";

            SetValue(value, minValue, maxValue);

            _background.color = genNumber % 2 == 0 ? _baseColor : _offColor;
        }
    }
}