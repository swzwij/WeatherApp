public class DailyData
{
    public float latitude;
    public float longitude;
    public float generationtime_ms;
    public int utc_offset_seconds;
    public string timezone;
    public string timezone_abbreviation;
    public float elevation;
    public DailyUnits daily_units;
    public DailyTemperatureData daily;

    [System.Serializable]
    public class DailyUnits
    {
        public string time;
        public string weathercode;
        public string temperature_2m_max;
        public string temperature_2m_min;
        public string precipitation_sum;
        public string windspeed_10m_max;
        public string winddirection_10m_dominant;
    }

    [System.Serializable]
    public class DailyTemperatureData
    {
        public string[] time;
        public int[] weathercode;
        public float[] temperature_2m_max;
        public float[] temperature_2m_min;
        public float[] precipitation_sum;
        public float[] windspeed_10m_max;
        public int[] winddirection_10m_dominant;
    }
}