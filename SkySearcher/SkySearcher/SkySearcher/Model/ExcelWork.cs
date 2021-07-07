using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkySearcher.Model
{
    class ExcelWork
    {
        string path;
        bool blocked;

        public ExcelWork(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Метод получения данных из файла Excel
        /// </summary>
        /// <returns></returns>
        public List<ExcelData> GetDataFromXl()
        {
            // Временная коллекция для данных, полученных из файла
            List<ExcelData> tempData = new List<ExcelData>();

            // Создание пустой книги Excel
            XLWorkbook workbook = null;

            // Создание пустой таблицы Excel
            IXLWorksheet workSheet = null;

            // Проверка пути к файлу
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    workbook = new XLWorkbook(path);

                    workSheet = workbook.Worksheet(1);
                }
                catch
                {
                    blocked = true;
                    MessageBox.Show("Возможно файл используется другим приложением");
                }

                // Проверка блокирования файла Excel
                if (!blocked)
                {
                    var rows = workSheet.RangeUsed().RowsUsed();

                    foreach (var r in rows)
                    {
                        // Получение даных из файла Excel
                        ExcelData excelData = new ExcelData(
                            r.Cell(1).Value.ToString(),
                            r.Cell(2).Value.ToString(),
                            r.Cell(3).Value.ToString());

                        tempData.Add(excelData);
                    }
                }
            }

            blocked = false;

            return tempData;
        }

        public string GetPcNamesFromExcel(ObservableCollection<AttributeValueObject> getingProp)
        {
            string SearchPcName = string.Empty;

            foreach (var n in getingProp)
            {
                if (n != getingProp.Last())
                    SearchPcName = SearchPcName + n.AttributePcValue + ';';
                else
                    SearchPcName = SearchPcName + n.AttributePcValue;
            }

            return SearchPcName;
        }

        //private bool ValidateExcel(IXLWorksheet worksheet)
        //{
        //    if (worksheet.ColumnCount() > 3 && worksheet.ColumnCount() < 3)
        //    {
        //        MessageBox.Show("Количество колонок файла не соответствует 3.");

        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
