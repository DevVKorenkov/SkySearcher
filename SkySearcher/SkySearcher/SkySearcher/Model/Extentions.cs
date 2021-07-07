using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkySearcher.Model
{
    public static class Extentions
    {
        /// <summary>
        /// (Расширение) Метод получение экзкмпляров из коллекции в соответствии с запросом
        /// </summary>
        /// <param name="propertys"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public static DirectoryEntry Where(this List<DirectoryEntry> propertys, string propValue)
        {
            DirectoryEntry temp = null;

            foreach (DirectoryEntry t in propertys)
            {
                foreach (PropertyValueCollection x in t.Properties)
                {
                    if($"{x.Value}" == propValue)
                    {
                        temp = t;
                    }
                }
            }

            return temp;
        }
    }
}
