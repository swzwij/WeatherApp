using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherApp.API
{
    public class SearchLocationRequest : APIRequest
    {
        private string _locationName;

        public override string URL => $"https://geocode.maps.co/search?q={_locationName}";

        public SearchLocationRequest(string locationName)
        {
            _locationName = locationName;
        }
    }
}