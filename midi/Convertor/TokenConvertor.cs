using System;
using music;
using NAudio.Midi;

namespace midi.Convertor
{
    internal class TokenConvertor
    {
        internal static Token Convert(NoteOnEvent @event, double tempo, int deltaTicksPerQuarterNote, double speed = 1.0)
        {
            return new Token
            {
                Length = LengthConvertor.Convert(@event, tempo, deltaTicksPerQuarterNote),
                Tone = ToneConvertor.Convert(@event),
                AbsoluteTime = TimeSpan.FromMilliseconds(ToMilliseconds(@event.AbsoluteTime, tempo, deltaTicksPerQuarterNote) / speed)
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