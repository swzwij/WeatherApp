namespace WeatherApp.API
{
    /// <summary>
    /// Class to handle the daylight cycle request made to the API.
    /// </summary>
    public class DaylightCycleRequest : APIRequest
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
        public override string URL => $"https://api.open-meteo.com/v1/forecast?latitude={_latitude}&longitude={_longitude}&daily=sunrise,sunset&forecast_days=1&timezone=Europe%2FBerlin";

        /// <summary>
        /// The data that will be send to the API.
        /// </summary>
        /// <param name="latitude">The latitude of the current location.</param>
        /// <param name="longitude">The longitude of the current location.</param>
        public DaylightCycleRequest(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}