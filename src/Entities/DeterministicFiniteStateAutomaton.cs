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
        private string _alphabet, _name;
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
            _statesNumber = int.Parse(lines[0]);
            _alphabet = lines[1];
            _startState = int.Parse(lines[2]); // Get states from file
            var finalStatesAsString = lines[4].Split(' ');
            _finalStates = Array.ConvertAll(finalStatesAsString, int.Parse);
            for (var i = 5; i <= lines.Length -1; i++)
            {
                var state = int.Parse(lines[i].Split(' ')[0]);
                var symbol = char.Parse(lines[i].Split(' ')[1]);
                var nextState = int.Parse(lines[i].Split(' ')[2]);
                _states.AddIfNotExists(state); _states.AddIfNotExists(nextState);
                try {
                    _transitions.AddIfNotExists(new Transition(state, symbol, nextState));
                } catch (Exception) { continue; }
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
                if (i == _states.Count-1) continue;
                automaton += (i != _states.Count-1) ? ", " : string.Empty;
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
        
        public bool Accept(string word)
        {
            var letters = word.ToCharArray();
            var state = this._startState;
            foreach (var letter in letters)
                try{
                    state = _transitions[state, letter];
                }catch (Exception) {continue;}
            return ((IList) this._finalStates).Contains(state);
        }
        
        /*private int Σ(int state, char alpha)
        {
            if (!_transitionTable.TryGetValue(state, out var transition)) return 0;
            if (!transition.ContainsKey(alpha)) return 0;
            var nextState = _transitionTable[state][alpha];
            return nextState;
        }*/

        #endregion
        
    }
}