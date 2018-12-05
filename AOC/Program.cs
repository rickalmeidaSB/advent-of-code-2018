using AOC.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Program
    {

        static void Main(string[] args)
        {
            var time = new Stopwatch();
            time.Start();

            var day = new Day4();
            var result = day.GetResult();

            time.Stop();
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine("Time: " + time.ElapsedMilliseconds.ToString());
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
