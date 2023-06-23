namespace WeatherApp.API
{
    public class HourlyRainRequest : APIRequest
    {
        private readonly double _latitude;

        private readonly double _longitude;

        private readonly string _startDate;

        private readonly string _endDate;

        public override string URL => $"https://api.open-meteo.com/v1/forecast?latitude={_latitude}&longitude={_longitude}&hourly=precipitation,weathercode&forecast_days=1&start_date={_startDate}&end_date={_endDate}";

        public HourlyRainRequest(double latitude, double longitude, string startDate, string endDate) 
        {
            _latitude = latitude;
            _longitude = longitude;
            _startDate = startDate;
            _endDate = endDate;
        }
    }
}