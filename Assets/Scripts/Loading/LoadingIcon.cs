using UnityEngine;

namespace WeatherApp.Loading
{
    /// <summary>
    /// Class to handle the loading icon.
    /// </summary>
    public class LoadingIcon : MonoBehaviour
    {
        /// <summary>
        /// The speed at which the icon rotates.
        /// </summary>
        private const float ROTATION_SPEED = 250;

        /// <summary>
        /// The the rect transform of this object.
        /// </summary>
        private RectTransform _rectTransform;

        /// <summary>
        /// Setting the rect transform.
        /// </summary>
        private void Awake() => _rectTransform = GetComponent<RectTransform>();

        /// <summary>
        /// Rotating the icon.
        /// </summary>
        private void Update()
        {
            if (transform.parent.gameObject.activeSelf)
                _rectTransform.Rotate(0f, 0f, -ROTATION_SPEED * Time.deltaTime);
        }
    }
}