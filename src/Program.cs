using System;
using System.IO;
using System.Linq;
using LexicalAnalyser.Entities;

namespace LexicalAnalyser
{
    internal static class Program
    {
        #region Help message
        private const string Help = 
@"LexicalAnalyser v1.3: https://github.com/MERZAK-X/LexicalAnalyser

Usage: dotnet LexicalAnalyser.dll [[Automaton] [Sourcecode]] [-v] [--help]

    Arguments:
        Automaton       Path to the Automaton's file
        Source          Path to the source code file to be analysed
    Options:
        -v, -vv         Verbose level, 1 or 2 respectively, if not set 0
        --help          Display this help and exit
        
Examples:
    ./LexicalAnalyser SimpleLanguageAutomaton.test SourceCode.test -v
    dotnet LexicalAnalyser.dll lib/examples/SimpleLanguageAutomaton.test lib/examples/SourceCode.test

Documentation: https://git.io/JfNf4

Copyright (C) 2020 ""NUL-X""";
        #endregion

        private static int Main(string[] args)
        {
            try
            {
                if (args.Length >= 2 && !args.Contains("--help"))
                { 
                    // Set the verbose level
                    var verbose = (args.Contains("-v")) ? 1 : (args.Contains("-vv")) ? 2 : 0;
                    // Create new Deterministic Finite State Automaton from given file
                    var automaton = DFSA.CreateInstance(args[0] ?? Console.ReadLine(), verbose);
                    // Display Automaton
                    automaton.Print();
                    // Analyse the given source code & display the results
                    var accepted = automaton.Analyse(File.ReadAllText(args[1] ?? Console.ReadLine()));
                    Console.WriteLine($@"{'\n'}{((accepted) ? '\u2713' : '\u2717')} The source file `{Path.GetFileName(args[1])}` is {((accepted) ? string.Empty : "NOT " )}accepted by the automaton's described language !");
                    return (accepted) ? 0 : 1;
                }
                Console.WriteLine(Help);
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred : {e.Message}\n"); // File not found or error occurred
                return 1;
            }
        }
    }
}