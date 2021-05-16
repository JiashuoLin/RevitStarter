using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RevitStarter.ApplicationPackageModel
{
    public class RuntimeRequirements
    {
        [XmlAttribute("OS")]
        public string OS { get; set; }

        [XmlAttribute("Platform")]
        public string Platform { get; set; }

        [XmlAttribute("SeriesMin")]
        public string SeriesMin { get; set; }

        [XmlAttribute("SeriesMax")]
        public string SeriesMax { get; set; }
    }
}
