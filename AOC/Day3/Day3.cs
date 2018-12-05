using AOC.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Day3 : AdventDay
    {
        public string GetResult()
        {
            string part1 = "";
            string part2 = "";

            try
            {
                var lines = File.ReadLines(@"./Day3/input.txt");

                part1 = GetFirstResult(lines);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

            return part1 + Environment.NewLine + part2;
        }

        private string GetFirstResult(IEnumerable<string> lines)
        {
            //#1147 @ 537,297: 26x16
            var Claims = new Dictionary<uint, Claim>();
            var Canvas = new Dictionary<string, bool>();
            
            foreach(var line in lines)
            {
                // Remove the hash, remove the spaces, and split on everything else
                var data = line.TrimStart('#')
                    .Replace(" ", string.Empty)
                    .Split(new char[]
                    {
                        '@',
                        ',',
                        ':',
                        'x'
                    });

                Claims.Add(Convert.ToUInt32(data[0]), new Claim(
                    Convert.ToUInt32(data[1]), // left
                    Convert.ToUInt32(data[2]), // top
                    Convert.ToUInt32(data[3]), // width
                    Convert.ToUInt32(data[4])  // height
                    ));
            }

            for (uint x = 0; x < 1000; x++)
            {
                for (uint y = 0; y < 1000; y++)
                {
                    bool isSquareClaimed = false;
                    bool isSquareOverlapped = false;

                    foreach(var claim in Claims)
                    {
                        if (claim.Value.isSquareClaimed(x, y))
                        {
                            if (isSquareClaimed)
                            {
                                isSquareOverlapped = true;
                                break;
                            }
                            else
                            {
                                isSquareClaimed = true;
                            }
                        }
                    }

                    Canvas.Add(x + "x" + y, isSquareOverlapped);
                }
            }

            int squaresWithOverlap = 0;
            foreach(var square in Canvas)
            {
                if(square.Value)
                {
                    squaresWithOverlap += 1;
                }
            }

            uint winningClaim = 0;

            foreach(var claim in Claims)
            {
                var val = claim.Value;
                bool isWinningClaim = true;

                for (uint x = val.LeftPosition; isWinningClaim && x < val.LeftPosition + val.Width; x++)
                {
                    for (uint y = val.TopPosition; isWinningClaim && y < val.TopPosition + val.Height; y++)
                    {
                        if (Canvas[x+"x"+y])
                        {
                            isWinningClaim = false;
                        }
                    }
                }

                if(isWinningClaim)
                {
                    winningClaim = claim.Key;
                    break;
                }
            }

            return squaresWithOverlap.ToString() + Environment.NewLine + winningClaim.ToString();
        }
    }
}
