using GoodToCode.Extensions;
using System;
using System.Collections.Generic;

namespace GoodToCode.Extensions.Test
{
    /// <summary>
    /// Entity for direct table access
    /// </summary>    
    public partial class Customer
    {
        /// <summary>
        /// Id of record
        ///  Can set Id before saving, and will be preserved
        ///  only if using GoodToCode.Framework.Repository for CRUD
        /// </summary>
        public virtual int Id { get; set; } = -1;

        /// <summary>
        /// Key of record
        ///  Can set Key before saving, and will be preserved
        ///  only if using GoodToCode.Framework.Repository for CRUD
        /// </summary>
        public virtual Guid Key { get; set; } = Guid.Empty;

        /// <summary>
        /// Date record was created
        /// </summary>
        public virtual DateTime CreatedDate { get; set; } = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);

        /// <summary>
        /// Date record was modified
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; } = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);

        /// <summary>
        /// Status of this record
        /// </summary>
        public virtual Guid State { get; set; } = Guid.Empty;

        /// <summary>
        /// FirstName of customers
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// MiddleName of customer
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// LastName of customer
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// BirthDate of customer
        /// </summary>
        public DateTime BirthDate { get; set; } = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);

        /// <summary>
        /// BirthDate of customer
        /// </summary>
        public int GenderId { get; set; } = Genders.NotSet.Key;

        /// <summary>
        /// Type of customer
        /// </summary>
        public int CustomerTypeId { get; set; } = -1;
       
        /// <summary>
        /// ISO 5218 Standard for Gender values
        /// </summary>
        public struct Genders
        {
            /// <summary>
            /// Default. Not set
            /// </summary>
            public static KeyValuePair<int, string> NotSet { get; } = new KeyValuePair<int, string>(-1, "Not Set");

            /// <summary>
            /// Unknown gender
            /// </summary>
            public static KeyValuePair<int, string> NotKnown { get; } = new KeyValuePair<int, string>(0, "Not Known");

            /// <summary>
            /// Male gender
            /// </summary>
            public static KeyValuePair<int, string> Male { get; } = new KeyValuePair<int, string>(1, "Male");

            /// <summary>
            /// Femal Gender
            /// </summary>
            public static KeyValuePair<int, string> Female { get; } = new KeyValuePair<int, string>(2, "Female");

            /// <summary>
            /// Not applicable or do not want to specify
            /// </summary>
            public static KeyValuePair<int, string> NotApplicable { get; } = new KeyValuePair<int, string>(9, "Not Applicable");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Customer() : base()
        {
        }
    }
}
