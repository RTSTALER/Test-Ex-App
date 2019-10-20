using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace TestExApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Dictionary<string, string> bigDictonary = GetDictonary(@"Dictonary.txt");
            string[] titles = GetTitles(@"titles2.txt");
            Trie<string> trie = GetTree(bigDictonary);
            string[] result = Translate(trie, titles);
            Console.WriteLine("Complete! Check result string[] on Debug mode ;)");
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.ReadKey();
        }
        
        static string[] Translate(Trie<string> tree, string[] titles)
        {
            string[] result = new string[titles.Length];
            for(int i = 0; i< titles.Length; i++)
            {
                string words = "";
                string key = "";
                string title = titles[i];
                key = title;
                for (int c = 0; c< titles[i].Length; c++)
                {
                    var s = tree.GetByPrefix(key).Select(e => e.Value).ToArray();
                    if (s.Length == 0)
                        key = key.Remove(key.Length - 1);
                    else
                    {
                        words += s[0];
                        c = 0;
                        title = title.Substring(key.Length);
                        key = title;
                        if (key == "")
                            break;
                    }                       
                }
                result[i] = words;
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
            catch(Exception e)
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
                    /*  char[] line = reader.ReadLine().ToCharArray();
                     foreach (var c in line)
                         if (Regex.IsMatch(c.ToString(), @"\A[\s\,\.\!\?\p{IsCyrillic}\p{IsBasicLatin}]*\z", RegexOptions.IgnoreCase | RegexOptions.Multiline))
                         {
                             value += c;
                         }
                         else
                         {
                             key += c;
                         }
                     resultDictonary.Add(key, value);*/

                    string[] lines = reader.ReadLine().Split(';');
                    resultDictonary.Add(lines[0], lines[1]);
                }
                return resultDictonary;
            }
           catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

    }
}





