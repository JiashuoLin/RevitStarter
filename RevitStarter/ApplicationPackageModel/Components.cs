using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RevitStarter.ApplicationPackageModel
{
    public class Components
    {
        [XmlElement("RuntimeRequirements")]
        public RuntimeRequirements RuntimeRequirements { get; set; }

        [XmlElement("ComponentEntry")]
        public ComponentEntry ComponentEntry { get; set; }

    }
}
