using System;
using music;
using NAudio.Midi;

namespace midi.Convertor
{
    internal class ToneConvertor
    {
        internal static Tone Convert(NoteOnEvent @event)
        {
            return new Tone
            {
                Note = Note(@event),
                Octave = Octave(@event)
            };
        }

        private static Note Note(NoteOnEvent @event)
        {
            switch (@event.NoteNumber%12)
            {
                case 0:
                    return music.Note.C;
                case 1:
                    return music.Note.CSharp;
                case 2:
                    return music.Note.D;
                case 3:
                    return music.Note.DSharp;
                case 4:
                    return music.Note.E;
                case 5:
                    return music.Note.F;
                case 6:
                    return music.Note.FSharp;
                case 7:
                    return music.Note.G;
                case 8:
                    return music.Note.GSharp;
                case 9:
                    return music.Note.A;
                case 10:
                    return music.Note.ASharp;
                case 11:
                    return music.Note.B;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static Octave Octave(NoteOnEvent @event)
        {
            switch (@event.NoteNumber/12)
            {
                case 0:
                    return music.Octave.Zeroth;
                case 1:
                    return music.Octave.First;
                case 2:
                    return music.Octave.Second;
                case 3:
                    return music.Octave.Third;
                case 4:
                    return music.Octave.Fourth;
                case 5:
                    return music.Octave.Fifth;
                case 6:
                    return music.Octave.Sixth;
                case 7:
                    return music.Octave.Seventh;
                case 8:
                    return music.Octave.Eighth;
                case 9:
                    return music.Octave.Ninth;
                case 10:
                    return music.Octave.Tenth;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}