using AOC.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day2 : AdventDay
    {
        public string GetResult()
        {
            string part1 = "";
            string part2 = "";

            try
            {
                var lines = File.ReadLines(@"./Day2/input.txt");

                part1 = GetFirstResult(lines).ToString();
                part2 = GetSecondResult(lines);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return part1 + Environment.NewLine + part2;
        }

        private int GetFirstResult(IEnumerable<string> lines)
        {
            int twoMatchingChars = 0;
            int threeMatchingChars = 0;

            foreach (var line in lines)
            {
                var d = new Dictionary<char, int>();
                foreach (var letter in line)
                {
                    int count = 0;
                    if (d.TryGetValue(letter, out count))
                    {
                        d[letter] = count + 1;
                    }
                    else
                    {
                        d.Add(letter, 1);
                    }
                }

                if (d.ContainsValue(3))
                {
                    threeMatchingChars += 1;
                }
                if (d.ContainsValue(2))
                {
                    twoMatchingChars += 1;
                }
            }

            return (twoMatchingChars * threeMatchingChars);
        }

        private string GetSecondResult(IEnumerable<string> lines)
        {
            string result = "";

            foreach (var line in lines)
            {
                foreach (var compare in lines)
                {
                    if(line == compare)
                    {
                        continue;
                    }

                    bool isSolution = true;
                    int missedIndex = -1;

                    //jbrenqtllgxnivmwystfukzodp - 26 chars
                    for (int i = 0; i < 26; i++)
                    {
                        if(line[i] != compare[i])
                        {
                            if(missedIndex != -1)
                            {
                                isSolution = false;
                                break;
                            }

                            missedIndex = i;
                        }
                    }

                    if (isSolution)
                    {
                        result = line.Remove(missedIndex, 1);
                        break;
                    }                    
                }

                if(result.Length > 0)
                {
                    break;
                }
            }

            return result;
        }
    }
}
