using System;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text.Cleansing
{
    /// <summary>
    /// Cleanses and removes Html unsafe characters
    /// </summary>
    
    public class DefaultCleanser : Cleanser
    {
        /// <summary>
        /// Target of this cleanser
        /// </summary>
        public override CleanserIds CleanserId { get; } = CleanserIds.Default;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public DefaultCleanser() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="textToCleanse">Plain text to have characters cleansed</param>
        public DefaultCleanser(string textToCleanse)
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
