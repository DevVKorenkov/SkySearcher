using SkySearcher.HelpedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearcher.Model
{
    class AttributeValueObject : PropChanged
    {
        /// <summary>
        /// Свойство имени атрибута имени ПК
        /// </summary>
        public string AttributePcName { get; set; }

        /// <summary>
        /// Свойство значения имени ПК
        /// </summary>
        public string AttributePcValue { get; set; }

        /// <summary>
        /// Свойство имени атрибута инвентарного номера
        /// </summary>
        public string AttributeDescName { get; set; }
       
        private string attributeDescValue;
        /// <summary>
        /// Свойство значения атрибута инвентарного номера
        /// </summary>
        public string AttributeDescValue 
        {
            get => attributeDescValue;
            set { attributeDescValue = value; OnPropertyChanged(nameof(AttributeDescValue)); }
        }

        private string selectDepInv;
        /// <summary>
        /// Свойство выбранного подразделения
        /// </summary>
        public string SelectDepInv
        {
            get { return selectDepInv; }
            set { selectDepInv = value; OnPropertyChanged(nameof(SelectDepInv)); }
        }

        /// <summary>
        /// коллекция подразделений
        /// </summary>
        public List<string> DepInvs { get; } = new List<string> { "Грузоперевозки", "ДЛ Транс", "Колл центр" };

        private string inputInv;
        /// <summary>
        /// Свойстов введенного инвентарного номера
        /// </summary>
        public string InputInv
        {
            get { return inputInv; }
            set { inputInv = value; OnPropertyChanged(nameof(InputInv)); }
        }
    }
}
