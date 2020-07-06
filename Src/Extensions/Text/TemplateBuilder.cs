using System;
using System.Collections.Generic;
using System.Linq;
using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Text
{
    /// <summary>
    /// Handles formatting html text, such as filling HTML templates.
    /// </summary>
    
    public class TemplateBuilder
    {        
        private List<string> templateDataField = new List<string>();
        private string templateEmptyField = string.Empty;
        private string templateFilledField = string.Empty;
        private bool isHTML = false;
        /// <summary>
        /// Setting to re-throw exception
        /// </summary>
        public bool ThrowException { get; set; } = false;
        
        /// <summary>
        /// Constructor forcing Immutability
        /// </summary>
        private TemplateBuilder() : base() {
#if (DEBUG)
            ThrowException = true;
#endif
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateBuilder(string template, List<string> data)
            : base()
        {
            isHTML = true;
            templateEmptyField = template;
            templateDataField = data;
        }
        
        /// <summary>
        /// Fills template
        /// </summary>
        private void TemplateFill()
        {            
            var dataFormatted = string.Empty;
            var templateFilled = string.Empty;

            IsValid(); // throw exception if bad data
            templateFilled = this.templateEmptyField;
            for (var dataCount = 0; dataCount <= this.templateDataField.Count - 1; dataCount++)
            {
                if (this.isHTML) { dataFormatted = this.templateDataField[dataCount].Replace(Environment.NewLine, "<br />").Replace("\n", "<br />"); }
                templateFilled = templateFilled.Replace("{" + dataCount.ToString() + "}", dataFormatted);
            }
            templateFilledField = templateFilled;
        }
        
        /// <summary>
        /// Returns built HTML template with data
        /// </summary>
        /// <returns></returns>
        public override string ToString() 
        {
            TemplateFill();
            return templateFilledField;
        }
        
        /// <summary>
        ///  Ensures data can be merged with template
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            var returnValue = false;
            for(int count = 0; count < this.templateDataField.Count() - 1; count++ )
            {
                if (!this.templateEmptyField.Contains("{" + count + "}")) throw new System.Exception("Error merging template and data. Not enough data to fill the template.");
            }
            return returnValue;
        }
    }
}
