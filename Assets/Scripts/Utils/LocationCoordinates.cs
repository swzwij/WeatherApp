namespace WeatherApp.Utils
{
    /// <summary>
    /// The coordiantes of a location.
    /// </summary>
    public class LocationCoordinates
    {
        /// <summary>
        /// The latitude.
        /// </summary>
        private double _latitude;

        /// <summary>
        /// The longitude.
        /// </summary>
        private double _longitude;

        /// <summary>
        /// Getting the latitude.
        /// </summary>
        public double Latitude => _latitude;

        /// <summary>
        /// Getting the longitude.
        /// </summary>
        public double Longitude => _longitude;

        /// <summary>
        /// The coordiantes of a location.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public LocationCoordinates(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}