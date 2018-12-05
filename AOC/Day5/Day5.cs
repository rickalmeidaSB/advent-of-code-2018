using AOC.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day5 : AdventDay
    {
        public string GetResult()
        {
            string part1 = "";

            try
            {
                var lines = File.ReadLines(@"./Day5/input.txt");

                part1 = GetFirstResult(lines);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return part1;
        }

        private string GetFirstResult(IEnumerable<string> lines)
        {
            var line = lines.First();

            var part1 = React(line);

            var secondReactionResults = new Dictionary<char, int>();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                string hi = c.ToString();
                string lo = hi.ToLower();

                secondReactionResults.Add(c, React(line.Replace(hi, string.Empty).Replace(lo, string.Empty)));

                Console.Write(hi);
            }

            Console.Clear();

            var part2 = secondReactionResults.OrderBy(p => p.Value).First().Value;

            return part1.ToString() + Environment.NewLine + part2.ToString();
        }

        private int React(string line)
        {
            int lastLength = -1;
            while (true)
            {
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    string hi = c.ToString();
                    string lo = hi.ToLower();

                    line = line.Replace(hi + lo, String.Empty);
                    line = line.Replace(lo + hi, string.Empty);
                }
                if(lastLength == line.Length)
                {
                    return line.Length;
                }
                else
                {
                    lastLength = line.Length;
                }
            }
        }
    }
}
