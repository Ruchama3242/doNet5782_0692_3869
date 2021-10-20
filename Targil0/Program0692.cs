using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome0692();
            welcome3869();
            Console.ReadKey();
        }
        static partial void welcome3869();
        private static void welcome0692()
        {
            Console.WriteLine("Enter yout name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);
        }
    }
}
