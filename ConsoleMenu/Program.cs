using System;
using System.Threading;
using LinqStructure;

namespace ConsoleMenu
{
    class Program
    {
        private static LinqService _service = LinqService.Service;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!\nLet's work with users data!");
            Thread.Sleep(1200);
            Console.Clear();

            Menu.MenuMap();

        }
    }
}
