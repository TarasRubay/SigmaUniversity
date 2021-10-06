using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XML_JSON_BINARY_SERELIZATION_
{
    class DataManagerJSON
    {
        public void SaveDataJSON(List<Student> students,string path)
        {
            try
            {
                string jsonUs = System.Text.Json.JsonSerializer.Serialize(students);
                File.WriteAllText(path, jsonUs);
                Console.WriteLine($"Save data in {path}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public List<Student> LoadDataJSON(string path)
        {
            List<Student> students = new();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = reader.ReadToEnd();
                    students = JsonConvert.DeserializeObject<List<Student>>(json);
                }
                Console.WriteLine($"Load data from {path}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return students;
        }
    }
}
