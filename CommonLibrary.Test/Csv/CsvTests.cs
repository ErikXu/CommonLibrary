using System;
using System.Collections.Generic;
using System.IO;
using CommonLibrary.Client.Csv;
using CommonLibrary.Client.Csv.Impl;
using CsvHelper.Configuration;
using NUnit.Framework;

namespace CommonLibrary.Test.Csv
{
    [TestFixture]
    public class CsvTests
    {
        private ICsvUtil _csvUtil;

        [SetUp]
        public void SetUp()
        {
            _csvUtil = new CsvUtil();
        }

        [Test]
        public void Read_Normal()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Csv", "Simple.csv");
            var simples = _csvUtil.Read<Simple, SimpleMap>(filePath);
            Assert.AreEqual(15, simples.Count);

            foreach (var simple in simples)
            {
                Console.WriteLine("Id:{0}, Name:{1}", simple.Id, simple.Name);
            }
        }

        [Test]
        public void Write_Simple_Normal()
        {
            var simples = new List<Simple>
            {
                new Simple{Id = 1, Name = "A"},
                new Simple{Id = 2, Name = "B"},
                new Simple{Id = 3, Name = "C"}
            };

            _csvUtil.Write(@"C:\simple.csv", simples);
        }
    }

    public class Simple
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public sealed class SimpleMap : CsvClassMap<Simple>
    {
        public SimpleMap()
        {
            Map(n => n.Id).Name("Id");
            Map(n => n.Name).Name("Name");
        }
    }
}