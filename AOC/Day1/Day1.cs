using AOC.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day1 : AdventDay
    {
        public string GetResult()
        {
            int duplicatedFrequency = -1;
            int firstResult = -1;
            HashSet<int> frequencies = new HashSet<int>();
            int frequency = 0;
            try
            {
                var lines = File.ReadLines(@"./Day1/input.txt");

                while (duplicatedFrequency == -1)
                {
                    foreach (var line in lines)
                    {
                        switch (line[0])
                        {
                            case '+':
                                frequency += Convert.ToInt32(line.TrimStart('+'));
                                break;
                            case '-':
                                frequency -= Convert.ToInt32(line.TrimStart('-'));
                                break;
                            default:
                                throw new Exception("Invalid Input");
                        }

                        if (frequencies.Contains(frequency))
                        {
                            duplicatedFrequency = frequency;
                            break;
                        }
                        else
                        {
                            frequencies.Add(frequency);
                        }
                    }

                    if (firstResult == -1)
                    {
                        firstResult = frequency;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return firstResult.ToString() + Environment.NewLine + duplicatedFrequency.ToString();
        }
    }
}
