namespace WeatherApp.API
{
    public class DailyRequest : APIRequest
    {
        private readonly double _latitude;

        private readonly double _longitude;

        private readonly string _startDate;

        private readonly string _endDate;

        public override string URL => $"https://api.open-meteo.com/v1/forecast?latitude={_latitude}&longitude={_longitude}&daily=weathercode,temperature_2m_max,temperature_2m_min,precipitation_sum,windspeed_10m_max,winddirection_10m_dominant&start_date={_startDate}&end_date={_endDate}&timezone=Europe%2FBerlin";

        public DailyRequest(double latitude, double longitude, string startDate, string endDate)
        {
            _latitude = latitude;
            _longitude = longitude;
            _startDate = startDate;
            _endDate = endDate;
        }
    }
}
