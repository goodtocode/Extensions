using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoodToCode.Extensions.IO
{
    /// <summary>
    /// Search a set of paths on a drive for a folder. 
    ///     Configure with auto search parent and children a certain levels in.
    /// </summary>
    public class FileSearcher
    {
        private readonly List<DirectoryInfo> pathsField = new List<DirectoryInfo>();
        private List<FileInfo> foundFilesField = new List<FileInfo>();
        
        /// <summary>
        /// Paths
        /// </summary>
        public IEnumerable<DirectoryInfo> Paths { get { return pathsField; } }
        /// <summary>
        /// FoundFiles
        /// </summary>
        public List<FileInfo> FoundFiles { get { return foundFilesField; } }
        /// <summary>
        /// FileNameOrMask
        /// </summary>
        public string FileNameOrMask { get; private set; } = Defaults.String;
        /// <summary>
        /// ParentLevels
        /// </summary>
        public int ParentLevels { get; private set; } = Defaults.Integer;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public FileSearcher() : base() { FileNameOrMask = "*.*"; this.ParentLevels = 2; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        public FileSearcher(string pathToSearch, string fileOrMaskToSearch)
            : this()
        {
            pathsField.Add(new DirectoryInfo(pathToSearch));
            FileNameOrMask = fileOrMaskToSearch;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathsToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        public FileSearcher(IEnumerable<string> pathsToSearch, string fileOrMaskToSearch)
            : this()
        {
            foreach (var item in pathsToSearch)
            {
                pathsField.Add(new DirectoryInfo(item));
            }
            FileNameOrMask = fileOrMaskToSearch;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        /// <param name="levelsUpToSearch"></param>
        public FileSearcher(string pathToSearch, string fileOrMaskToSearch, int levelsUpToSearch) : this(new List<string>() { pathToSearch }, fileOrMaskToSearch, levelsUpToSearch) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathsToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        /// <param name="levelsUpToSearch"></param>
        public FileSearcher(IEnumerable<string> pathsToSearch, string fileOrMaskToSearch, int levelsUpToSearch = 2) 
            : this(pathsToSearch, fileOrMaskToSearch)
        {
            DirectoryInfo currentPath;
            ParentLevels = levelsUpToSearch;
            // Add new paths to search
            foreach (var item in this.pathsField.ToList())
            {
                currentPath = new DirectoryInfo(item.ToString());
                for (var Count = 0; Count < this.ParentLevels; Count++)
                {
                    currentPath = new DirectoryInfo(currentPath.Parent.FullName); // Break reference chain with new instance
                    pathsField.Add(new DirectoryInfo(currentPath.ToString()));                    
                }
            }
        }
        
        /// <summary>
        /// Search
        /// </summary>
        public List<FileInfo> Search()
        {
            _ = new List<FileInfo>();

            foundFilesField = new List<FileInfo>();
            foreach (var Item in this.Paths)
            {
                foundFilesField.AddRange(Item.GetFiles(this.FileNameOrMask));
            }

            return FoundFiles;
        }
    }
}
