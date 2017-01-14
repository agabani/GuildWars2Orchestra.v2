using music;
using NAudio.Midi;

namespace midi
{
    internal class LengthConvertor
    {
        internal static Length Convert(NoteOnEvent @event, double tempo, int deltaTicksPerQuarterNote)
        {
            return new Length
            {
                Fraction = Fraction(ToMilliseconds(@event.NoteLength, tempo, deltaTicksPerQuarterNote), tempo)
            };
        }

        private static double ToMilliseconds(long tick, double tempo, double deltaTicksPerQuarterNote)
        {
            return tick*60000/deltaTicksPerQuarterNote/tempo;
        }

        private static Fraction Fraction(double milliseconds, double tempo)
        {
            return new Fraction(milliseconds/1000.0*tempo/60/4, 1);
        }
    }
}