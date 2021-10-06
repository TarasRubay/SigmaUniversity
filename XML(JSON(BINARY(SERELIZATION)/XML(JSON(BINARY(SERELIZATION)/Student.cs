using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML_JSON_BINARY_SERELIZATION_
{
    [Serializable]
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        //[XmlElement("ExtraData")] 
        //public List<KeyValuePair<string, string>> XMLDictionaryProxy
        //{
        //    get
        //    {
        //        return new List<KeyValuePair<string, string>>(ExtraData);
        //    }
        //    set
        //    {
        //        ExtraData = new Dictionary<string, string>();
        //        foreach (var pair in value)
        //            ExtraData[pair.Key] = pair.Value;
        //    }
        //}
        //[XmlIgnore] // це для серилізації словника ще не працює
        public Dictionary<string, string> ExtraData { get; set; }
        public List<string> Courses { get; set; }
        public void SetBirthDate(string value)
        {
            BirthDate = DateTime.Parse(value);
        }
    }
}
