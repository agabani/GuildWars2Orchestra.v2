using music;

namespace midi.Rule
{
    internal class SingleRule : IRule
    {
        private readonly Tone _tone;

        public SingleRule(Tone tone)
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