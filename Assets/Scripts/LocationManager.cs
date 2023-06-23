using System;
using TMPro;
using UnityEngine;
using WeatherApp.API;

public class LocationManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _locationText;

    private void OnEnable() => GPSManager.Instance.onGetLocation += SetLocation;

    private void OnDisable() => GPSManager.Instance.onGetLocation -= SetLocation;

    private void SetLocation(LocationCoordinates location)
    {
        Action<LocationData> onComplete = (response) => _locationText.text = $"{response.address.town}";
        Action onFailure = () => Debug.LogError("Failed to get location data");

        LocationRequest request = new(location.Latitude, location.Longitude);
        new_APIManager.Instance.GetCall(request, onComplete, onFailure);
    }
}
