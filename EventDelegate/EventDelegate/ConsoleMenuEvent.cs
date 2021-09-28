using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegate
{
    class ConsoleMenuEvent
    {
        public delegate void CollectingText(string text);
        public event CollectingText Notify;
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
                Notify?.Invoke(text);
            }

        }
    }
}
