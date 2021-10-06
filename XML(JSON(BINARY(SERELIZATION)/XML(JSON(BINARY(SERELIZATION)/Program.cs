using System;
using System.Collections.Generic;

namespace XML_JSON_BINARY_SERELIZATION_
{
    class Program
    {
        static void Main(string[] args)
        {

            string XMLpathInput = @"Input_students.xml";
            string XMLpathOutput = @"Output_students.xml";
            string XMLpathSERL = @"Output_studentsSERL.xml";
            string JSONpath = @"Input_studentsJSON.json";
            string BINARYpath = @"Input_studentsBiNARY.bin";
            string ns = @"www.datastudents.com";
            DataManagerXML dataManager = new();
            List<Student>  students = dataManager.LoadDataXml(XMLpathInput, ns);
            students[0].ExtraData.Add("Teacher", "yes");
            students[0].Courses.Add(".NET 5 core");
            students[1].ExtraData.Add("Women", "yes");
            students[1].Courses.Add("HTML");
            dataManager.SaveDataXml(students, XMLpathOutput, ns);
            DataManagerJSON dataManagerJSON = new();
            dataManagerJSON.SaveDataJSON(students, JSONpath);
            List<Student> users = dataManagerJSON.LoadDataJSON(JSONpath);
            DataManagerBINARY dataManagerBINARY = new();
            dataManagerBINARY.SaveDataBINARY(users, BINARYpath);
            List<Student> students1 = dataManagerBINARY.LoadDataBINARY(BINARYpath);
            //dataManager.SaveDataXmlSERELIZATION(students1, XMLpathSERL,ns); ще не працює через неможливіть серилізації словника
            //List<Student> students2 = dataManager.LoadDataXmlSERELIZATION(XMLpathSERL);
            Console.WriteLine("Hello World!");
        }
    }
}
