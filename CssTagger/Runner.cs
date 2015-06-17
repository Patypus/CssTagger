using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CssTagger
{
    internal class Runner
    {
        /// <summary>
        ///  Perform the task of running tagging keywords in a file.
        /// </summary>
        /// <param name="keywordsFile">Path to the file containing the keywords to locate</param>
        /// <param name="markerWord">String to place, this is envisioned that this will be something unique that can be searched for easily</param>
        /// <param name="fileToScan">Path to the file to search for keywords in and to add tag words to</param>
        /// <returns>String status message about the final result of the tagging operation</returns>
        public string RunTagging(string keywordsFile, string markerWord, string fileToScan)
        {
            var result = string.Empty;
            try
            {
                var keywords = LoadFileContent(keywordsFile);
                var scanFileContents = LoadFileContent(keywordsFile);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        private IEnumerable<string> LoadFileContent(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllLines(filePath);
            }
            else
            {
                throw new FileNotFoundException(string.Join(": ", "Unable to find file at path", filePath));
            }
        }

        private IEnumerable<string> TagKeywordsInScanFileContents(IEnumerable<string> keywords, string markerWord, IEnumerable<string> scanContent)
        {
            var result = scanContent.Select(line => line.Contains())
            return null;
        }
    }
}
