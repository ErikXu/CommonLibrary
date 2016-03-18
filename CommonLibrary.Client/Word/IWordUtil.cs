using System.Collections.Generic;
using System.IO;

namespace CommonLibrary.Client.Word
{
    public interface IWordUtil
    {
        void FillToStream(Stream stream, string templatePath, IEnumerable<WordItem> items);

        void FillToFile(string filePath, string templatePath, IEnumerable<WordItem> items);
    }

    public class WordItem
    {
        public int Index { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}