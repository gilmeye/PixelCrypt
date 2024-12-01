using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using t = System.Timers;

namespace finalProj
{
    internal class Decrypt
    {
        static int index = 0;
        static Random rnd = new Random();
        public static t.Timer aTimer;


        public static void FillpCount()
        {
            int totCount = DS.Pixels.Count;
            foreach (Pixel pix in DS.Pixels)
            {
                if (DS.pCount.ContainsKey(pix))
                {
                    DS.pCount[pix]++;
                }
                else
                {
                    DS.pCount[pix] = 1;
                }
                
            }

            List<Color> colors = new List<Color>();
            foreach (Color co in DS.pCount.Keys)
            {
                colors.Add(co);
            }

            foreach (Color co in colors)
            {
                DS.pCount[co] /= totCount;
            }
        }



        public static void FillMark()
        {
            string prev;
            string next;
            int count;
            DataTable dt = DbService.SelectDB();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                prev = dt.Rows[i][0].ToString();
                next = dt.Rows[i][1].ToString();
                count = int.Parse(dt.Rows[i][2].ToString());
                if (DS.Mark.ContainsKey(prev))
                {
                    DS.Mark[prev].Add(new Next(next, count));
                }
                else
                {
                    DS.Mark[prev] = new List<Next>();
                    DS.Mark[prev].Add(new Next(next, count));
                }
            }
        }


        public static void FillFrequency()
        {
            DataTable dt = DbService.SelectFrequency();
            int totCount = DbService.CountLetters();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                DS.Frequency[dt.Rows[i][0].ToString()[0]] = int.Parse(dt.Rows[i][1].ToString()) / (double)totCount;
            }
            
        }


        public static void CreateKey()
        {
            
            List<Color> colors = new List<Color>();
            foreach (var pix in DS.pCount)
            {
                double c = 0;
                Color p = null;
                foreach (var pix2 in DS.pCount)
                {
                    if(pix2.Value > c && !colors.Contains(pix2.Key))
                    {
                        c = pix2.Value;
                        p = pix2.Key; 
                    }
                    
                }

                colors.Add(p);
            }


            foreach (Color c in colors)
            {
                double lowdiff = double.MaxValue;
                char letter = ' ';
                Color color = c;
                foreach (var fr in DS.Frequency)
                {
                    double diff = Math.Abs(fr.Value - DS.pCount[c]);
                    if ((diff < lowdiff) && !DS.Key.ContainsValue(fr.Key))
                    {
                        lowdiff = diff;
                        letter = fr.Key;
                    }
                }

                if (letter != ' ')
                {
                    DS.Key[color] = letter;
                }
                else
                {
                    throw new Exception();
                }
            }
            Test.Key();
        }



        public static void AddLetters()
        {
            int[] counter = new int[26];
            foreach (Pixel pixel in DS.Pixels)
            {
                Color c = pixel;
                char cr = '\0';
                if (DS.Key.ContainsKey(c))
                {
                    cr = DS.Key[c];
                    if (DS.tBank == null)
                    {
                        DS.tBank = new Tree<Bank>(new Bank((int)(255 / pixel.A)));
                    }
                    else if (DS.tBank.Find(new Bank((int)(255 / pixel.A))) == null)
                    {
                        DS.tBank.Add(new Bank((int)(255 / pixel.A)));
                    }
                    Bank b = DS.tBank.Find(new Bank((int)(255 / pixel.A))).Data;
                    b.add(cr);
                    counter[cr - 'a']++;

                }
                else
                {
                    throw new Exception();
                }
            }
        }


        public static bool isEmpty(Tree<Bank> Bank)
        {

            if(Bank == null) return true;
            return (Bank.Data.BankEmpty()) && isEmpty(Bank.Left) && isEmpty(Bank.Right);
        }

        public static string LastThree()
        {
            string[] strings = DS.Text.Split(' ');
            string last = "";
            int len = (strings.Length < 3)? strings.Length : 3;
            for (int i = len; i > 0; i--)
            {
                last += strings[strings.Length - i];
                if(i > 1)
                {
                    last += " ";
                }
            }

            return last;
        }


        public static bool WordPossible(string word)
        {
            int len = word.Length;
            char[] counter = new char[26];
            Bank b = null;
            b = DS.tBank.Find(new Bank(len)).Data;
            if(b == null) return false;
            foreach(char c in word)
            {
                counter[c - 'a']++;
            }

            foreach (char c in word)
            {
                if(!b.data.ContainsKey(c)) return false;
                if(counter[c - 'a'] > b.data[c]) return false;
            }

            return true;
        }


        public static void RemoveLetters(string word)
        {
            int len = word.Length;
            char[] counter = new char[26];
            Bank b = null;
            b = DS.tBank.Find(new Bank(len)).Data;
            foreach (char c in word)
            {
                b.data[c]--;
            }
        }


        public static void addWord(string word)
        {
            if(DS.Text.Length == 0)
            {
                DS.Text += word;
            }
            else
            {
                DS.Text += " " + word;
            }
        }


        public static void RemoveWord()
        {

            string[] words = DS.Text.Split(' ');
            if(words.Length == 1)
            {
                DS.Text = "";
            }
            else
            {
                string newT = "";
                int i;
                for (i = 0; i < words.Length - 2; i++)
                {
                    newT += words[i] + " ";
                }
                newT += words[i];
                DS.Text = newT;
            }
        }


        public static void ReturnLetters(string word)
        {
            int len = word.Length;
            char[] counter = new char[26];
            Bank b = null;
            b = DS.tBank.Find(new Bank(len)).Data;
            foreach (char c in word)
            {
                b.data[c]++;
            }
        }



        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }




        public static bool GenerateText()
        {
            int tChance = 0;
            double cCount = 0;
            double rNum;
            string LtWords = null;
            List <Next> possible = null;
            Next Chosen = null;

            if (isEmpty(DS.tBank))
            {
                PrintText();
                return true;
            }

            LtWords = LastThree();
            try
            {
                possible = DeepCopy(DS.Mark[LtWords]);
            }
            catch (Exception e)
            {
                return false;
            }
            if(possible == null) return false;
            while (possible.Count != 0) 
            {
                foreach (Next next in possible)
                {
                    tChance += next.count;
                }

                rNum = rnd.NextDouble();
                foreach (Next next in possible)
                {
                    cCount += ((double)next.count / tChance);
                    if(cCount >= rNum) 
                    {
                        Chosen = next;
                        possible.Remove(next);
                        tChance -= next.count;
                        break;
                    }
                }
                tChance = 0;
                cCount = 0;
                if (WordPossible(Chosen.word))
                {
                    RemoveLetters(Chosen.word);
                    addWord(Chosen.word);
                    if (GenerateText())
                    {
                        return true;
                    }

                    RemoveWord();
                    ReturnLetters(Chosen.word);
                }
            }


            return false;
        }

        public static void PrintText()
        {
            aTimer = new t.Timer(500);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            aTimer.Start();
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();


        }



        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            int len = rnd.Next(2, 7);
            int i;
            for (i = 0; i < len && index < DS.Text.Length; i++)
            {
                while (DS.Text[index] != ' ' && i < DS.Text.Length && index + len < DS.Text.Length)
                {
                    Console.Write(DS.Text[index]);
                    index++;
                }
                if (index < DS.Text.Length)
                {
                    Console.Write(DS.Text[index]);
                    index++;
                }

      

            }

            if(index == DS.Text.Length)
            {
                aTimer.Stop();
                aTimer.Dispose();
            }
        }





    }
}
