using music;

namespace midi.Rule
{
    internal class RangeRule : IRule
    {
        private readonly Tone _lowerTone;
        private readonly Tone _upperTone;

        public RangeRule(Tone lowerTone, Tone upperTone)
        {
            _lowerTone = lowerTone;
            _upperTone = upperTone;
        }

        public bool IsAllowed(Tone tone) => tone >= _lowerTone && tone <= _upperTone;

        public override string ToString()
        {
            return $"{_lowerTone}-{_upperTone}";
        }
    }
}