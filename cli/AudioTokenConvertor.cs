using System;
using cli.models;

namespace cli
{
    internal class AudioTokenConvertor
    {
        private const double Frequency0 = 16.35;
        private static readonly Tone Note0 = new Tone {Note = Note.C, Octave = Octave.Zeroth};

        private static readonly Note[] NoteSequence = {Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A, Note.ASharp, Note.B};

        private readonly double _secondsPerBeat;

        public AudioTokenConvertor(double bpm)
        {
            _secondsPerBeat = 60/bpm;
        }

        public AudioToken Convert(Token token)
        {
            return new AudioToken
            {
                Duration = Duration(token),
                Frequency = Frequency(token)
            };
        }

        private TimeSpan Duration(Token token)
        {
            double duration;

            switch (token.Length.Fraction)
            {
                case Fraction.Full:
                    duration = 4*1000*_secondsPerBeat;
                    break;
                case Fraction.Half:
                    duration = 2*1000*_secondsPerBeat;
                    break;
                case Fraction.Quater:
                    duration = 1*1000*_secondsPerBeat;
                    break;
                case Fraction.Eighth:
                    duration = .5*1000*_secondsPerBeat;
                    break;
                case Fraction.Sixteenth:
                    duration = .25*1000*_secondsPerBeat;
                    break;
                case Fraction.Thirtyseconth:
                    duration = .125*1000*_secondsPerBeat;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return token.Length.Extended ? TimeSpan.FromMilliseconds(duration*1.5) : TimeSpan.FromMilliseconds(duration);
        }

        private static double Frequency(Token token)
        {
            return token.Tone.IsRest
                ? 0
                : Frequency0*Math.Pow(Math.Pow(2, (double) 1/12), SemiTonesBetween(Note0, token.Tone));
        }

        private static int SemiTonesBetween(Tone lower, Tone upper)
        {
            return OverAllIndex(upper) - OverAllIndex(lower);
        }

        private static int OverAllIndex(Tone tone)
        {
            return NoteIndex(tone) + OctiveNumeric(tone.Octave)*12;
        }

        private static int NoteIndex(Tone tone)
        {
            return Array.IndexOf(NoteSequence, tone.Note);
        }

        private static int OctiveNumeric(Octave octave)
        {
            switch (octave)
            {
                case Octave.Zeroth:
                    return 0;
                case Octave.First:
                    return 1;
                case Octave.Second:
                    return 2;
                case Octave.Third:
                    return 3;
                case Octave.Fourth:
                    return 4;
                case Octave.Fifth:
                    return 5;
                case Octave.Sixth:
                    return 6;
                case Octave.Seventh:
                    return 7;
                case Octave.Eighth:
                    return 8;
                default:
                    throw new ArgumentOutOfRangeException(nameof(octave), octave, null);
            }
        }
    }
}