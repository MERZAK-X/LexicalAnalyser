using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LexicalAnalyzer.Utils;

namespace LexicalAnalyzer.Entities
{
    public class DFSA : Automaton
    {
        #region Variables

        private int _startState, _statesNumber;
        private int[] _finalStates;
        private List<int> _states = new List<int>();
        private List<string> _keywords;
        private string _alphabet, _name;
        private string[] _commentDelimiter;
        private string _string = string.Empty; 
        private bool _isText = false, _comment = false; // Checks whether the input is a string / comment
        private TransitionsMap _transitions = new TransitionsMap();

        #endregion

        #region Contructor & Factory

        public static DFSA CreateInstance(string filename)
        {
            var automaton = new DFSA(filename);
            return automaton;
        }

        private DFSA(string filename)
        {
            var lines = File.ReadAllLines(filename);
            _name = Path.GetFileNameWithoutExtension(filename);
            _statesNumber = int.Parse(lines[0]); // Number of states
            _alphabet = lines[1];
            _startState = int.Parse(lines[2]); // Get states from file
            var finalStatesAsString = lines[4].Split(' ');
            _finalStates = Array.ConvertAll(finalStatesAsString, int.Parse);
            _keywords = lines[5].Split(' ').ToList();
            _keywords = _keywords.ConvertAll(keyword => keyword.ToLower());
            _commentDelimiter = lines[6].Split(' ').ToArray();
            for (var i = 7; i <= lines.Length - 1; i++)
            {
                if(lines[i].StartsWith('#')) continue; // Adds #3
                var state = int.Parse(lines[i].Split(' ')[0]);
                var symbol = char.Parse(lines[i].Split(' ')[1]);
                var nextState = int.Parse(lines[i].Split(' ')[2]);
                _states.AddIfNotExists(state);
                _states.AddIfNotExists(nextState);
                try { _transitions.AddIfNotExists(new Transition(state, symbol, nextState)); }
                    catch (Exception) { continue; }
            }
        }

        #endregion

        #region Functions & Methods

        public override string ToString()
        {
            #region Variables

            var automaton = $"Automaton [{_name}] : ";

            #endregion

            #region States {E}

            automaton += "E = {";
            for (var i = 0; i < _states.Count; i++)
            {
                automaton += _states[i];
                if (i == _states.Count - 1) continue;
                automaton += (i != _states.Count - 1) ? ", " : string.Empty;
            }

            automaton += "} ; ";

            #endregion

            #region Alphabet {A}

            var iteration = 1;
            automaton += "A = {";
            foreach (var c in _alphabet)
            {
                automaton += c;
                if (iteration >= _alphabet.Length) continue;
                automaton += ", ";
                iteration++;
            }

            #endregion

            #region Transitions {σ}

            automaton += $"}} ; Transitions: {{{_transitions}}}";

            #endregion

            #region Initial States {q₀}

            automaton += $" ; q₀ = {this._startState} ; ";

            #endregion

            #region Final States {F}

            iteration = 1;
            automaton += "F = {";
            foreach (var finalState in _finalStates)
            {
                automaton += finalState;
                if (iteration >= _finalStates.Length) continue;
                automaton += ", ";
                iteration++;
            }

            automaton += "}\n";
            return automaton;

            #endregion
        }

        public void Print()
        {
            Console.WriteLine(this);
        }

        public bool Analyse(string sourceCode)
        {
            #region Variables
            var accepted = true; var currentAccepted = false;
            char [] separators = {' ', '\n', '\t', ';'};
            var tokenId = 0; TokensMap token;
            #endregion
            
            foreach (var word in sourceCode.Split(separators))
            {
                tokenId = Accepts(word); // Get the final state; 0 = none = word not accepted

                #region Check for Tokens that could not be described in TockensMap Enum
                switch (tokenId) {
                    case -1: case -2: // Comment occured
                        continue;
                    case 2: // Checks if a `ARTH OP`, since enums cannot take multi-values
                        tokenId = 11; break;
                    case 3: // Checks if a `REAL`, since enums cannot take multi-values
                        tokenId = 8; break;
                    case 5: case 9: // Checks if a `REL OP`, since enums cannot take multi-values
                        tokenId = 10; break;
                    case 13: // Checks for string and aggregates it 
                        _string += ' '+word+' '; break;
                }
                #endregion
                
                currentAccepted = (tokenId != 0); // if a final state was found
                token = (TokensMap) tokenId; // Get token by id from the map
                accepted = accepted && currentAccepted; // If the previous words and current one are accepted -> for the last return statement
                Console.WriteLine($@"{((currentAccepted) ? '\u2713' : '\u2717')} <{word}{((token != 0) ? ", "+token : string.Empty )}>");
            }
            
            return accepted;
        }

        public int Accepts(string word)
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
            
            if (_comment){
                _comment = !word.EndsWith(_commentDelimiter[1]);
                return -1;
            }
            #endregion

            #region Check for strings
            if (!_isText)
                _isText = word.StartsWith('"');
            
            if (_isText){
                _isText = !word.EndsWith('"');
                return 13;
            }
            #endregion

            #region Check for Keywords
            if (_keywords.Any(x=> x == word)) return 12; // Checks whether the word is a keyword from the _keywords array
            #endregion

            #region Calculate the Final State

            foreach (var letter in letters)
                try{ state = _transitions[state, letter]; /*Console.WriteLine(letter); // Uncomment for debug */} 
                catch (NullReferenceException) { return 0; }
                catch (Exception) { return 0; }

            #endregion

            return ((IList) this._finalStates).Contains(state) ? state : 0; // Return the final state instead of a bool ; 0 = no states found
        }

        public bool Accept(string word)
        {
            var letters = word.ToCharArray();
            var state = _startState;

            foreach (var letter in letters)
                try{ state = _transitions[state, char.ToLower(letter)]; } 
                    catch (Exception) { return false; }

            return ((IList) this._finalStates).Contains(state);
        }
        #endregion
    }
}