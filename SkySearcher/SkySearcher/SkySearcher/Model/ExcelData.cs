using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;

namespace SkySearcher.Model
{
    class ExcelData
    {
        /// <summary>
        /// Свойство имени ПК
        /// </summary>
        public string PcName { get; set; }

        /// <summary>
        /// Свойтсов подразделения
        /// </summary>
        public string Dep { get; set; }

        /// <summary>
        /// Свойстов инвентарного номера
        /// </summary>
        public string InvNum { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pcName"></param>
        /// <param name="dep"></param>
        /// <param name="invNum"></param>
        public ExcelData(string pcName, string dep, string invNum)
        {
            PcName = pcName;
            Dep = dep;
            InvNum = invNum;
        }
    }
}
