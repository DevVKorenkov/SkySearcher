using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkySearcher.Model
{
    class SerializeXml
    {
        private List<PcSaveErrors> _serializeObject;

        public SerializeXml(List<PcSaveErrors> serializeObject)
        {
            _serializeObject = serializeObject;
        }

        public SerializeXml() { }

        void Serialize()
        {
            XmlSerializer serialize = new XmlSerializer(_serializeObject.GetType());

            using (FileStream fs = new FileStream("NowErrors.xml", FileMode.OpenOrCreate))
            {
                serialize.Serialize(fs, _serializeObject);
            }
        }

        public void Start()
        {
            Serialize();
        }

        public object DeSerialize()
        {
            if (File.Exists("NowErrors.xml"))
            {
                List<PcSaveErrors> tempList = new List<PcSaveErrors>();

                XmlSerializer serialize = new XmlSerializer(tempList.GetType());

                using (FileStream reader = new FileStream("NowErrors.xml", FileMode.OpenOrCreate))
                {
                    tempList = (List<PcSaveErrors>)serialize.Deserialize(reader);
                }

                return tempList;
            }
            else
                throw new Exception("Отсутствует файл ошибок");
        }
    }
}
