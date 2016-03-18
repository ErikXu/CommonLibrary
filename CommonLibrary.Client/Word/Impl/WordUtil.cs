using System.Collections.Generic;
using System.IO;
using Novacode;

namespace CommonLibrary.Client.Word.Impl
{
    public class WordUtil : IWordUtil
    {
        public void FillToStream(Stream stream, string templatePath, IEnumerable<WordItem> items)
        {
            var doc = DocX.Load(templatePath);
            foreach (var item in items)
            {
                doc.Paragraphs[item.Index].ReplaceText(item.Key, item.Value);
            }
            doc.SaveAs(stream);
        }

        public void FillToFile(string filePath, string templatePath, IEnumerable<WordItem> items)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                FillToStream(fs, templatePath, items);
            }
        }
    }
}