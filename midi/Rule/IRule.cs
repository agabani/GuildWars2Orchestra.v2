using music;

namespace midi.Rule
{
    internal interface IRule
    {
        bool IsAllowed(Tone tone);
    }
}