using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ConsoleFunctions
{
    public static class StringToAscii
    {
        const string charListFileName = "AsciiArt.txt";
        const string charMap = @"ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ.,!? 0123456789";
        const int linesPerChar = 6;

        /// <summary>
        /// Converts string to string array containing each line of the Ascii art.
        /// </summary>
        /// <param name="s">String to convert.</param>
        public static string[] GetAsciiStrings(string s)
        {
            s = s.ToUpper();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ConsoleFunctions.AsciiArt.txt");
            string[] charList = ReadAllLinesFromStream(stream);

            StringBuilder[] asciiWordBuilder = new StringBuilder[linesPerChar];

            for (int i = 0; i < asciiWordBuilder.Length; i++)
            {
                asciiWordBuilder[i] = new StringBuilder();
            }

            for (int i = 0; i < s.Length; i++)
            {
                int charIndex = charMap.IndexOf(s[i]);
                if (charIndex != -1)
                {
                    for (int j = 0; j < linesPerChar; j++)
                    {
                        asciiWordBuilder[j].Append(charList[(charIndex * linesPerChar) + j]);
                    }
                }
            }

            string[] asciiWord = new string[linesPerChar];
            for (int i = 0; i < linesPerChar; i++)
            {
                asciiWord[i] = asciiWordBuilder[i].ToString();
            }

            return asciiWord;
        }

        /// <summary>
        /// Print string as Ascii art.
        /// </summary>
        /// <param name="s">String to print.</param>
        public static void Print(string s)
        {
            string[] stringsToPrint = GetAsciiStrings(s);

            for (int i = 0; i < stringsToPrint.Length; i++)
            {
                Console.WriteLine(stringsToPrint[i]);
            }
        }

        /// <summary>
        /// Print string as Ascii art centered. Prints nothing if ascii art become longer than console scree width.
        /// </summary>
        /// <param name="s">String to print.</param>
        public static void PrintCentered(string s)
        {
            string[] stringsToPrint = GetAsciiStrings(s);

            if (stringsToPrint[0].Length <= Console.WindowWidth)
            {
                StringBuilder paddingBuilder = new StringBuilder("");

                for (int i = 0; i < ((Console.WindowWidth - stringsToPrint[0].Length) / 2); i++)
                {
                    paddingBuilder.Append(' ');
                }

                string padding = paddingBuilder.ToString();

                for (int i = 0; i < stringsToPrint.Length; i++)
                {
                    Console.Write(padding);
                    Console.WriteLine(stringsToPrint[i]);
                }
            }
        }


        /// <summary>
        /// Print string as Ascii art horizontally.
        /// </summary>
        /// <param name="s">String to print.</param>
        public static void PrintVertical(string s)
        {


            s = s.ToUpper();

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ConsoleFunctions.AsciiArt.txt");
            string[] charList = ReadAllLinesFromStream(stream);

            for (int i = 0; i < s.Length; i++)
            {
                int charIndex;
                if ((charIndex = charMap.IndexOf(s[i])) != -1)
                {
                    for (int j = 0; j < linesPerChar; j++)
                    {
                        Console.WriteLine(charList[(charIndex * linesPerChar) + j]);
                    }
                }
            }
        }

        private static string[] ReadAllLinesFromStream(Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream);
            string[] buffer = new string[linesPerChar * charMap.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = streamReader.ReadLine();
            }
            return buffer;
        }

    }
}
