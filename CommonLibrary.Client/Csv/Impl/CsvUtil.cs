using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace CommonLibrary.Client.Csv.Impl
{
    public class CsvUtil : ICsvUtil
    {
        public List<T> Read<T, TMap>(string filePath) where TMap : CsvClassMap<T>
        {
            using (var streamReader = File.OpenText(filePath))
            {
                using (var csv = new CsvReader(streamReader))
                {
                    csv.Configuration.RegisterClassMap<TMap>();
                    var records = csv.GetRecords<T>();
                    return records.ToList();
                }
            }
        }

        public void Write<T>(string filePath, List<T> list)
        {
            using (var writer = new StreamWriter(filePath, false))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(list);
            }
        }
    }
}