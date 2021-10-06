using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XML_JSON_BINARY_SERELIZATION_
{
    class DataManagerXML
    {
        public List<Student> LoadDataXmlSERELIZATION(string path)
        {
            List<Student> students = new();
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Student>));
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
                    students = formatter.Deserialize(stream) as List<Student>;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return students;
        }
        public void SaveDataXmlSERELIZATION(List<Student> students, string path,string ns)
        {
            
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Student>),ns);
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
                    formatter.Serialize(stream, students);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Student> LoadDataXml(string path,string Namespace)
        {
            List<Student> students = new();
            try
            {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            using (XmlReader reader = XmlReader.Create(path, settings))
            {
                reader.MoveToContent();
                while (reader.Read() && reader.NodeType == XmlNodeType.Element && reader.Name == "Student")
                {
                    Student student = new();
                    student.FirstName = reader["firstName"];
                    student.LastName = reader["lastName"];
                    reader.Read();                    
                    student.SetBirthDate(reader.ReadElementContentAsString("BirthDate", Namespace));
                    student.Email = reader.ReadElementContentAsString("Email", Namespace);
                    student.PhoneNumber = reader.ReadElementContentAsString("PhoneNumber", Namespace);
                    student.ExtraData = ReadExtraDataXML(reader,Namespace);
                    reader.Read();
                    student.Courses = ReadCoursesXML(reader,Namespace);
                    students.Add(student);
                    reader.Read();
                }
            }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return students;
        }
        public void SaveDataXml(List<Student> students, string path, string Namespace)
        {
            try
            {

           
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
            using (XmlWriter writer = XmlWriter.Create(path, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Students", Namespace);
                foreach (var item in students)
                {
                    writer.WriteStartElement("Student", Namespace);
                    writer.WriteStartAttribute("firstName");
                    writer.WriteValue(item.FirstName);
                    writer.WriteStartAttribute("lastName");
                    writer.WriteValue(item.LastName);
                    writer.WriteEndAttribute();

                    writer.WriteStartElement("BirthDate", Namespace);
                    writer.WriteValue(item.BirthDate.ToShortDateString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Email", Namespace);
                    writer.WriteValue(item.Email);
                    writer.WriteEndElement();

                    writer.WriteStartElement("PhoneNumber", Namespace);
                    writer.WriteValue(item.PhoneNumber);
                    writer.WriteEndElement();

                    writer.WriteStartElement("ExtraData", Namespace);
                    WriteExtraDataXML(item, writer, Namespace);
                    writer.WriteEndElement();
                    
                    writer.WriteStartElement("Courses", Namespace);
                    WriteCoursesXML(item, writer, Namespace);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void WriteExtraDataXML(Student student, XmlWriter writer,string Namespace)
        {
            try
            {
            foreach (var item in student.ExtraData)
            {
                writer.WriteStartElement("ExtraDataElement", Namespace);
                writer.WriteStartAttribute("name");
                writer.WriteValue(item.Key);
                writer.WriteEndAttribute();
                writer.WriteValue(item.Value);
                writer.WriteEndElement();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void WriteCoursesXML(Student student, XmlWriter writer, string Namespace)
        {
            try
            {
            foreach (var item in student.Courses)
            {
                writer.WriteStartElement("ExtraDataElement", Namespace); 
                writer.WriteValue(item);
                writer.WriteEndElement();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static Dictionary<string,string> ReadExtraDataXML(XmlReader xmlReader,string ns)
        {
            Dictionary<string, string> extraData = new();
            try { 
            xmlReader.Read();
            while (xmlReader.NodeType != XmlNodeType.EndElement)
            {
                extraData.Add(xmlReader["name"], xmlReader.ReadElementContentAsString("ExtraDataElement", ns));
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return extraData;
        }
        public static List<string> ReadCoursesXML(XmlReader xmlReader, string ns)
        {
                        List<string> courses = new();
            try
            {
            xmlReader.Read();
            while (xmlReader.NodeType != XmlNodeType.EndElement)
            {
                courses.Add(xmlReader.ReadElementContentAsString("Course", ns));
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return courses;
        }
    }
}
