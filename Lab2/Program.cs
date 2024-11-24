using System;
using System.IO;

namespace Lab2
{
    internal class Program
    {
        static int[,] board;
        static int N;

        static void Main()
        {
            Task2 task = new Task2();
            int result = task.Start();
            File.WriteAllText(@"Lab2\output.txt", result.ToString());
        }        
    }
}
