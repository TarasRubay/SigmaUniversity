using System;
using System.Linq;
using System.Collections.Generic;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string task1 = "Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert";
            string task2 = "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988";
            string task3 = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";

            int switch_on = 100;
            do
            {
                switch (switch_on)
                {
                    case 100:
                        Console.WriteLine("1 - AddNumberToName\n" +
                                          "2 - SortByBirthday\n" +
                                          "3 - CountTimeAllSongs\n" +
                                          
                                          "0 - exit");
                        try
                        {
                            switch_on = int.Parse(Console.ReadLine());
                            Console.Clear();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case 1:
                        try
                        {
                        Console.WriteLine($"input: {task1}");
                        Console.WriteLine($"outut: {AddNumberToName(task1)}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        switch_on = 100;
                        break;
                    case 2:
                        try
                        {
                        foreach (var item in SortByBirthday(task2))
                        {
                            //Person p = new();
                            //p = item as Person;
                            //Console.WriteLine(p);
                            ////////////////////////////////Як анонімний тип привеси в явний?
                            Console.WriteLine(item);
                        }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);  
                        }
                        switch_on = 100;
                        break;
                    case 3:
                        try
                        {
                            long timeSpanTict = 0;
                            foreach (var item in CountTimeAllSongs(task3))
                            {
                                Console.WriteLine(item);
                                timeSpanTict += item.Ticks;
                            }
                        Console.WriteLine($"Sum all time title: {TimeSpan.FromTicks(timeSpanTict)}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        switch_on = 100;
                        break;
                    
                    default:
                        break;
                }
            } while (switch_on != 0);
        }
       
        public record Person
        {
            public DateTime Birthday { get; set; } = default!;
            public int Age { get; set; } = default!;
            public string Name { get; set; } = default!;
            public override string ToString()
            {
                return $"Name: {Name}; Age: {Age}; Birthday: {Birthday.ToShortDateString()};";
            }
        }
        public static string AddNumberToName(string input)
        {
            return String.Join(", ",Enumerable.Range(1, input.Split(',').Length)
                .Zip(input.Split(','))
                .Select(a => a.First.ToString().Trim() + '.' + a.Second)
                .ToArray());
        }
        public static IEnumerable<dynamic> SortByBirthday(string input)
        {
            return input.Split(';')
                .AsEnumerable()
                .OrderBy(a => (int)((DateTime.Now - new DateTime(
                    int.Parse(a.Split(',')[1].Split('/')[2].Trim()),
                    int.Parse(a.Split(',')[1].Split('/')[1].Trim()),
                    int.Parse(a.Split(',')[1].Split('/')[0].Trim()))).Days / 365.25))
                .Select((a) => new
                {
                    Birthday = new DateTime(
                    int.Parse(a.Split(',')[1].Split('/')[2].Trim()),
                    int.Parse(a.Split(',')[1].Split('/')[1].Trim()),
                    int.Parse(a.Split(',')[1].Split('/')[0].Trim())),
                    Age = ((int)((DateTime.Now - new DateTime(
                    int.Parse(a.Split(',')[1].Split('/')[2].Trim()),
                    int.Parse(a.Split(',')[1].Split('/')[1].Trim()),
                    int.Parse(a.Split(',')[1].Split('/')[0].Trim()))).Days / 365.25)),
                    Name = a.Split(',')[0].Trim()
                });
            
               
        }
        public static IEnumerable<TimeSpan> CountTimeAllSongs(string input)
        {
            
            return from i in input.Split(',')
                   select  TimeSpan.FromSeconds(
                       (int.Parse(i.Split(':')[0]) * 60) + int.Parse(i.Split(':')[1]));
        }
        
    }
}
