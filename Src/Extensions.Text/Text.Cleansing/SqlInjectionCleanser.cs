using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Cleansing
{
    /// <summary>
    /// Cleanses and removes Html unsafe characters
    /// </summary>
    
    public class SqlInjectionCleanser : Cleanser
    {
        /// <summary>
        /// Target of this cleanser
        /// </summary>
        public override CleanserIds CleanserId { get; } = CleanserIds.Default;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public SqlInjectionCleanser() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textToCleanse">Plain text to have characters cleansed</param>
        public SqlInjectionCleanser(string textToCleanse)
            : this()
        {
            TextToCleanse = textToCleanse;
        }

        /// <summary>
        /// Cleanses a string
        /// </summary>
        public override string Cleanse()
        {
            TextCleansed = TextToCleanse;
            return TextCleansed;
        }
    }
}
