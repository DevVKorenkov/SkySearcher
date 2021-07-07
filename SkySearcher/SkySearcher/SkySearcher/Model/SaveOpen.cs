using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearcher.Model
{
    class SaveOpen
    {
        /// <summary>
        ///  Метод выбора файла Excel
        /// </summary>
        /// <returns></returns>
        public string OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Файл Excel (*.xls;*.xlsx)|*.xls;*.xlsx";

            if(ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
