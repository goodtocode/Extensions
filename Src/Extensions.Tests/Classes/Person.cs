using GoodToCode.Extensions;
using System;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// Simulates a customer business object for passing over Http and binding to screens
    /// </summary>
    public class PersonInfo
    {
        public string FirstName { get; set; } = Defaults.String;
        public string LastName { get; set; } = Defaults.String;
        public DateTime BirthDate { get; set; } = Defaults.Date;
    }
}