using music;

namespace midi.Filter
{
    public interface IToneFilter
    {
        bool IsAllowed(Tone tone);
    }

    public class TrueToneFilter : IToneFilter
    {
        public bool IsAllowed(Tone tone)
        {
            return true;
        }
    }
}