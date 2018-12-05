using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Claim
    {
        public uint LeftPosition { get; private set;  }
        public uint Width { get; private set; }

        public uint TopPosition { get; private set; }
        public uint Height { get; private set; }

        public Claim(uint left, uint top, uint width, uint height)
        {
            if(left + width > 1000 || top + height > 1000)
            {
                throw new ArgumentOutOfRangeException("You went off the canvas!");
            }


            LeftPosition = left;
            Width = width;

            TopPosition = top;
            Height = height;
        }

        public bool isSquareClaimed(uint x, uint y)
        {
            
            if (x < LeftPosition || y < TopPosition // Claim is to the right or below this square            
                || x >= LeftPosition + Width || y >= TopPosition + Height) // Claim is to the left or above this square
            { 
                return false;
            }

            return true;
        }
    }
}
