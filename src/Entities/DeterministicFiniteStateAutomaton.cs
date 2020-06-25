using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LexicalAnalyser.Utils;

namespace LexicalAnalyser.Entities
{
    public class DFSA : Automaton
    {
        #region Variables

        private int _startState, _statesNumber, _verbose = 0;
        private int[] _finalStates;
        private List<int> _states = new List<int>();
        private List<string> _keywords;
        private readonly string _alphabet, _name;
        private readonly string[] _operators, _commentDelimiter;
        private string _string = string.Empty; 
        private bool _isText, _comment; // Checks whether the input is a string / comment
        private TransitionsMap _transitions = new TransitionsMap();
        private TokensList _tokens = new TokensList();

        #endregion

        #region Contructor & Factory

        public static DFSA CreateInstance(string filename, int verbose = 0)
        {
            var automaton = new DFSA(filename, verbose);
            return automaton;
        }

        private DFSA(string filename, int verbose)
        {
            var lines = File.ReadAllLines(filename);
            var l = 0;

            #region Automaton info
            _name = Path.GetFileNameWithoutExtension(filename);
            _statesNumber = int.Parse(lines[l]); // Number of states
            _verbose = verbose; // Display verbose level 
            #endregion

            #region Alphabet
            _alphabet = lines[++l];
            #endregion

            #region Start State
            _startState = int.Parse(lines[++l]); // Get states from file
            #endregion

            #region Final States
            var finalStatesAsString = lines[++l].Split(' ');
            _finalStates = Array.ConvertAll(finalStatesAsString, int.Parse);
            #endregion

            #region Operators
            _operators = lines[++l].Split(' ').ToArray();
            #endregion

            #region Keywords
            _keywords = lines[++l].Split(' ').ToList();
            _keywords = _keywords.ConvertAll(keyword => keyword.ToLower());
            #endregion

            #region Comment Delimiter
            _commentDelimiter = lines[++l].Split(' ').ToArray();
            #endregion

            #region Transitions

            for (var i = ++l; i <= lines.Length - 1; i++)
            {
                if(lines[i].StartsWith('#')) continue; // Adds #3
                var state = int.Parse(lines[i].Split(' ')[0]);
                var symbol = char.Parse(lines[i].Split(' ')[1]);
                var nextState = int.Parse(lines[i].Split(' ')[2]);
                _states.AddIfNotExists(state);
                _states.AddIfNotExists(nextState);
                try { _transitions.AddIfNotExists(new Transition(state, symbol, nextState)); }
                catch (Exception) { /* continue; */ }
            }

            #endregion
            
        }

        #endregion

        #region Functions & Methods
        public void Print()
        {
            Console.WriteLine(this);
        }

        public bool Analyse(string sourceCode)
        {
            #region Variables
            char[] separators = {' ', '\n', '\t', ';'};
            var words = sourceCode.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Token token;
            _tokens?.Clear(); // Clear old results if any
            #endregion

            #region Display the input source code
            if (_verbose > 0) 
                Console.WriteLine($"Source code : \n\n```\n{sourceCode}\n```\n");
            #endregion

            #region Analyse each lexeme of the source code

            foreach (var word in words)
            {
                var tokenId = Accepts(word);

                #region Check if rejected word contains OPERATORS

                if (tokenId == 0 && word.Contains(_operators) && word.Split(_operators).All(Accept))
                {
                    #region Split Tokens using Automaton's Token Spliter

                    foreach (var subword in SplitToken(word))
                    {
                        tokenId = Accepts(subword);
                
                        #region Check for Tokens that should be ignored or aggregated
                        switch (tokenId) {
                            case -1: case -2: // Comment or string.Empty occurred 
                                continue;
                            case 13: // Checks for string and aggregates it  
                                _string += subword.Replace('"', '\0')+' ';
                                if (_isText) continue;
                                _string = _string.Substring(0, _string.Length - 1); // Removes the extra space after building the _string
                                break;
                        }
                        #endregion

                        token = new Token(tokenId, ((tokenId==13) ? _string : subword));
                        _tokens.Add(token);
                    }

                    #endregion
                    continue; // Tokens added, skip to next iteration to avoid err insertion in _token
                }

                #endregion
                
                #region Check for Tokens that should be ignored or aggregated
                switch (tokenId) {
                    case -1: case -2: // Comment or string.Empty occurred 
                        continue;
                    case 13: // Checks for string and aggregates it  
                        _string += word.Replace('"', '\0')+' ';
                        if (_isText) continue;
                        _string = _string.Substring(0, _string.Length - 1); // Removes the extra space after building the _string
                        break;
                }
                #endregion

                token = new Token(tokenId, ((tokenId==13) ? _string : word));
                _tokens.Add(token);
            }

            #endregion

            #region Display the analysed Tokens
            Console.WriteLine($@"{_tokens}");
            #endregion
            
            return _tokens?.Accepted() ?? false; // Avoid NullReferenceException
        }

