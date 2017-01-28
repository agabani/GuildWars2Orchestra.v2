using music;

namespace midi.Filter
{
    internal class SingleToneFilter : IToneFilter
    {
        private readonly Tone _tone;

        public SingleToneFilter(Tone tone)
        {
            _tone = tone;
        }

        public bool IsAllowed(Tone tone) => Equals(tone, _tone);

        public override string ToString()
        {
            return $"{_tone.ToString()}";
        }
    }
}