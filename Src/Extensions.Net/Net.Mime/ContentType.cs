using GoodToCode.Extensions;

namespace GoodToCode.Extensions.Net
{
    /// <summary>
    /// Holds a float content type and file extension
    /// </summary>
    public class ContentType
    {        
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; protected set; } = string.Empty;
        /// <summary>
        /// Extension
        /// </summary>
        public string Extension { get; protected set; } = string.Empty;
        
        /// <summary>
        /// Force immutability
        /// </summary>
        private ContentType() : base() { }

        /// <summary>
        /// Constructor with data
        /// </summary>
        /// <param name="name"></param>
        public ContentType(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Constructor with data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        public ContentType(string name, string extension)
            : this()
        {
            Name = name;
            Extension = extension;
        }
    }
}
