using System.Collections.Generic;
using System.Linq;
using music;

namespace midi.Rule
{
    internal class AnyRule : IRule
    {
        private readonly IReadOnlyCollection<IRule> _rules;

        public AnyRule(IReadOnlyCollection<IRule> rules)
        {
            _rules = rules;
        }

        public bool IsAllowed(Tone tone)
        {
            return _rules.Any(rule => rule.IsAllowed(tone));
        }

        public override string ToString()
        {
            return $"{string.Join(",", _rules)}";
        }
    }
}