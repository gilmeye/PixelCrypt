using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class DS
    {
        public static List<Pixel> Pixels = new List<Pixel>();
        public static Dictionary<Color, double> pCount = new Dictionary<Color, double>();
        public static Dictionary<Color, char> Key = new Dictionary<Color, char>();
        public static Dictionary<char, double> Frequency = new Dictionary<char, double>();
        public static Tree<Bank> tBank = null;
        public static Dictionary<string, List<Next>> Mark = new Dictionary<string, List<Next>>();
        public static string Text = "";
    }
}
