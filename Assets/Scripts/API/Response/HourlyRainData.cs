namespace WeatherApp.API
{
    /// <summary>
    /// Class to handle the hourly rain data.
    /// </summary>
    public class HourlyRainData
    {
        /// <summary>
        /// The latitude of where the data has come from.
        /// </summary>
        public float latitude;

        /// <summary>
        /// The lonitude of where the data has come from
        /// </summary>
        public float longitude;

        /// <summary>
        /// The time it took to get the data.
        /// </summary>
        public float generationtimeMS;

        /// <summary>
        /// The offset of the utc.
        /// </summary>
        public int utcOffsetSeconds;

        /// <summary>
        /// The timezone.
        /// </summary>
        public string timezone;

        /// <summary>
        /// The timezone abbreviation.
        /// </summary>
        public string timezoneAbbreviation;

        /// <summary>
        /// The elevation of where the weather data has come from.
        /// </summary>
        public float elevation;

        /// <summary>
        /// The hourly units.
        /// </summary>
        public HourlyUnits hourlyUnits;

        /// <summary>
        /// The hourly data.
        /// </summary>
        public HourlyData hourly;

        /// <summary>
        /// Class to handle the hourly units.
        /// </summary>
        [System.Serializable]
        public class HourlyUnits
        {
            /// <summary>
            /// The hour.
            /// </summary>
            public string time;

            /// <summary>
            /// The amount of rain that will fall.
            /// </summary>
            public string precipitation;

            /// <summary>
            /// The weather code.
            /// </summary>
            public string weathercode;
        }

        /// <summary>
        /// Class to handle the hourly data.
        /// </summary>
        [System.Serializable]
        public class HourlyData
        {
            /// <summary>
            /// List of times with an interval of 60 minutes.
            /// </summary>
            public string[] time;

            /// <summary>
            /// list of amount of milimeters that will fall each hour.
            /// </summary>
            public float[] precipitation;

            /// <summary>
            /// List of weather codes of each hour.
            /// </summary>
            public int[] weathercode;
        }
    }
}