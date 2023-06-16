using SingletonBehaviour;
using System;
using UnityEngine;

/// <summary>
/// Class to handle getting the gps location of the device.
/// </summary>
public class GPSManager : SingletonBehaviour<GPSManager>
{
    /// <summary>
    /// Holds the last location.
    /// </summary>
    private Vector2 _lastLocation;

    private double _latitude;

    private double _longitude;

    public double Latitude => _latitude;

    public double Longitude => _longitude;

    /// <summary>
    /// Getting the last used location.
    /// </summary>
    public Vector2 LastLocation => _lastLocation;

    /// <summary>
    /// Event for when the location is gotten.
    /// </summary>
    public Action<Vector2> onGetLocation;

    /// <summary>
    /// Get Location.
    /// </summary>
    private void Awake() => GetLocation();

    /// <summary>
    /// Getting the location.
    /// </summary>
    private void GetLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services are not enabled on the device.");
            return;
        }

        Input.location.Start();

        LocationInfo locationInfo = Input.location.lastData;

        _latitude = locationInfo.latitude;
        _longitude = locationInfo.longitude;

        Vector2 location = new((float)_latitude, (float)_longitude);

        Input.location.Stop();

        onGetLocation?.Invoke(location);
    }
}
