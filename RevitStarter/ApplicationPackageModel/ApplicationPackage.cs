using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RevitStarter.ApplicationPackageModel
{
    [XmlRoot("ApplicationPackage")]
    public class ApplicationPackage
    {
        [XmlElement("Components")]
        public Components[] Components { get; set; }
    }
}
