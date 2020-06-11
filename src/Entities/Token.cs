namespace LexicalAnalyzer.Entities
{
    public class Token
    {
        private TokenType _tokenId;
        private string _value;
        private bool _accepted;

        public Token(int tokenId, string value)
        {
            #region Check for Tokens that could not be described in TokenTypes Enum
            switch (tokenId) {
                case -1: case -2: // Comment or string.Empty occured 
                    tokenId = 14; break;
                case 2: // Checks if a `ARTH OP`, since enums cannot take multi-values 
                    tokenId = 11; break;
                case 3: // Checks if a `REAL`, since enums cannot take multi-values 
                    tokenId = 8; break;
                case 5: case 9: // Checks if a `REL OP`, since enums cannot take multi-values 
                    tokenId = 10; break;
            }
            #endregion
            _tokenId = (TokenType) tokenId;
            _value = value;
            _accepted = (_tokenId != 0); // True if the token has an accepted final state != 0
        }

        public override string ToString()
        {
            return $@"{((_accepted) ? '\u2713' : '\u2717')} <{((_accepted) ? _tokenId+"," : string.Empty )}{(_value)}>";
        }

        public bool Accepted() => _accepted;  // same as => (_tokenId != 0) but without recalculating
    }
}