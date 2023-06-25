using System;

namespace WeatherApp.Utils
{
    public static class DateUtils
    {
        /// <summary>
        /// Format a given year, month and day into a string.
        /// </summary>
        /// <param name="year">The year</param>
        /// <param name="month">The month</param>
        /// <param name="day">The day</param>
        /// <returns>A string formated as such "year-month-day"</returns>
        public static string FormatDate(int year, int month, int day)
        {
            string monthString = $"{month}";
            string dayString = $"{day}";

            if (monthString.Length == 1)
                monthString = $"0{monthString}";

            if (dayString.Length == 1)
                dayString = $"0{dayString}";

            return $"{year}-{monthString}-{dayString}";
        }

        /// <summary>
        /// Get the day of the week by the given date.
        /// </summary>
        /// <param name="date">The given date in format "year-month-day".</param>
        /// <returns>A string with the day of the week.</returns>
        public static string GetDayOfWeek(string date)
        {
            string[] dateParts = date.Split('-');

            string year = dateParts[0];
            string month = dateParts[1];
            string day = dateParts[2];

            DateTime dateValue = new(int.Parse(year), int.Parse(month), int.Parse(day));
            string dayOfWeek = $"{dateValue.DayOfWeek}"[..3];
            return $"{dayOfWeek} {day}";
        }
    }
}