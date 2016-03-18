using System;
using System.Collections.Generic;
using System.IO;
using CommonLibrary.Client.Word;
using CommonLibrary.Client.Word.Impl;
using NUnit.Framework;

namespace CommonLibrary.Test.Word
{
    [TestFixture]
    public class WordTests
    {
        private IWordUtil _wordUtil;

        [SetUp]
        public void SetUp()
        {
            _wordUtil = new WordUtil();
        }

        [Test]
        public void Fill_Normal()
        {
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Word", "Template.docx");

            var items = new List<WordItem>
            {
                new WordItem{Index = 0, Key = "[甲方]", Value = "张三"},
                new WordItem{Index = 1, Key = "[身份证号]", Value = "987654321123456789"},
                new WordItem{Index = 2, Key = "[联系电话]", Value = "13600000000"},
                new WordItem{Index = 3, Key = "[乙方]", Value = "李四"},
                new WordItem{Index = 4, Key = "[注册号]", Value = "88888888"},
                new WordItem{Index = 5, Key = "[法定代表人]", Value = "王五"},
                new WordItem{Index = 6, Key = "[联系地址]", Value = "XXX街XXX号"}
            };

            _wordUtil.FillToFile(@"D:\simple.docx", templatePath, items);
        }
    }
}