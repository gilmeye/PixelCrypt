using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace finalProj
{
    internal class MainProgram
    {




        static void Main(string[] args)
        {

            
            int task = 1;
            Console.WriteLine("Text to Pixel Art Encryption");
            Console.WriteLine("");
            Console.WriteLine("Choose which task you want to preform:");
            Console.WriteLine("0. exit program");
            Console.WriteLine("1. encrypt text");
            Console.WriteLine("2. decrypt text");
            Console.WriteLine("3. Feed text into data base");
            Console.WriteLine("");
            task = int.Parse(Console.ReadLine());


            while (task > 0)
            {

                switch (task)
                {
                    case 1:
                        {
                            Console.WriteLine("Please enter text to encrypt:");
                            DS.Text = Console.ReadLine();
                            Console.WriteLine("");
                            Encrypt.TextToLetters();
                            Encrypt.LettersToPixels();
                            Encrypt.GenerateImage();
                            Encrypt.PrintImage();
                            
                            break;
                        }
                    case 2:
                        {
                            int feed = 0;
                            Console.WriteLine("Decrypting image now...");
                            Console.WriteLine("");
                            //Test.TestPixels();
                            Decrypt.FillpCount();
                            //Test.TestpCount();
                            Decrypt.FillFrequency();
                            Decrypt.CreateKey();
                            //Test.TestKey();
                            Decrypt.AddLetters();
                            Decrypt.FillMark();
                            DS.Text = "";
                            Decrypt.GenerateText();
                            Decrypt.PrintText();
                            Console.WriteLine("would you like to feed this text to the database? \n0 - no \n1- yes");
                            feed = int.Parse(Console.ReadLine());
                            if(feed > 0)
                            {
                                Console.WriteLine("working on it!");
                                FeedToData.InsertText();
                                FeedToData.UpdateFrequency();
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Please enter text to feed to database:");
                            DS.Text = Console.ReadLine();
                            Console.WriteLine("working on it!");
                            FeedToData.InsertText();
                            FeedToData.UpdateFrequency();
                            Console.WriteLine("");
                            
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("input not valid");
                            Console.WriteLine("");
                            break;
                        }
                }

                Console.WriteLine("Task completed! please choose another taks to preform");
                Console.WriteLine("");
                Console.WriteLine("Text to Pixel Art Encryption");
                Console.WriteLine("");
                Console.WriteLine("Choose which task you want to preform:");
                Console.WriteLine("0. exit program");
                Console.WriteLine("1. encrypt text");
                Console.WriteLine("2. decrypt text");
                Console.WriteLine("3. Feed text into data base");
                Console.WriteLine("");
                task = int.Parse(Console.ReadLine());
           
            }
        }
    }
}
