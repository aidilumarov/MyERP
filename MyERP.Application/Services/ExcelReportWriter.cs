using MyERP.Dtos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyERP.Application.Services
{
    public class ExcelReportWriter : IReportWriter<OrderDto>
    {
        private string _fileExtension = ".xlsx";

        private List<string[]> _headerRows = new List<string[]>
        {
            new string[] { "Order ID", "Order Date", "Order Price" }
        };

        private string _reportName = "Orders Report";

        public ExcelReportWriter()
        {
        }

        public FileInfo WriteReportToFile(IEnumerable<OrderDto> items)
        {
            FileInfo excelFile = new FileInfo(Path.GetTempFileName() + _fileExtension);

            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(_reportName);

                // Determine header range and populate headers
                var headerRange = GetHeaderRange(_headerRows);
                worksheet.Cells[headerRange].LoadFromArrays(_headerRows);

                // Insert rows
                var cellData = ConvertListOfEntitiesToListOfRowObjects(items);
                worksheet.Cells[2, 1].LoadFromArrays(cellData);

                package.SaveAs(excelFile);
            }

            return excelFile;
        }

        private string GetHeaderRange(List<string[]> headerRows)
        {
            return "A1:" + Char.ConvertFromUtf32(headerRows[0].Length + 64) + "1";
        }

        private List<object[]> ConvertListOfEntitiesToListOfRowObjects(IEnumerable<OrderDto> orders)
        {
            var cellData = new List<object[]>();

            foreach (var order in orders)
            {
                cellData.Add(new object[] { order.Id, order.Date.ToShortDateString(), order.Price.ToString() });
            }

            return cellData;
        }
    }
}
