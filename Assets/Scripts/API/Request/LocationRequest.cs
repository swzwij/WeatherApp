namespace WeatherApp.API
{
    /// <summary>
    /// Class to handle the location request made to the API.
    /// </summary>
    public class LocationRequest : APIRequest
    {
        /// <summary>
        /// The latitude of the current location.
        /// </summary>
        private readonly double _latitude;

        /// <summary>
        /// The longitude of the current location.
        /// </summary>
        private readonly double _longitude;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string URL => $"https://geocode.maps.co/reverse?lat={_latitude}&lon={_longitude}";

        /// <summary>
        /// The data that will be send to the API.
        /// </summary>
        /// <param name="latitude">The latitude of the current location.</param>
        /// <param name="longitude">The longitude of the current location.</param>
        public LocationRequest(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}
