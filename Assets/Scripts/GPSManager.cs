using SingletonBehaviour;
using System;
using System.Collections;
using UnityEngine;
using WeatherApp.Utils;

namespace WeatherApp.Location
{
    /// <summary>
    /// Class to handle getting the gps location of the device.
    /// </summary>
    public class GPSManager : SingletonBehaviour<GPSManager>
    {
        /// <summary>
        /// Holds the last location.
        /// </summary>
        private LocationCoordinates _lastLocation;

        /// <summary>
        /// Getting the last used location.
        /// </summary>
        public LocationCoordinates LastLocation => _lastLocation;

        /// <summary>
        /// Event for when the location is gotten.
        /// </summary>
        public Action<LocationCoordinates> onGetLocation;

        /// <summary>
        /// Get Location.
        /// </summary>
        private void Awake() => StartCoroutine(GetLocation());

        /// <summary>
        /// Getting the location.
        /// </summary>
        private IEnumerator GetLocation()
        {
#if UNITY_EDITOR
            LocationCoordinates location = new(52.64, 5.06);

            yield return new WaitForSeconds(2f);

            _lastLocation = location;
            onGetLocation?.Invoke(location);
#else
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("Location services are not enabled on the device.");
                yield break;
            }

            Input.location.Start();

            yield return new WaitUntil(() => Input.location.status == LocationServiceStatus.Running);

            LocationInfo locationInfo = Input.location.lastData;
            LocationCoordinates location = new(locationInfo.latitude, locationInfo.longitude);

            Input.location.Stop();

            _lastLocation = location;
            onGetLocation?.Invoke(location);
#endif
        }
    }
}
