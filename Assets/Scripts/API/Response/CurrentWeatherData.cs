namespace WeatherApp.API
{
    public class CurrentWeatherData
    {
        public float latitude;
        public float longitude;
        public float generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public float elevation;
        public HourlyUnits hourly_units;
        public HourlyData hourly;

        [System.Serializable]
        public class HourlyUnits
        {
            public string time;
            public string relativehumidity_2m;
            public string dewpoint_2m;
            public string apparent_temperature;
            public string precipitation_probability;
            public string precipitation;
            public string cloudcover;
            public string visibility;
            public string windspeed_10m;
            public string winddirection_10m;
        }

        [System.Serializable]
        public class HourlyData
        {
            public string[] time;
            public int[] relativehumidity_2m;
            public float[] dewpoint_2m;
            public float[] apparent_temperature;
            public int[] precipitation_probability;
            public float[] precipitation;
            public int[] cloudcover;
            public float[] visibility;
            public float[] windspeed_10m;
            public int[] winddirection_10m;
        }
    }
}