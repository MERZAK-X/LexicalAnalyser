using System;
using System.Collections.Generic;
using System.Linq;
using LexicalAnalyzer.Entities;

namespace LexicalAnalyzer.Utils
{
    public static class Extension
    {
        public static void AddIfNotExists(this TransitionsMap transitionsMap, Transition transition)
        {
            if (!transitionsMap.Exists(transition.Equals))
                transitionsMap.Add(transition);
        }
        
        public static bool Has(this TransitionsMap transitionsMap, Transition transition) => transitionsMap.Exists(transition.Equals);
        
        public static void AddIfNotExists<T>(this List<T> list, T item)
        {
            if (!list.Contains(item)) list.Add(item);
        }

        public static TokenType GetToken(this TokenType tokenType, int tokenId)
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
            return (TokenType) tokenId;
        }
        
        public static IEnumerable<string> SplitKeep(this string word, string separator)
        {
            var index = word.IndexOf(separator, StringComparison.Ordinal);
            // Separate the OP from ID/NUMBER using ' '
            word = string.Concat(word.Select(c => c == separator[0] ? $" {separator} " : c.ToString())).TrimStart(' ');
            //word = word.Insert(index + separator.Length," "); word = word.Insert(index, " ");
            // Return each subword / OP
            foreach (var subword in word.Split(' '))
                yield return subword;
        }
    }
}