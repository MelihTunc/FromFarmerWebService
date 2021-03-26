using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace FromFarmer.Utilities.Operations
{
    public class ExcelOperation
    {
        private string path = "";
        private string sheetName = "";
        public ExcelOperation(string _path, string _sheetName)
        {
            path = _path;
            sheetName = _sheetName;
        }

        public DataTable ReadData()
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    string relId = doc.WorkbookPart.Workbook.Descendants<Sheet>().First(s => sheetName.Equals(s.Name)).Id;
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(relId) as WorksheetPart).Worksheet;

                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                    foreach (Row row in rows)
                    {
                        if (row.RowIndex.Value == 1)
                        {
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                dt.Columns.Add(GetValue(doc, cell));
                            }
                        }
                        else
                        {
                            dt.Rows.Add();
                            int i = 0;
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                i++;
                            }
                        }
                    }
                }
            }
            return dt;
        }

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = " ";

            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
            }
            return value;
        }

    }
}
