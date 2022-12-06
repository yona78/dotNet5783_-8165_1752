using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8165();
            Welcome1752();
            Console.ReadKey();
        }

        static partial void Welcome1752();
        private static void Welcome8165()
        {
            Console.Write("what is your name: ");
            String x = Console.ReadLine();
            Console.WriteLine("{0} is a good name", x);
        }

    }
}
