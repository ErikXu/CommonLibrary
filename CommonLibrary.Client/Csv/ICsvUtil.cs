using System.Collections.Generic;
using CsvHelper.Configuration;

namespace CommonLibrary.Client.Csv
{
    public interface ICsvUtil
    {
        List<T> Read<T, TMap>(string filePath) where TMap : CsvClassMap<T>;

        void Write<T>(string filePath, List<T> list);
    }
}