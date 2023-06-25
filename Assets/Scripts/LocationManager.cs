using System;
using TMPro;
using UnityEngine;
using WeatherApp.API;
using WeatherApp.Utils;

namespace WeatherApp.Location
{
    /// <summary>
    /// Class to handle showing the location.
    /// </summary>
    public class LocationManager : MonoBehaviour
    {
        /// <summary>
        /// Reference to the text displaying the location.
        /// </summary>
        [SerializeField]
        private TMP_Text _locationText;

        /// <summary>
        /// Setting the onGetLocation listener.
        /// </summary>
        private void OnEnable() => GPSManager.Instance.onGetLocation += SetLocation;

        /// <summary>
        /// Removing the onGetLocation listener.
        /// </summary>
        private void OnDisable() => GPSManager.Instance.onGetLocation -= SetLocation;

        /// <summary>
        /// Make an API call to get the name of the current location.
        /// </summary>
        /// <param name="location">The current location.</param>
        private void SetLocation(LocationCoordinates location)
        {
            Action<LocationData> onComplete = (response) => _locationText.text = $"{response.address.town}";
            Action onFailure = () => Debug.LogError("Failed to get location data");

            LocationRequest request = new(location.Latitude, location.Longitude);
            APIManager.Instance.GetCall(request, onComplete, onFailure);
        }
    }
}