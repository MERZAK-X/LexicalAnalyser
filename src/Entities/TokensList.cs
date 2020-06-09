using System.Collections.Generic;
using System.Linq;

namespace LexicalAnalyzer.Entities
{
    public class TokensList : List<Token>
    {
        public override string ToString() => this.Aggregate("", (current, token) => current + $"\n{token}");

        public bool Accepted() => this.All(token => token.Accepted());
    }
}