using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CssTagger
{
    internal class Runner
    {
        //The @ is crucial to escape the slashes. If you miss it the thing breaks really weirdly and isn't annoying. At all.
        private const string regexTest = @"\b{0}\b";

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
                var scanFileContents = LoadFileContent(fileToScan);
                var taggedContents = TagKeywordsInScanFileContents(keywords, markerWord, scanFileContents);
                var taggedFileLocation = WriteTaggedContent(fileToScan, taggedContents);
                result = String.Join(" ", "Complete. Tagged file written to:", taggedFileLocation);
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
            var result = new List<string>();
            foreach (var line in scanContent)
            {
                var lineToAdd = line;
                
                foreach (var wordInLine in keywords)
                {
                    var wordInLineRegex = new Regex(wordInLine);
                    lineToAdd = Regex.Replace(lineToAdd, string.Format(regexTest, wordInLine), string.Join("_", markerWord, wordInLine));
                }
                
                result.Add(lineToAdd);
            }
            return result;
        }

        private string WriteTaggedContent(string originalFilePath, IEnumerable<string> taggedContent)
        {
            var filePathComponents = GetFilePathAndNameCompontents(originalFilePath);
            var fileExtension = Path.GetExtension(originalFilePath);
            var taggedFilePath = Path.Combine(filePathComponents.Item1, "tagged_" + filePathComponents.Item2 + fileExtension);
            File.WriteAllLines(taggedFilePath, taggedContent);

            return taggedFilePath;
        }

        private Tuple<string, string> GetFilePathAndNameCompontents(string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var pathName = Path.GetDirectoryName(path);

            return Tuple.Create<string, string>(pathName, fileName);
        }
    }
}
