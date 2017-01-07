using System;
using System.Linq;
using music;
using NAudio.Midi;

namespace midi
{
    public class MidiReader
    {
        public Sheet Read(string path)
        {
            var midiFile = new MidiFile(path);

            var tempo = Tempo(midiFile.Events);

            var timeSignature = TimeSignature(midiFile.Events);

            return new Sheet
            {
                Tempo = tempo,
                Tokens = Notes(midiFile.Events, tempo, midiFile.DeltaTicksPerQuarterNote)
            };
        }

        private static double Tempo(MidiEventCollection midiEventCollection)
        {
            return midiEventCollection
                .SelectMany(@event => @event)
                .OfType<TempoEvent>()
                .Where(@event => @event.MetaEventType == MetaEventType.SetTempo)
                .OrderBy(@event => @event.AbsoluteTime)
                .Last().Tempo;
        }

        private static string TimeSignature(MidiEventCollection midiEventCollection)
        {
            return midiEventCollection
                .SelectMany(x => x)
                .OfType<TimeSignatureEvent>()
                .Where(@event => @event.MetaEventType == MetaEventType.TimeSignature)
                .OrderBy(@event => @event.AbsoluteTime)
                .Last().TimeSignature;
        }

        private static Token[][] Notes(MidiEventCollection midiEventCollection, double tempo, int deltaTicksPerQuarterNote)
        {
            var tokens = new Token[midiEventCollection.Tracks][];

            for (var track = 0; track < midiEventCollection.Tracks; track++)
            {
                tokens[track] = midiEventCollection[track]
                    .OfType<NoteOnEvent>()
                    .Where(@event => @event.Velocity > 0)
                    .Select(@event => Convert(@event, tempo, deltaTicksPerQuarterNote))
                    .ToArray();
            }

            return tokens;
        }

        private static Token Convert(NoteOnEvent @event, double tempo, int deltaTicksPerQuarterNote)
        {
            Note note;
            switch (@event.NoteNumber%12)
            {
                case 0:
                    note = Note.C;
                    break;
                case 1:
                    note = Note.CSharp;
                    break;
                case 2:
                    note = Note.D;
                    break;
                case 3:
                    note = Note.DSharp;
                    break;
                case 4:
                    note = Note.E;
                    break;
                case 5:
                    note = Note.F;
                    break;
                case 6:
                    note = Note.FSharp;
                    break;
                case 7:
                    note = Note.G;
                    break;
                case 8:
                    note = Note.GSharp;
                    break;
                case 9:
                    note = Note.A;
                    break;
                case 10:
                    note = Note.ASharp;
                    break;
                case 11:
                    note = Note.B;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Octave octave;
            switch (@event.NoteNumber/12)
            {
                case 0:
                    octave = Octave.Zeroth;
                    break;
                case 1:
                    octave = Octave.First;
                    break;
                case 2:
                    octave = Octave.Second;
                    break;
                case 3:
                    octave = Octave.Third;
                    break;
                case 4:
                    octave = Octave.Fourth;
                    break;
                case 5:
                    octave = Octave.Fifth;
                    break;
                case 6:
                    octave = Octave.Sixth;
                    break;
                case 7:
                    octave = Octave.Seventh;
                    break;
                case 8:
                    octave = Octave.Eighth;
                    break;
                case 9:
                    octave = Octave.Ninth;
                    break;
                case 10:
                    octave = Octave.Tenth;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Token(
                note, octave,
                ToMilliseconds(@event.NoteLength, tempo, deltaTicksPerQuarterNote), tempo,
                ToMilliseconds(@event.AbsoluteTime, tempo, deltaTicksPerQuarterNote)
                );
        }

        private static Token Token(Note note, Octave octave, double noteLengthMs, double tempo, double absoluteTimeMs)
        {
            return new Token
            {
                Length = new Length
                {
                    Fraction = Fraction(noteLengthMs, tempo)
                },
                Tone = new Tone
                {
                    Note = note,
                    Octave = octave
                },
                AbsoluteTime = TimeSpan.FromMilliseconds(absoluteTimeMs)
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