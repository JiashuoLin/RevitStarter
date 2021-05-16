﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RevitStarter.ApplicationPackageModel
{
    public class ComponentEntry
    {
        [XmlAttribute("AppName")]
        public string AppName { get; set; }

        [XmlAttribute("ModuleName")]
        public string ModuleName { get; set; }
    }
}
