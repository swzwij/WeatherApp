namespace WeatherApp.API
{
    public class CurrentWeatherRequest : APIRequest
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
        public override string URL => $"https://api.open-meteo.com/v1/forecast?latitude={_latitude}&longitude={_longitude}&hourly=relativehumidity_2m,dewpoint_2m,apparent_temperature,precipitation_probability,precipitation,cloudcover,visibility,windspeed_10m,winddirection_10m&forecast_days=1";

        /// <summary>
        /// The data that will be send to the API.
        /// </summary>
        /// <param name="latitude">The latitude of the current location.</param>
        /// <param name="longitude">The longitude of the current location.</param>
        public CurrentWeatherRequest(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}