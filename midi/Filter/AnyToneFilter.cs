using System.Collections.Generic;
using System.Linq;
using music;

namespace midi.Filter
{
    internal class AnyToneFilter : IToneFilter
    {
        private readonly IReadOnlyCollection<IToneFilter> _rules;

        public AnyToneFilter(IReadOnlyCollection<IToneFilter> rules)
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