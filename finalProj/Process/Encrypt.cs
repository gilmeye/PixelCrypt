using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProj
{
    internal class Encrypt
    {


        public static Dictionary<char, Color> ColorKey = new Dictionary<char, Color>()
        {
            {'a', new Color(255, 0, 0)},       // Red
            {'b', new Color(0, 255, 0)},       // Green
            {'c', new Color(0, 0, 255)},       // Blue
            {'d', new Color(255, 255, 0)},     // Yellow
            {'e', new Color(255, 0, 255)},     // Magenta
            {'f', new Color(0, 255, 255)},     // Cyan
            {'g', new Color(128, 0, 0)},       // Maroon
            {'h', new Color(0, 128, 0)},       // DarkGreen
            {'i', new Color(0, 0, 128)},       // Navy
            {'j', new Color(128, 128, 0)},     // Olive
            {'k', new Color(128, 0, 128)},     // Purple
            {'l', new Color(0, 128, 128)},     // Teal
            {'m', new Color(192, 192, 192)},   // Silver
            {'n', new Color(128, 128, 128)},   // Gray
            {'o', new Color(255, 165, 0)},     // Orange
            {'p', new Color(165, 42, 42)},     // Brown
            {'q', new Color(173, 216, 230)},   // LightBlue
            {'r', new Color(240, 128, 128)},   // LightCoral
            {'s', new Color(152, 251, 152)},   // PaleGreen
            {'t', new Color(205, 92, 92)},     // IndianRed
            {'u', new Color(218, 165, 32)},    // GoldenRod
            {'v', new Color(245, 222, 179)},   // Wheat
            {'w', new Color(255, 192, 203)},   // Pink
            {'x', new Color(255, 228, 181)},   // PeachPuff
            {'y', new Color(119, 136, 153)},   // LightSlateGray
            {'z', new Color(50, 205, 50)},     // LimeGreen
        };

        public static Pixel[,] PixelArt;
        public static List<Letter> LetterList;



        public static void TextToLetters()
        {
            LetterList = new List<Letter>();
            char[] delimiterChars = { ' ', ':', ';', '\t', '-', '.', ',' };
            string text = DS.Text;
            text = text.Replace("'", "");
            string[] words = text.Split(delimiterChars);
            foreach (string word in words)
            {
                int len = word.Length;
                foreach (char c in word)
                {
                    LetterList.Add(new Letter(c, len));
                }
            }

            DS.Pixels.Clear();
        }

        public static void LettersToPixels()
        {
            foreach (Letter letter in LetterList)
            {
                Pixel px = new Pixel(ColorKey[letter.letter].R, ColorKey[letter.letter].G, ColorKey[letter.letter].B, (int)(255 / letter.wLength));
                DS.Pixels.Add(px);
            }
        }



        static void ShufflePixelArt()
        {
            int rows = PixelArt.GetLength(0);
            int cols = PixelArt.GetLength(1);
            Pixel[] array = new Pixel[rows * cols];

            // Convert 2D matrix to 1D array
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i * cols + j] = PixelArt[i, j];
                }
            }

            // Apply Fisher-Yates shuffle to the 1D array
            Random rng = new Random();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                Pixel temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            // Convert 1D array back to 2D matrix
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    PixelArt[i, j] = array[i * cols + j];
                }
            }
        }


        public static void GenerateImage()
        {
            int area = DS.Pixels.Count;

            // Calculate the side lengths of the rectangle using integer arithmetic
            int sideLength1 = (int)Math.Sqrt(area);
            double sideLength2 = Math.Ceiling((double)area / sideLength1);
            int k = 0;
            PixelArt = new Pixel[sideLength1, (int)sideLength2]; 
            for (int i = 0; i < sideLength1; i++)
            {
                for(int j = 0; j < sideLength2; j++)
                {
                    if(k < DS.Pixels.Count)
                    {
                        PixelArt[i, j] = DS.Pixels[k];
                        k++;
                    }
                    else
                    {
                        PixelArt[i, j] = new Pixel(255, 255, 255, 255);
                    }
                }
            }

            ShufflePixelArt();
        }




        static void PrintColoredSquare(int r, int g, int b)
        {
            string ANSI_RESET = "\u001b[0m"; // Reset ANSI color codes
            string ANSI_COLOR = $"\u001b[48;2;{r};{g};{b}m"; // Set background color with RGB values
            string pixel = "  "; // Each "pixel" is two characters wide for square appearance
            Console.Write(ANSI_COLOR + pixel + ANSI_RESET); // Print colored square with RGBA values
        }




        public static void PrintImage()
        {
            int k = 0;
            for (int i = 0; i < PixelArt.GetLength(0) && k < DS.Pixels.Count; i++)
            {
                for(int j = 0; j < PixelArt.GetLength(1) && k < DS.Pixels.Count; j++)
                {
                    PrintColoredSquare(PixelArt[i, j].R, PixelArt[i, j].G, PixelArt[i, j].B);
                    k++;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
