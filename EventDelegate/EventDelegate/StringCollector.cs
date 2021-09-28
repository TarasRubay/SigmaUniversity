using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegate
{
    class StringCollector
    {
        private List<string> listStrings = new();
        public void Action(string text)
        {
            if(!text.Any(s => Char.IsDigit(s)))
            listStrings.Add(text);
        }
        public override string ToString()
        {
            string seq = "StringCollector\n";
            foreach (var item in listStrings)
            {
                seq += item + "\n";
            }
            return seq;
        }
        
    }
}
