namespace WeatherApp.API
{
    public class DaylightCycleData
    {
        public double latitude;
        public double longitude;
        public double generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public double elevation;
        public DailyData daily;

        [System.Serializable]
        public class DailyData
        {
            public string[] time;
            public string[] sunrise;
            public string[] sunset;
        }
    }
}