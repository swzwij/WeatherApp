namespace WeatherApp.API
{
    /// <summary>
    /// Class to handle the hourly temperature request made to the API.
    /// </summary>
    public class HourlyTemperatureRequest : APIRequest
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
        /// The start day from when the data will be gotten.
        /// </summary>
        private readonly string _startDate;

        /// <summary>
        /// The end day till the data will be gotten.
        /// </summary>
        private readonly string _endDate;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string URL => $"https://api.open-meteo.com/v1/forecast?latitude={_latitude}&longitude={_longitude}&hourly=temperature_2m,weathercode&forecast_days={1}&start_date={_startDate}&end_date={_endDate}";

        /// <summary>
        /// The data that will be send to the API.
        /// </summary>
        /// <param name="latitude">The latitude of the current location.</param>
        /// <param name="longitude">The longitude of the current location.</param>
        /// <param name="startDate">The start day from when the data will be gotten.</param>
        /// <param name="endDate">The end day till the data will be gotten.</param>
        public HourlyTemperatureRequest(double latitude, double longitude, string startDate, string endDate)
        {
            _latitude = latitude;
            _longitude = longitude;
            _startDate = startDate;
            _endDate = endDate;
        }
    }
}