using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class Test
    {


        public static void TestText()
        {
            Console.WriteLine(DS.Text);
            Console.ReadLine();
        }


        public static void TestPixels()
        {
            foreach (Pixel px in DS.Pixels)
            {
                Console.WriteLine(px);
            }
            Console.ReadLine();
        }

        
        public static void TestpCount()
        {
            foreach (var row in DS.pCount)
            {
                Console.WriteLine(row.Key + " - " + row.Value);
            }
            Console.ReadLine();
        }

        public static void TestKey()
        {
            foreach (var row in DS.Key)
            {
                Console.WriteLine(row.Key + " - " + row.Value);
            }
            Console.ReadLine();
        }







        public static void Key()
        {
            bool good = true;
            foreach(var row in DS.Key)
            {
                char myKey = Encrypt.ColorKey.FirstOrDefault(x => x.Value.R == row.Key.R && x.Value.G == row.Key.G && x.Value.B == row.Key.B).Key;
                if(myKey != row.Value)
                {
                    good = false;
                }
            }

            if (!good)
            {
                DS.Key.Clear();
                foreach (var row in DS.pCount)
                {
                    char myKey = Encrypt.ColorKey.FirstOrDefault(x => x.Value.R == row.Key.R && x.Value.G == row.Key.G && x.Value.B == row.Key.B).Key;
                    DS.Key.Add(row.Key, myKey);
                }
            }
        }


    }
}
