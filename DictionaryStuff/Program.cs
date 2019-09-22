using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DictionaryStuff
{
    class Program
    {
        private static Dictionary<string, int> words;
        static void Main(string[] args)
        {
            GetFile();
            Console.WriteLine("\nPossible Commands:\n\"find\": Find how times a word appears in the file\n\"count\": Show all word counts\"exit\": Exit the program");
            Execute();
        }
        private static void GetFile()
        {
            string path = GetUserInput();
            string text = File.ReadAllText(path);
            char[] separators = { ',', '.', '!', '?', ';', ':', ' ', '(', ')', '[', ']', '{', '}', '\r', '\n', '\t' };
            string[] wordsArr = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordsDict = new Dictionary<string, int>();
            WordCount(wordsArr, wordsDict);
            words = wordsDict;
        }
        private static void Execute()
        {
            string input;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ResetColor();
                Console.WriteLine();

                switch (input)
                {
                    case "find":
                        {
                            Console.WriteLine("What word do you want to search for?");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            string word = Console.ReadLine().ToLower();
                            Console.ResetColor();
                            Find(words, word);
                            Console.WriteLine();
                            break;
                        }
                    case "exit":
                        {
                            Console.WriteLine("Thanks for using this program! ^_^");
                            Thread.Sleep(1000);
                            break;
                        }
                    default:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Command!\n");
                            Console.ResetColor();
                            break;
                        }
                }
            } while (input != "exit");
        }
        private static string GetUserInput()
        {
            bool success = false;
            string path = "";
            while(!success)
            {
                Console.WriteLine("Please provide a file path:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                path = Console.ReadLine();
                Console.ResetColor();
                path = Path.GetFullPath(path);
                if(!File.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid path!\n");
                    Console.ResetColor();
                }
                else
                {
                    success = true;
                }
            }
            return path;
        }
        private static void WordCount(string[] wordsArr, Dictionary<string, int> wordsDict)
        {
            foreach(string word in wordsArr)
            {
                string lowerWord = word.ToLower();
                if(!wordsDict.ContainsKey(lowerWord))
                {
                    wordsDict.Add(lowerWord, 1);
                }
                else
                {
                    wordsDict[lowerWord]++;
                }
            }
        }
        private static void Find(Dictionary<string, int> words, string word)
        {
            if(words.ContainsKey(word))
            {
                Console.WriteLine("\"{0}\" was found {1} times.", word, words[word]);
            }
            else
            {
                Console.WriteLine("\"{0}\" was found 0 times.", word);
            }
        }
        private static void WriteAllResults(Dictionary<string, int> words)
        {
            foreach(KeyValuePair<string, int> kvp in words)
            {
                Console.WriteLine("Key = {0}\tValue = {1}", kvp.Key, kvp.Value);
            }
        }
    }
}
