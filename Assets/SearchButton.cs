using SingletonBehaviour;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeatherApp.API;
using WeatherApp.Utils;

[RequireComponent(typeof(Button))]
public class SearchButton : SingletonBehaviour<SearchButton>
{
    [SerializeField]
    private TMP_InputField _searchBar;

    private Button _button;

    public Action<LocationCoordinates> onSearchNewLocation;

    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    private void OnClick()
    {
        string location = _searchBar.text;

        Action<SearchLocationData> onComplete = (response) => HandleLocationResponse(response);
        Action onFailure = () => Debug.LogError("Failed to get location temperature");

        SearchLocationRequest request = new(location);
        APIManager.Instance.GetCall(request, onComplete, onFailure);
    }

    private void HandleLocationResponse(SearchLocationData response)
    {
        double latitude = double.Parse(response.locations[0].lat);
        double longitude = double.Parse(response.locations[0].lon);

        LocationCoordinates coordinates = new(latitude, longitude);

        onSearchNewLocation?.Invoke(coordinates);
    }
}
