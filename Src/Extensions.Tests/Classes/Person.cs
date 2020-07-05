using GoodToCode.Extensions;
using System;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// Simulates a customer business object for passing over Http and binding to screens
    /// </summary>
    public class PersonInfo
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
    }
}