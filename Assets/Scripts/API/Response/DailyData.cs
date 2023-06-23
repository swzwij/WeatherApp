namespace WeatherApp.API
{
    /// <summary>
    /// Class to handle the daily data.
    /// </summary>
    public class DailyData
    {
        /// <summary>
        /// The latitude of where the data has come from.
        /// </summary>
        public readonly float latitude;

        /// <summary>
        /// The lonitude of where the data has come from
        /// </summary>
        public readonly float longitude;

        /// <summary>
        /// The time it took to get the data.
        /// </summary>
        public readonly float generationtimeMS;

        /// <summary>
        /// The offset of the utc.
        /// </summary>
        public readonly int utcOffsetSeconds;

        /// <summary>
        /// The timezone.
        /// </summary>
        public readonly string timezone;

        /// <summary>
        /// The timezone abbreviation.
        /// </summary>
        public readonly string timezoneAbbreviation;

        /// <summary>
        /// The elevation of where the weather data has come from.
        /// </summary>
        public readonly float elevation;

        /// <summary>
        /// The daily units.
        /// </summary>
        public readonly DailyUnits dailyUnits;

        /// <summary>
        /// The daily temperature data.
        /// </summary>
        public readonly DailyTemperatureData daily;

        /// <summary>
        /// Class to handle the daily units.
        /// </summary>
        [System.Serializable]
        public class DailyUnits
        {
            /// <summary>
            /// The hour.
            /// </summary>
            public readonly string time;

            /// <summary>
            /// The weather code.
            /// </summary>
            public readonly string weathercode;

            /// <summary>
            /// The maximum temperature at 2 meters height.
            /// </summary>
            public readonly string temperature2mMax;

            /// <summary>
            /// The minimum temperature at 2 meters height.
            /// </summary>
            public readonly string temperature2mMin;

            /// <summary>
            /// The amount of milimeters of rain will fall in that hour.
            /// </summary>
            public readonly string precipitationSum;

            /// <summary>
            /// The max windspeed at 10 meters height.
            /// </summary>
            public readonly string windspeed10mMax;

            /// <summary>
            /// The wind direction at 10 meters height.
            /// </summary>
            public readonly string winddirection10mDominant;
        }

        /// <summary>
        /// Class to handle the daily temperature data.
        /// </summary>
        [System.Serializable]
        public class DailyTemperatureData
        {
            /// <summary>
            /// List of times with an interval of 60 minutes.
            /// </summary>
            public readonly string[] time;

            /// <summary>
            /// List of weather codes of each hour.
            /// </summary>
            public readonly int[] weathercode;

            /// <summary>
            /// List of max temperatures at 2 meters height for each hour.
            /// </summary>
            public readonly float[] temperature2mMax;

            /// <summary>
            /// List of min temperatures at 2 meters height for each hour.
            /// </summary>
            public readonly float[] temperature2mMin;

            /// <summary>
            /// list of amount of milimeters that will fall each hour.
            /// </summary>
            public readonly float[] precipitationSum;

            /// <summary>
            /// List of max windspeeds at 10 meters height for each hour.
            /// </summary>
            public readonly float[] windspeed10mMax;

            /// <summary>
            /// List of wind directions at 10 meters height for each hour.
            /// </summary>
            public readonly int[] winddirection10mDominant;
        }
    }
}