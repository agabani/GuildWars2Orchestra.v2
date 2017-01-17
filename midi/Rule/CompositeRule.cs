using System.Collections.Generic;
using System.Linq;
using music;

namespace midi.Rule
{
    internal class CompositeRule : IRule
    {
        private readonly IReadOnlyCollection<IRule> _rules;

        public CompositeRule(IReadOnlyCollection<IRule> rules)
        {
            _rules = rules;
        }

        public bool IsAllowed(Tone tone)
        {
            return _rules.Any(rule => rule.IsAllowed(tone));
        }
    }
}