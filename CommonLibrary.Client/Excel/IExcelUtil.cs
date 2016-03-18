using System.Collections.Generic;
using System.IO;

namespace CommonLibrary.Client.Excel
{
    public interface IExcelUtil
    {
        void ExportToStream(Stream stream, params Sheet[] sheets);

        void ExportToFile(string filePath, params Sheet[] sheets);
    }

    public class Sheet
    {
        public string Name { get; set; }

        public List<dynamic> Rows { get; set; }

        public List<Cell> Cells { get; set; } 
    }

    public class Cell
    {
        public string Title { get; set; }
        public string Field { get; set; }
        public string Format { get; set; }
        public Cell(string title, string field, string format = null)
        {
            Title = title;
            Field = field;
            Format = format;
        }
    }
}