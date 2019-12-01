using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            NodeRoot tree = new NodeRoot();



            Console.WriteLine("Hello World!");
            //read in the dictionary
            DictionarySorter.ReadLines();
            //reverse alphabetize the dictionary
            //DictionarySorter.ReverseLines();
            //write to the dictionary folder with the lists of each item that starts with...
            DictionarySorter.StartsWithLetters("Z");
            DictionarySorter.StartsWithLetters("He");
            DictionarySorter.HasLetterAtPosition('e', 2);
            Console.Read();
        }
    }

    class Node
    {
        string _word;
        Dictionary<char, Node> _dict;

        public Node Add(char value)
        {
            if(this._dict == null)
            {
                this._dict = new Dictionary<char, Node>();
            }
            Node result;
            if(this._dict.TryGetValue(value, out result))
            {
                return result;
            }

            result = new Node();
            this._dict[value] = result;
            return result;
        }

        public Node Get(char value)
        {
            if(this._dict == null)
            {
                return null;
            }
            Node result;
            if(this._dict.TryGetValue(value, out result))
            {
                return result;
            }
            return null;
        }

        public void SetWord(string word)
        {
            this._word = word;
        }
        public string GetWord()
        {
            return this._word;
        }
    }

    class NodeRoot
    {
        Node _root = new Node();

        public void AddWord(string value)
        {
            Node current = this._root;
            for(int i = 0; i < value.Length; i++)
            {
                current = current.Add(value[i]);
            }
            current.SetWord(value);
        }
        public bool ContainsWord(string value)
        {
            Node current = this._root;
            for(int i=0; i < value.Length; i++)
            {
                current = current.Get(value[i]);
                if(current == null)
                {
                    return false;
                }
            }
            return current != null && current.GetWord() != null;
        }
    }

    public static class DictionarySorter
    {
        //where the dictionary is located. this Dictionary folder will need to be created, and words_alpha.txt placed into it
        static string filePath = @"C:\Dictionary\words_alpha.txt";
        //where to write the lists to
        static string writtenFilePath = @"C:\Dictionary\";
        public static string[] lines;
        public static string[] reversedLines;

        public static void ReadLines()
        {
            lines = System.IO.File.ReadAllLines(filePath);
            NodeRoot tree = new NodeRoot();
            foreach(string line in lines)
            {
                tree.AddWord(line);
                Console.WriteLine("Added " + line + " to tree.");
            }

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

        public static void HasLetterAtPosition(char letter, int position)
        {
            //Console.WriteLine("Finding words with " + letter + " at position " + position);
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var queryResults =
                from l in lines
                where l.Length >= 2
                where l.ElementAt(1).Equals(letter)
                select l;
            foreach (var item in queryResults)
            {
                //Console.WriteLine(item);
            }
            System.IO.File.WriteAllLines(writtenFilePath + "letter_at_position_" + letter + "_" + position + ".txt", queryResults);
            watch.Stop();
            Console.WriteLine("Finding all words with " + letter + " at position " + position + " took " + watch.ElapsedMilliseconds + " milliseconds.");
        }

        public static void StartsWithLetters(string letter)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //Console.WriteLine("Words starting with " + letter + ":");
            var queryResults =
                from l in lines
                where l.StartsWith(letter, StringComparison.CurrentCultureIgnoreCase)
                select l;
            foreach(var item in queryResults)
            {
                //Console.WriteLine(item);
            }
            System.IO.File.WriteAllLines(writtenFilePath + "starts_with_letters_" + letter + ".txt", queryResults);
            watch.Stop();
            Console.WriteLine("Finding all words that start with " + letter + " took " + watch.ElapsedMilliseconds + " milliseconds.");
        }
    }
}
