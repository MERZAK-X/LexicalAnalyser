using System;
using System.IO;
using LexicalAnalyzer.Entities;

namespace LexicalAnalyzer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var automaton = DFSA.CreateInstance(args[0]);
                automaton.Print();
                var accepted = automaton.Analyse(File.ReadAllText(args[1]));
                Console.WriteLine($@"{'\n'}{((accepted) ? '\u2713' : '\u2717')} The source file `{Path.GetFileName(args[1])}` is {((accepted) ? String.Empty : "NOT " )}accepted by the automaton's described language !");
            }
            catch (Exception e)
            {
                // File not found or error occured
                Console.WriteLine(e.Message + '\n' + e.StackTrace);
            }
        }
    }
}