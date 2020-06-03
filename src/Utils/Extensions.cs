using System.Collections.Generic;
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
    }
}