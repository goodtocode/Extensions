using System;

namespace GoodToCode.Extensions.Mathematics
{
    /// <summary>
    /// Calculates age in days and years
    /// </summary>
    /// <remarks></remarks>

    public class Age
    {
        private readonly DateTime birthDayField = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
        private readonly DateTime todayField = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
        private int yearsField = -1;
        private int daysField = -1;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dateToAge">Date that will be used to calculate age</param>
        public Age(DateTime dateToAge)
            : base()
        {
            yearsField = 0;
            daysField = 0;
            birthDayField = dateToAge;
            todayField = DateTime.UtcNow;
        }

        /// <summary>
        /// Age in years
        /// </summary>
        public int Years
        {
            get
            {
                if (birthDayField != new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc))
                {
                    yearsField = DateTime.Today.Year - birthDayField.Year;
                    if (birthDayField.AddYears(yearsField) > DateTime.Today)
                    {
                        yearsField -= 1;
                    }
                }
                return yearsField;
            }
        }

        /// <summary>
        /// Age in days
        /// </summary>
        public int Days
        {
            get
            {
                if (birthDayField != new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc))
                {
                    daysField = todayField.Subtract(birthDayField).Days;
                }
                return daysField;
            }
        }
    }
}
