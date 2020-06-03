namespace LexicalAnalyzer.Entities
{
    public class Transition
    {
        private readonly int _startState, _endState;
        private readonly char _symbol;

        public Transition(int startState, char symbol, int endState)
        {
            _startState = startState;
            _endState = endState;
            _symbol = symbol;
        }

        public override string ToString() => $"σ({_startState}, {_symbol}) = {_endState}";

        public bool Equals(Transition other) => (this._startState == other._startState && this._symbol == other._symbol && this._endState == other._endState);

        public int σ(int? startState, char? symbol) => (startState == _startState && symbol == _symbol) ? _endState : -1;
        
        public bool isΣ(int? startState, char? symbol) => (startState == _startState && symbol == _symbol);
    }
}