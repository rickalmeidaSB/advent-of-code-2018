using AOC.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day4 : AdventDay
    {
        public string GetResult()
        {
            string part1 = "";

            try
            {
                var lines = File.ReadLines(@"./Day4/input.txt");

                part1 = GetFirstResult(lines);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return part1;
        }

        public class Event
        {
            public int month { get; set; }
            public int day { get; set; }
            public int minute { get; set; }
            public int guard { get; set; }
            public bool isAwake { get; set; }

        }
        private string GetFirstResult(IEnumerable<string> lines)
        {
            var Timeline = new Dictionary<DateTime, string>();
            var sleepingMinutes = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var data = line.TrimStart('[')
                        .Split(']');

                var time = DateTime.ParseExact(data[0], "yyyy-MM-dd HH:mm", null);

                Timeline.Add(time, data[1].Trim());
            }

            int currentGuard = -1;
            bool isCurrentlyAwake = true;

            int lastEventMinute = Timeline.OrderBy(d => d.Key).First().Key.Minute;
            bool wasAsleepOneMinuteAgo = false;
            int lastSleepMinute = -1;

            foreach (var ev in Timeline.OrderBy(d => d.Key))
            {
                if (ev.Value.Contains("#"))
                {
                    currentGuard = Convert.ToInt32(ev.Value.Split(new char[] { '#', ' ' })[2]);
                    lastEventMinute = 0;
                    lastSleepMinute = -1;
                    wasAsleepOneMinuteAgo = false;
                }
                else if (ev.Value.Contains("wakes up"))
                {
                    isCurrentlyAwake = true;
                }
                else // falls asleep
                {
                    lastSleepMinute = ev.Key.Minute;
                    isCurrentlyAwake = false;
                }

                if (isCurrentlyAwake)
                {
                    if (wasAsleepOneMinuteAgo)
                    {
                        for (int i = lastSleepMinute; i < ev.Key.Minute; i++)
                        {
                            string key = currentGuard + "x" + i;
                            int count = 0;
                            if (sleepingMinutes.TryGetValue(key, out count))
                            {
                                sleepingMinutes[key] = count + 1;
                            }
                            else
                            {
                                sleepingMinutes.Add(key, 1);
                            }

                        }
                    }
                    wasAsleepOneMinuteAgo = false;
                }
                else
                {
                    wasAsleepOneMinuteAgo = true;
                }
            }

            var guards = new HashSet<int>();
            foreach (var guard in Timeline.Where(v => v.Value.Contains("#")))
            {
                var strGuard = guard.Value.Split(new char[] { '#', ' ' });
                int id = Convert.ToInt32(strGuard[2]);
                guards.Add(id);
            }

            var highestSleepyCount = -1;
            var mostSleepyGuard = -1;

            foreach (var guard in guards)
            {
                int sleepyCount = sleepingMinutes.Where(s => s.Key.Split('x')[0] == guard.ToString()).Sum(c => c.Value);

                if (sleepyCount > highestSleepyCount)
                {
                    highestSleepyCount = sleepyCount;
                    mostSleepyGuard = guard;
                }
            }

            var mostSleepyMinuteForGuard = sleepingMinutes.Where(g => g.Key.Split('x')[0] == mostSleepyGuard.ToString()).OrderByDescending(o => o.Value).First().Key.Split('x')[1];

            var mostSleepyMinuteForASingleGuard = Convert.ToInt32(sleepingMinutes.OrderByDescending(o => o.Value).First().Key.Split('x')[0]);
            var mostSleepyGuardOnASingleMinute = Convert.ToInt32(sleepingMinutes.OrderByDescending(o => o.Value).First().Key.Split('x')[1]);

            return (mostSleepyGuard * Convert.ToInt32(mostSleepyMinuteForGuard)).ToString() + Environment.NewLine + (mostSleepyMinuteForASingleGuard * mostSleepyGuardOnASingleMinute);
        }
    }
}
