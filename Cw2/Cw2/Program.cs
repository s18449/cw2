using CsvHelper;
using Cw2.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;

namespace Cw2
{
    class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length > 3)
            {
                File.WriteAllText("łog.txt", "Incorrect number of arguments provided: " + args.Length + " arguments");
                throw new ApplicationException("Incorrect number of arguments provided: " + args.Length + " arguments");
            }

            string CsvFileName = "dane.csv";
            string ResultFileName = "żesult.xml";
            string DataType = "xml";

            if (args.Length == 3)
            {
                CsvFileName = args[0];
                ResultFileName = args[1];
                DataType = args[2];
            } 

            try
            {
                using (StreamReader reader = new StreamReader(CsvFileName))
                using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    Student student = new Student();
                    csv.Configuration.HasHeaderRecord = false;
                    List<Student> records = csv.GetRecords<Student>().ToList();

                    for(int i = 0; i < records.Count; i++)
                    {
                        foreach(PropertyInfo propertyInfo in records.ElementAt(i).GetType().GetProperties())
                        {
                            if(propertyInfo.PropertyType == typeof(string))
                            {
                                string value = (string)propertyInfo.GetValue(records.ElementAt(i));
                                if(string.IsNullOrEmpty(value))
                                {
                                    records.RemoveAt(i);
                                }
                            }
                               
                        }
                    }

                    FileStream writer = new FileStream(ResultFileName, FileMode.Create);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("uczelnia"));
                    serializer.Serialize(writer, records);
                    writer.Close();

                    string jsonString = JsonSerializer.Serialize(records);
                    File.WriteAllText("data.json", jsonString);
                }
            } catch (FileNotFoundException ex)
            {
                File.WriteAllText("łog.txt", ex.Message);
            }
        }
    }
}
