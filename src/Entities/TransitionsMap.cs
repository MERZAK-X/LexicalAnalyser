using System.Collections.Generic;
using System.Linq;

namespace LexicalAnalyzer.Entities
{
    public class TransitionsMap : List<Transition>
    {
        public override string ToString() => this.Aggregate("", (current, transition) => current + $" [ {transition} ] ");

        public int this[int i, char c] => Find(transition => transition.isΣ(i,c))?.σ(i,c) ?? -1; // Returns the final state of the transition
    }
}