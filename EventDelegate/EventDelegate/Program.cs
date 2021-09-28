using System;

namespace EventDelegate
{
    class Program
    {
        
             
        static void Main(string[] args)
        {
            AlphaNumericCollector alphaNumericCollector = new();
            StringCollector stringCollector = new();
            int switch_on = 100;
            do
            {
                switch (switch_on)
                {
                    case 100:
                        Console.WriteLine("1 - Delegate\n" +
                                          "2 - Event\n" +
                                          "3 - Action\n" +
                                          "4 - view AlphaNumericCollector list\n" +
                                          "5 - view StringCollector list\n" +
                                          "0 - exit");
                        try
                        {
                            switch_on = int.Parse(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case 1:
                        ConsoleMenuDelegate consoleMenuDelegate = new();
                        consoleMenuDelegate.TextDelegate += alphaNumericCollector.Action;
                        consoleMenuDelegate.TextDelegate += stringCollector.Action;
                        consoleMenuDelegate.Start();
                        consoleMenuDelegate.TextDelegate -= alphaNumericCollector.Action;
                        consoleMenuDelegate.TextDelegate -= stringCollector.Action;
                        switch_on = 100;
                        break;
                    case 2:
                        ConsoleMenuEvent consoleMenuEvent = new();
                        consoleMenuEvent.Notify += alphaNumericCollector.Action;
                        consoleMenuEvent.Notify += stringCollector.Action;
                        consoleMenuEvent.Start();
                        consoleMenuEvent.Notify -= alphaNumericCollector.Action;
                        consoleMenuEvent.Notify -= stringCollector.Action;
                        switch_on = 100;
                        break;
                    case 3:
                        ConsoleMenuAction consoleMenuAction = new();
                        consoleMenuAction.ActionNotify += alphaNumericCollector.Action;
                        consoleMenuAction.ActionNotify += stringCollector.Action;
                        consoleMenuAction.Start();
                        consoleMenuAction.ActionNotify -= alphaNumericCollector.Action;
                        consoleMenuAction.ActionNotify -= stringCollector.Action;
                        switch_on = 100;
                        break;
                    case 4:
                        Console.WriteLine(alphaNumericCollector.ToString());
                        switch_on = 100;
                        break;
                    case 5:
                        Console.WriteLine(stringCollector.ToString());
                        switch_on = 100;
                        break;
                    default:
                        break;
                }
            } while (switch_on != 0);
        }
       
        
    }
}
