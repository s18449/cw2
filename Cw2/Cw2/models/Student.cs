using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cw2.models
{
    [Serializable]
    public class Student
    {
        [Index(0)]
        [JsonPropertyName("Imie")]
        [XmlElement(ElementName = "Imie")]
        public string FirstName { get; set; }

        [Index(1)]
        [XmlAttribute("Nazwisko")]
        public string LastName { get; set; }

        [Index(2)]
        public string Studia { get; set; }

        [Index(3)]
        public string Mode { get; set; }

        [Index(4)]
        public string IndexNumber { get; set; }

        [Index(5)]
        public DateTime Birthdate { get; set; }

        [Index(6)]
        public string Mail { get; set; }

        [Index(7)]
        public string MotherFirstName { get; set; }

        [Index(8)]
        public string FatherFirstName { get; set; }

    }
}
