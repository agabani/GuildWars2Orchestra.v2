using music;

namespace midi.Filter
{
    internal class RangeToneFilter : IToneFilter
    {
        private readonly Tone _lowerTone;
        private readonly Tone _upperTone;

        public RangeToneFilter(Tone lowerTone, Tone upperTone)
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