using System;
using System.Collections.Generic;
using System.Globalization;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// Extends DateTIme class
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// DateTime formats, use case for De-serialization in Json
        /// </summary>
        public struct Formats
        {
            /// <summary>
            /// ISO 8601 datetime with 3 milliseconds precision
            /// </summary>
            public static string ISO8601F = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff";

            /// <summary>
            /// ISO 8601 datetime with seconds precision
            /// </summary>
            public static string ISO8601 = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss";

            /// <summary>
            /// ISO 8601 datetime with 3 milliseconds precision
            /// </summary>
            public static string JsonNet = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

            /// <summary>
            /// Microsoft webapi default datetime format specifier
            /// </summary>
            public static string Default = "u";
        }

        /// <summary>
        /// List of formats for iteration
        /// </summary>
        public static List<string> FormatList = new List<string>() { Formats.JsonNet, Formats.ISO8601F, Formats.ISO8601, Formats.Default };

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        ///  representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="item">Date to add a day</param>
        /// <returns>DateTime exactly one day from the original date.</returns>
        public static string ToString(this DateTime item)
        {
            return item.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        ///  representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="item">Date to add a day</param>
        /// <param name="formatString">A standard or custom date and time format string</param>
        /// <returns>DateTime exactly one day from the original date.</returns>
        public static string ToString(this DateTime item, string formatString)
        {
            return item.ToString(formatString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Tomorrow from date
        /// </summary>
        /// <param name="item">Date to add a day</param>
        /// <returns>DateTime exactly one day from the original date.</returns>
        public static DateTime Tomorrow(this DateTime item)
        {
            return item.AddDays(1);
        }

        /// <summary>
        /// Yesterday from date
        /// </summary>
        /// <param name="item">Date to subtract a day</param>
        /// <returns>DateTime exactly one day in the past from the original date.</returns>
        public static DateTime Yesterday(this DateTime item)
        {
            return item.AddDays(-1);
        }

        /// <summary>
        /// First day of a month
        /// </summary>
        /// <param name="item">DateTime to get the month and year</param>
        /// <returns>1st day of the passed DateTime.</returns>
        public static DateTime FirstDayOfMonth(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, 1, 00, 00, 00, 000, DateTimeKind.Utc);
        }

        /// <summary>
        /// Last day of a month and year
        /// </summary>
        /// <param name="item">DateTime to get the month and year</param>
        /// <returns>Last day of the passed DateTime.</returns>
        public static DateTime LastDayOfMonth(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, DateTime.DaysInMonth(item.Year, item.Month), 00, 00, 00, 000, DateTimeKind.Utc);
        }
        
        /// <summary>
        /// Adds number of weekdays to date (skipping Saturday and Sunday)
        /// </summary>
        /// <param name="item">Item in which to add number of weekdays to.</param>
        /// <param name="weekdays">Number of M-F days to add to this date.</param>
        /// <returns>Date after adding number of weekdays (skipping Saturday and Sunday)</returns>
        public static DateTime AddWeekdays(this DateTime item, int weekdays)
        {
            var sign = weekdays < 0 ? -1 : 1;
            var unsignedDays = Math.Abs(weekdays);
            var weekdaysAdded = 0;
            var returnValue = Defaults.Date;

            while (weekdaysAdded < unsignedDays)
            {
                item = item.AddDays(sign);
                if (item.DayOfWeek != DayOfWeek.Saturday && item.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdaysAdded++;
                }
            }
            returnValue = item;

            return returnValue;
        }
        
        /// <summary>
        /// Age in years based on date to today
        /// </summary>
        /// <param name="item">DateTime to determine age.</param>
        /// <returns>Age in years of the DateTime.</returns>
        public static int Age(this DateTime item)
        {
            var returnValue = 0;

            if (item != Defaults.Date)
            {
                returnValue = DateTime.Today.Year - item.Year;
                if (item.AddYears(returnValue) > DateTime.Today)
                {
                    returnValue -= 1;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Ensures date is savable in SQL Server
        /// </summary>
        /// <param name="item">DateTime to validate.</param>
        /// <returns>True for if the date can be saved to SQL Server.</returns>
        public static bool IsSavable(this DateTime item)
        {
            var sqlMinimumDate = new DateTime(1753, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc); // Minimum Date SQL Accepts
            var sqlMaximumDate = new DateTime(9999, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc); // Maximum Date SQL Accepts
            var returnValue = Defaults.Boolean;
            var minResult = DateTime.Compare(item, sqlMinimumDate);
            var maxResult = DateTime.Compare(item, sqlMaximumDate);

            if ((minResult >= 0) & (maxResult <= 0))
            {
                returnValue = true;
            }
            
            return returnValue;
        }

        /// <summary>
        /// Replaces Default.{Type} with String.Empty with a readable value
        /// Values to replace are: 01/01/1900, -1, 0.00, 00000000-0000-0000-0000-000000000000
        /// I.e. Replace -1 with "". Replace 0.00 with "Free"
        /// </summary>
        /// <param name="item">Original value to replace</param>
        /// <param name="replaceWith">Value to replace default values with</param>
        /// <returns></returns>
        public static string ToReadable(this DateTime item, string replaceWith = "")
        {
            var returnValue = item.ToString();
            returnValue = item == Defaults.Date ? replaceWith : returnValue;
            return returnValue;
        }
    }
}
