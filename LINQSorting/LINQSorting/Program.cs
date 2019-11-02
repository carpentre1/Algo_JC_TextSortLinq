using System;
using System.Linq;

namespace LINQDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DictionarySorter.ReadLines();
            //DictionarySorter.ReverseLines();
            DictionarySorter.StartsWithLetter("z");
            Console.Read();
        }
    }

    public static class DictionarySorter
    {
        static string filePath = @"C:\Dictionary\words_alpha.txt";
        public static string[] lines;
        public static string[] reversedLines;

        public static void ReadLines()
        {
            lines = System.IO.File.ReadAllLines(filePath);
        }
        public static void ReverseLines()
        {
            reversedLines = lines.Reverse().ToArray();
            foreach(string line in reversedLines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("Done.");
        }

        public static void StartsWithLetter(string letter)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Words starting with " + letter + ":");
            var queryResults =
                from l in lines
                where l.StartsWith(letter)
                select l;
            foreach(var item in queryResults)
            {
                Console.WriteLine(item);
            }
            watch.Stop();
            Console.WriteLine("Finding all words that start with " + letter + " took " + watch.ElapsedMilliseconds + " milliseconds.");
        }
    }
}
