using System;
using System.IO;
using System.Linq;
using LexicalAnalyzer.Entities;

namespace LexicalAnalyzer
{
    internal static class Program
    {
        #region Help message
        private const string Help = 
@"LexicalAnalyser: https://github.com/MERZAK-X/LexicalAnalyser

Usage: dotnet LexicalAnalyser.dll [[Automaton] [Sourcecode]] [--help]

    Arguments:
        Automaton       Path to the Automaton's file
        Source          Path to the source code file to be analysed
    Options:
        --help          Display this help and exit
        
Examples:
    dotnet LexicalAnalyser.dll SimpleLanguageAutomaton.test SourceCode.test
    LexicalAnalyser.exe lib/examples/SimpleLanguageAutomaton.test lib/examples/SourceCode.test

Copyright (C) 2020 ""NUL-X""";
        #endregion

        private static int Main(string[] args)
        {
            try
            {
                if (args.Length == 2 && !args.Contains("--help"))
                {
                    var automaton = DFSA.CreateInstance(args[0] ?? Console.ReadLine()); // Create new Deterministic Finite State Automaton from given file
                    automaton.Print(); // Display Automaton
                    var accepted = automaton.Analyse(File.ReadAllText(args[1] ?? Console.ReadLine()));
                    Console.WriteLine($@"{'\n'}{((accepted) ? '\u2713' : '\u2717')} The source file `{Path.GetFileName(args[1])}` is {((accepted) ? string.Empty : "NOT " )}accepted by the automaton's described language !");
                    return (accepted) ? 0 : 1;
                }
                Console.WriteLine(Help);
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured :{e.Message}\n"); // File not found or error occured
                return 1;
            }
        }
    }
}