using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkySearcher.Model
{
    [Serializable, XmlRoot(ElementName = "Errors")]
    public class PcSaveErrors
    {
        public string ErrorTime { get; set; }

        public string PcName { get; set; }

        public string Reason { get; set; }

        public PcSaveErrors(string errorTime, string pcName, string reason)
        {
            ErrorTime = errorTime;

            PcName = pcName;

            Reason = reason;
        }

        public PcSaveErrors() { }
    }
}
