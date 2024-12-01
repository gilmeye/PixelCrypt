using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class Pixel : Color
    {
        public double A { get; set; }

        public Pixel(int r, int g, int b, double A) : base(r, g, b)
        {
            this.A = A;
        }


        public override string ToString()
        {
            return this.R + " " + this.G + " " + this.B + " " + this.A;
        }
    }
}
