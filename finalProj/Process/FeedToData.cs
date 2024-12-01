using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class FeedToData
    {
        public static void InsertText()
        {
            char[] delimiterChars = { ' ', ':', ';', '\t', '-', '.', ',' };
            string text = DS.Text;
            text = text.Replace("'", "");
            string[] words = text.Split(delimiterChars);
            string s1 = "", s2 = "", s3 = "";
            string sKey = "";
            string sValue = "";

            foreach (var word in words)
            {
                sKey = "";
                if (word.Length > 0)
                {
                    s1 = s2;
                    s2 = s3;
                    s3 = sValue;
                    sValue = word;

                    if (s1.Length > 0)
                    {
                        sKey = s1;
                    }

                    if (s2.Length > 0)
                    {
                        if (sKey.Length > 0)
                        {
                            sKey += " ";
                        }

                        sKey += s2;
                    }

                    if (s3.Length > 0)
                    {
                        if (sKey.Length > 0)
                        {
                            sKey += " ";
                        }

                        sKey += s3;
                    }

                    DbService.InsertNextConnection(sKey, sValue);

                }
            }
        }


        public static void UpdateFrequency()
        {
            foreach (char c in DS.Text)
            {
                if ((c <= 'z' && c >= 'a') || (c <= 'Z' && c >= 'A'))
                {
                    int count = DbService.SelectLetterFreq(c);
                    if (count > 0)
                    {
                        DbService.UpdateLetterFreq(count + 1, c);
                    }
                    else
                    {
                        DbService.InsertLetterFreq(c, 1);
                    }
                }
            }
        }
    }
}
