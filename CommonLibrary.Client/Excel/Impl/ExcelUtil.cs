using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CommonLibrary.Client.Excel.Impl
{
    public class ExcelUtil : IExcelUtil
    {
        public void Export(Stream stream, params Sheet[] sheets)
        {
            var workbook = new HSSFWorkbook();

            foreach (var sheet in sheets)
            {
                var workSheet = workbook.CreateSheet(sheet.Name);
                CreateHeader(workSheet, sheet.Cells);
                FillRows(workbook, workSheet, sheet.Cells, sheet.Rows);
            }

            workbook.Write(stream);
        }

        public void Export(string filePath, params Sheet[] sheets)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                Export(fs, sheets);
            }
        }

        private static void CreateHeader(ISheet workSheet, IEnumerable<Cell> cells)
        {
            var header = workSheet.CreateRow(0);

            var currentCol = 0;

            foreach (var cell in cells)
            {
                header.CreateCell(currentCol).SetCellValue(cell.Title);
                currentCol++;
            }
        }

        private void FillRows(HSSFWorkbook workbook, ISheet workSheet, List<Cell> cells, dynamic rows)
        {
            var dateStyle = workbook.CreateCellStyle();
            var format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            var currentRow = 1;

            if (rows == null)
            {
                return;
            }
            foreach (var row in rows)
            {
                var workRow = workSheet.CreateRow(currentRow);
                var currentCol = 0;
                var type = row.GetType();

                foreach (var cell in cells)
                {
                    var workCell = workRow.CreateCell(currentCol);
                    SetCellValue(workbook, workCell, type, row, cell);

                    currentCol++;
                }
                currentRow++;
            }
        }

        private static void SetCellValue(HSSFWorkbook workbook, ICell workCell, Type type, dynamic row, Cell cell)
        {
            var value = type.GetProperty(cell.Field).GetValue(row);
            if (value == null)
            {
                return;
            }

            if (value is DateTime)
            {
                workCell.SetCellValue((DateTime)value);
            }
            else if (value is int)
            {
                workCell.SetCellValue((int)value);
            }
            else if (value is double)
            {
                workCell.SetCellValue((double)value);
            }
            else 
            {
                workCell.SetCellValue(value.ToString());
            }

            if (!string.IsNullOrWhiteSpace(cell.Format))
            {
                var cellStyle = workbook.CreateCellStyle();
                var format = workbook.CreateDataFormat();
                cellStyle.DataFormat = format.GetFormat(cell.Format);
                workCell.CellStyle = cellStyle;
            }
        }
    }
}