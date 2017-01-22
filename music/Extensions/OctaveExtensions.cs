using System;

namespace music.Extensions
{
    public static class OctaveExtensions
    {
        public static string ToString(this Octave octave)
        {
            switch (octave)
            {
                case Octave.Zeroth:
                    return "0";
                case Octave.First:
                    return "1";
                case Octave.Second:
                    return "2";
                case Octave.Third:
                    return "3";
                case Octave.Fourth:
                    return "4";
                case Octave.Fifth:
                    return "5";
                case Octave.Sixth:
                    return "6";
                case Octave.Seventh:
                    return "7";
                case Octave.Eighth:
                    return "8";
                case Octave.Ninth:
                    return "9";
                case Octave.Tenth:
                    return "10";
                default:
                    throw new ArgumentOutOfRangeException(nameof(octave), octave, null);
            }
        }
    }
}