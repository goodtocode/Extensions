using System.IO;
using System.Text;

namespace GoodToCode.Extensions.IO
{
    /// <summary>
    /// Search a set of paths on a drive for a folder. 
    ///     Configure with auto search parent and children a certain levels in.
    /// </summary>

    public class FileReader
    {
        private readonly string file = Defaults.String; 
        /// <summary>
        /// Constructor
        /// </summary>
        public FileReader() : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Path and file to read</param>
        public FileReader(string fileName)
            : this()
        {
            file = fileName;
        }

        /// <summary>
        /// Reads a file using StreamReader class
        /// </summary>
        public string ReadToEnd()
        {
            var returnValue = Defaults.String;
            using (var streamReader = new StreamReader(file, Encoding.UTF8))
            {
                returnValue = streamReader.ReadToEnd();
            }
            return returnValue;
        }        
    }
}
