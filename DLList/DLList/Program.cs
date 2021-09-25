using System;

namespace DLList
{
    class Program
    {
        static void Main(string[] args)
        {
            DLList<int> list = new();
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("list.PushFront();");
            list.PushFront(1);
            list.PushFront(2);
            list.PushFront(3);
            list.PushFront(4);
            list.PushFront(5);
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            Console.WriteLine("list.PushBack();");
            list.PushBack(6);
            list.PushBack(7);
            list.PushBack(8);
            list.PushBack(9);
            list.PushBack(10);
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine();
            int tmp = list.PopFront();
            Console.WriteLine($"list.PopFront(); return {tmp}");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine();
            tmp = list.PopBack();
            Console.WriteLine($"list.PopBack(); return {tmp}");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine();
            Console.WriteLine("list.Remove(data);");
            int ind = -1;
            foreach (var item in list)
            {
                ind++;
                if (ind == 5)
                {
                    Console.Write($"[list.Remove({item}); index {ind}] ");
                    list.Remove(item);
                }
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            ind = 3;
            Console.WriteLine();
            Console.WriteLine($"list[{ind}]; return {list[ind]}");
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine($"list[{ind}] = {43}");
            list[ind] = 43;
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            list.InsertAfter(list[^1], 222);
            Console.WriteLine();
            Console.WriteLine("list.InsertAfter(list[^1],222);");
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            //list[34] = 4; // Out of range Exception
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine();
            Console.WriteLine("list.Cleare();");
            list.Cleare();
            Console.WriteLine($"list.Length; return {list.Length}");
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Console.WriteLine(list.First); // null reference exception
            Console.ReadKey();
        }
    }
}
