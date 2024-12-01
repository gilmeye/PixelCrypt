using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class Letter : IComparable<Letter>
    {
        public char letter { get; set; }
        public bool isCapital { get; set; } = false; 
        public int wLength { get; set; }

        public Letter(char letter, int wlength)
        {
            this.letter = letter;
            this.wLength = wlength;
            if (letter <= 'Z' && letter >= 'A')
            {
                isCapital = true;
            }
        }


        public int CompareTo(Letter other)
        {
            // If 'other' is not a valid object reference, this instance is greater.
            if (other == null)
                return 1;

            // Use the integer's CompareTo method to compare the wLength values
            return wLength.CompareTo(other.wLength);
        }
    }
}
