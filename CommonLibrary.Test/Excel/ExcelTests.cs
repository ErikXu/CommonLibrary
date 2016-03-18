using System;
using System.Collections.Generic;
using CommonLibrary.Client.Excel;
using CommonLibrary.Client.Excel.Impl;
using NUnit.Framework;

namespace CommonLibrary.Test.Excel
{
    [TestFixture]
    public class ExcelTests
    {
        private IExcelUtil _excelUtil;

        [SetUp]
        public void SetUp()
        {
            _excelUtil = new ExcelUtil();
        }

        [Test]
        public void Export_Normal()
        {
            var users = new List<ExcelUser>
            {
                new ExcelUser{Name = "张三", BirthDate = new DateTime(1990,12,21), Age = 20},
                new ExcelUser{Name = "李四", BirthDate = new DateTime(1987,11,11), Age = 25},
                new ExcelUser{Name = "王五", BirthDate = new DateTime(1988,1,1), Age = 24}
            };

            var sheet1 = new Sheet
            {
                Name = "用户信息",
                Cells = new List<Cell> { new Cell("姓名", "Name"), new Cell("生日", "BirthDate", "yyyy-mm-dd"), new Cell("年龄", "Age") },
                Rows = new List<dynamic>(users)
            };

            var salaries = new List<ExcelSalary>
            {
                new ExcelSalary{Name = "张三", Date = new DateTime(2000,12,21), Salary = 1926.12},
                new ExcelSalary{Name = "李四", Date = new DateTime(2000,12,21), Salary = 1226.12},
                new ExcelSalary{Name = "王五", Date = new DateTime(2000,12,21), Salary = 121126.12}
            };

            var sheet2 = new Sheet
            {
                Name = "工资信息",
                Cells = new List<Cell> { new Cell("姓名", "Name"), new Cell("日期", "Date", "yyyy/mm/dd"), new Cell("工资", "Salary") },
                Rows = new List<dynamic>(salaries)
            };

            _excelUtil.ExportToFile(@"D:\simple.xls", sheet1, sheet2);
        }
    }

    public class ExcelUser
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
    }

    public class ExcelSalary
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Salary { get; set; }
    }
}