        private int Accepts(string word)
        {
            #region Variables

            var letters = (word.ToLower()).ToCharArray();
            var state = _startState;

            #endregion

            #region Check for empty word string.Empty

            if (word == string.Empty) return -2; // Check for empty word '' caused by spaces in source code

            #endregion

            #region Check for comments

            if (!_comment)
                _comment = word.StartsWith(_commentDelimiter[0]);

            if (_comment)
            {
                _comment = !word.EndsWith(_commentDelimiter[1]);
                return -1;
            }

            #endregion

            #region Check for strings

            if ((word.StartsWith('"') && word.EndsWith('"')) && word != "\""){ // Cases where string is declared as "string" 
                _string = string.Empty; // Fixes #9
                return 13;
            }

            if (word.Contains('"')){ // Sets _isText flag to on/off when facing a double quote
                _isText = !_isText;
                if (_isText) _string = string.Empty; // Fixes #9
                return 13;
            }
            
            if (_isText)
                return 13;
            
            #endregion

            #region Check for Keywords
            if (_keywords.Any(x=> x == word.ToLower())) return 12; // Checks whether the word is a keyword from the _keywords array
            #endregion

            #region Calculate the Final State

            foreach (var letter in letters)
            {
                state = _transitions[state, letter]; /* σ(state, letter) returns next state */
                if (state == -1) return 0;
            }
            #endregion

            return ((IList) _finalStates).Contains(state) ? state : 0; // Return the final state instead of a bool ; 0 = no states found
        }

        public bool Accept(string word)
        {
            var state = _startState;

            foreach (var letter in word.ToLower())
            {
                state = _transitions[state, letter]; /* σ(state, letter) returns next state */
                if (state == -1) return false;
            }

            return ((IList) _finalStates).Contains(state); // Returns whether a word is accepted by the automaton or not
        }

        public IEnumerable<string> SplitToken(string word)
        {
            #region Variables
            var state = _startState; var invalidStates = new[]{0, 13, 14};
            var subwords = new List<string>(); var subword = string.Empty;
            #endregion

            #region Split Tokens depending on their final states
            for (var index = 0; index < word.Length; index++)
            {
                #region Check for valid σ(state, letter) Transition, if not found add current subword & restart OP on current symbol
                state = _transitions[state, char.ToLower(word[index])]; // σ(state, letter) obviously
                if(state == -1) {
                    state = _startState; // Reset to the initial state
                    index--; // Restart operating from the current char
                    subwords.Add(subword); // Add the saved subword to the list
                    subword = string.Empty; // Clear the subword builder for next use
                    continue; // Get back to last char as start of word 
                }
                #endregion
                // If has a next state, add current letter to subword
                if (!invalidStates.Contains(state)) subword += word[index]; 
                // Reached the end of the word array add last left letters
                if(index == word.Length-1) subwords.Add(subword); 
            }
            #endregion

            return subwords.ToArray();
        }
        
        public override string ToString()
        {
            #region Variables

            var automaton = $"Automaton [{_name}] : ";

            #endregion

            #region States {E}

            automaton += "\n\nE = {";
            for (var i = 0; i < _states.Count; i++)
            {
                automaton += _states[i];
                if (i == _states.Count - 1) continue;
                automaton += (i != _states.Count - 1) ? ", " : string.Empty;
            }

            automaton += "} ; \n";

            #endregion

            #region Alphabet {A}

            automaton += "\nA = {";
            automaton = _alphabet.Aggregate(automaton, (current, c) => current + ((c == _alphabet.Last()) ? $"{c}" : $"{c}, "));

            #endregion

            #region Transitions {σ}

            if(_verbose > 1)
                automaton += $"}} ; \n\nTransitions: {{{_transitions}\n";

            #endregion

            #region Initial States {q₀}

            automaton += $"}} ; \n\nq₀ = {_startState} ; \n";

            #endregion

            #region Final States {F}
            
            automaton += "\nF = {";
            automaton = _finalStates.Aggregate(automaton, (current, finalState) => current + ((finalState == _finalStates.Last()) ? $"{finalState}" : $"{finalState}, "));

            automaton += "} ;\n";
            return automaton;

            #endregion
        }
        #endregion
    }
}