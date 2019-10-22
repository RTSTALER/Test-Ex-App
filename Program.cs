using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TestExApp
{
    class Program
    {
        static int MaxBigKey = 0;
        static List<char> constituentsChar = new List<char>();
        public static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            Stopwatch SearchWatch = new Stopwatch();
            stopWatch.Start();
            Dictionary<string, string> bigDictonary = GetDictonary(@"Dictonary.txt");
            string[] titles = GetTitles(@"titles2.txt");
            Trie<string> trie = GetTree(bigDictonary);
            SearchWatch.Start();
            string[] result = Translate(trie, titles);
            SearchWatch.Stop();
            Console.WriteLine("Complete! Check result string[] on Debug mode ;)");
            stopWatch.Stop();
            Console.WriteLine("RunTime " + stopWatch.ElapsedMilliseconds + " ms");
            Console.WriteLine("TranslateTime " + SearchWatch.ElapsedMilliseconds + " ms");
            Console.ReadKey();
        }

        static string[] Translate(Trie<string> tree, string[] titles)
        {
            string[] result = new string[titles.Length];
            for (int i = 0; i < titles.Length; i++)
            {
                string key = "";
                var words = "";
                var tempTitile = titles[i];
                var unknownSymbols = "" ;
                
                foreach (var c in tempTitile)
                    if (!constituentsChar.Contains(c))
                        unknownSymbols += c;
                foreach (var s in unknownSymbols)
                   tempTitile = tempTitile.Replace(s.ToString(), s+" ");

                var lines = tempTitile.Split(' ');
                foreach(var line in lines)
                {
                    int offset = 0;
                    for (int c = 0; c < line.Length; c++)
                    {
                        key += line[c];
                        var s = tree.GetByPrefix(key).Select(e => e.Value).ToArray();
                        if (s.Length != 1)
                            continue;
                        else
                        {
                            offset += key.Length;
                            words += s[0];
                            key = "";
                        }
                            
                    }
                    if (offset != 0)
                        words += line.Substring(0, offset);
                }
                result[i] += words;
            }
            return result;
        }
        static Trie<string> GetTree(Dictionary<string, string> _dictonary)
        {
            var trie = new Trie<string>();
            foreach (var k in _dictonary)
                trie.Add(k.Key, k.Value);
            return trie;
        }
        static string[] GetTitles(string directory)
        {
            try
            {
                return File.ReadAllLines(directory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static Dictionary<string, string> GetDictonary(string directory)
        {
            try
            {
                Dictionary<string, string> resultDictonary = new Dictionary<string, string>();
                StreamReader reader = new StreamReader(directory);
                while (!reader.EndOfStream)
                {
                    string[] lines = reader.ReadLine().Split(';');
                    if (lines[0].Length > MaxBigKey)
                        MaxBigKey = lines[0].Length;
                    foreach (var c in lines[0])
                        if (!constituentsChar.Contains(c))
                            constituentsChar.Add(c);
                    resultDictonary.Add(lines[0], lines[1]);
                }
                return resultDictonary;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

    }
}





