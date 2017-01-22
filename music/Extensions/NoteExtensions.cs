using System;

namespace music.Extensions
{
    public static class NoteExtensions
    {
        public static string ToString(this Note note)
        {
            switch (note)
            {
                case Note.C:
                    return "C";
                case Note.CSharp:
                    return "C#";
                case Note.D:
                    return "D";
                case Note.DSharp:
                    return "D#";
                case Note.E:
                    return "E";
                case Note.F:
                    return "F";
                case Note.FSharp:
                    return "F#";
                case Note.G:
                    return "G";
                case Note.GSharp:
                    return "G#";
                case Note.A:
                    return "A";
                case Note.ASharp:
                    return "A#";
                case Note.B:
                    return "B";
                default:
                    throw new ArgumentOutOfRangeException(nameof(note), note, null);
            }
        }
    }
}