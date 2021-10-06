using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace XML_JSON_BINARY_SERELIZATION_
{
    class DataManagerBINARY
    {
        public void SaveDataBINARY(List<Student> students, string path)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                    formatter.Serialize(stream, students);
                Console.WriteLine($"Save data in {path}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public List<Student> LoadDataBINARY(string path)
        {
            List<Student> students = new();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    students = formatter.Deserialize(stream) as List<Student>;
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
