using music;

namespace midi.Rule
{
    public interface IRule
    {
        bool IsAllowed(Tone tone);
    }
}