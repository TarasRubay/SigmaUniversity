using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegate
{
    class AlphaNumericCollector
    {
        private List<string> listStrings = new();
        public void Action(string text)
        {
            if(text.Any(s => char.IsDigit(s)))
            {
                listStrings.Add(text);
            }
        }
        public override string ToString()
        {
            string seq = "AlphaNumericCollector\n";
            foreach (var item in listStrings)
            {
                seq += item + "\n";
            }
            return seq;
        }
    }
}
