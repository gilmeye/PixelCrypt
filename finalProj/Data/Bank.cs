using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class Bank : IComparable<Bank>
    {
        public int length { get; set; }
        public Dictionary<char, int> data { get; set; }

        public Bank(int length) 
        { 
            this.length = length;
            data = new Dictionary<char, int>();
            
        }


        public int CompareTo(Bank other)
        {
            // If 'other' is not a valid object reference, this instance is greater.
            if (other == null)
                return 1;

            // Use the integer's CompareTo method to compare the wLength values
            return length.CompareTo(other.length);
        }


        public void add(char c)
        {
            if (data.ContainsKey(c))
            {
                data[c]++;
            }
            else
            {
                data.Add(c, 1);
            }
        }


        public bool BankEmpty()
        {
            foreach (var row in this.data)
            {
                if(row.Value > 0)
                {
                    return false;
                }
            }

            return true;
        }



    }
}
