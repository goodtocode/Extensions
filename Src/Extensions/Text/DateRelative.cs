using System;
using GoodToCode.Extensions;
using System.Globalization;

namespace GoodToCode.Extensions.Text
{
    /// <summary>
    /// Relative date and time based on now/utc now.
    /// Responds with english text for relative time lapse
    /// </summary>    
    public class DateRelative
    {
        private DateTime nowDate = DateTime.UtcNow;
        /// <summary>
        /// DateToCompare
        /// </summary>
        public DateTime DateToCompare { get; private set; } = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
        /// <summary>
        /// Difference
        /// </summary>
        public TimeSpan Difference { get; private set; } = new TimeSpan(0);
        /// <summary>
        /// RelativeToDate
        /// </summary>
        public string RelativeToDate { get { return RelativeDifferenceGet(DateToCompare); } private set { } }        
        
        /// <summary>
        /// Constructor
        /// </summary>
        private DateRelative() : base()
        {
            nowDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Constructor that accepts date to compare to UtcNow
        /// </summary>
        /// <param name="dateToCompare"></param>
        public DateRelative(DateTime dateToCompare) : this()
        {
            DateToCompare = dateToCompare; 
        }
        
        /// <summary>
        /// Determines relative difference between now and date
        /// </summary>
        /// <returns>English language representation of difference</returns>
        public override string ToString()
        {
            return RelativeToDate;
        }

        /// <summary>
        /// Helper to get 
        /// </summary>
        /// <param name="compareDate"></param>
        /// <returns></returns>
        private string RelativeDifferenceGet(DateTime compareDate)
        {
            var returnValue = string.Empty;
            TimeSpan diffTime = compareDate.Subtract(this.nowDate);

            // Determine difference
            if (diffTime.TotalDays >= 365)
                returnValue = String.Concat("on ", this.nowDate.ToString("MMMM d, yyyy", CultureInfo.InvariantCulture));
            if (diffTime.TotalDays >= 7)
                returnValue = String.Concat("on ", this.nowDate.ToString("MMMM d", CultureInfo.InvariantCulture));
            else if (diffTime.TotalDays > 1)
                returnValue = String.Format("{0:N0} days ago", diffTime.TotalDays);
            else if (diffTime.TotalDays == 1)
                returnValue = "yesterday";
            else if (diffTime.TotalHours >= 2)
                returnValue = String.Format("{0:N0} hours ago", diffTime.TotalHours);
            else if (diffTime.TotalMinutes >= 60)
                returnValue = "more than an hour ago";
            else if (diffTime.TotalMinutes >= 5)
                returnValue = String.Format("{0:N0} minutes ago", diffTime.TotalMinutes);
            if (diffTime.TotalMinutes >= 1)
                returnValue = "a few minutes ago";
            else
                returnValue = "less than a minute ago";

            // return data
            return returnValue;
        }
    }
}
