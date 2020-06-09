namespace LexicalAnalyzer.Entities
{
    public class Token
    {
        private TokenType _tokenId;
        private string _value;
        private bool _accepted;

        public Token(int tokenId, string value)
        {
            _tokenId = (TokenType) tokenId;
            _value = value;
            _accepted = (_tokenId != 0); // True if the token has an accepted final state != 0
        }

        public override string ToString()
        {
            return $@"{((_accepted) ? '\u2713' : '\u2717')} <{(_value)}{((_accepted) ? ","+_tokenId : string.Empty )}>";
        }

        public bool Accepted() => _accepted;  // same as => (_tokenId != 0) but without recalculating
    }
}