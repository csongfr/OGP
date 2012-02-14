using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OGP.ValueObjects
{
    public class VOCache
    {                                     
        [XmlElement(typeof(string), ElementName = "PROJET")]
        public string DernierProjetOuvert { get; set; }
    }
}
