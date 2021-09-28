using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegate
{
    class ConsoleMenuDelegate
    {
        public delegate void CollectingTextDelegate(string text);
        public CollectingTextDelegate TextDelegate { get; set; }
        
       
        private bool healty;
        public void Start()
        {
            Console.WriteLine("Write text, if you want to go out write \"stop\"");
            healty = true;
            while (healty)
            {
                string text = Console.ReadLine();
                if (text == "stop")
                {
                    healty = !healty;
                    break;
                }
                TextDelegate(text);
            }
        }
    }
}
