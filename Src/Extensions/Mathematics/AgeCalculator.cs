using System;

namespace GoodToCode.Extensions.Mathematics
{
    /// <summary>
    /// Calculates age in days and years
    /// </summary>
    /// <remarks></remarks>

    public class Age
    {
        private readonly DateTime birthDayField = Defaults.Date;
        private readonly DateTime todayField = Defaults.Date;
        private int yearsField = Defaults.Integer;
        private int daysField = Defaults.Integer;
        
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
                if (birthDayField != Defaults.Date)
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
                if (birthDayField != Defaults.Date)
                {
                    daysField = todayField.Subtract(birthDayField).Days;
                }
                return daysField;
            }
        }
    }
}
