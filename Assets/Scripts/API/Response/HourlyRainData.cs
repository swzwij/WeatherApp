public class HourlyRainData
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
        public string precipitation;
        public string weathercode;
    }

    [System.Serializable]
    public class HourlyData
    {
        public string[] time;
        public float[] precipitation;
        public int[] weathercode;
    }
}