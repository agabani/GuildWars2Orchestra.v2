using System;

namespace cli
{
    internal class Assembler
    {
        private const double Bpm = 120;
        private const double SecondsPerBeat = 60/Bpm;

        public TimeSpan DurationFromToken(Token token)
        {
            double duration;

            switch (token.Length.Fraction)
            {
                case Fraction.Full:
                    duration = 4*1000*SecondsPerBeat;
                    break;
                case Fraction.Half:
                    duration = 2*1000*SecondsPerBeat;
                    break;
                case Fraction.Quater:
                    duration = 1*1000*SecondsPerBeat;
                    break;
                case Fraction.Eighth:
                    duration = .5*1000*SecondsPerBeat;
                    break;
                case Fraction.Sixteenth:
                    duration = .25*1000*SecondsPerBeat;
                    break;
                case Fraction.Thirtyseconth:
                    duration = .125*1000*SecondsPerBeat;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return token.Length.Extended ? TimeSpan.FromMilliseconds(duration*1.5) : TimeSpan.FromMilliseconds(duration);
        }

        public double FrequencyFromToken(Token token)
        {
            var gap = SemiTonesBetween(new Tone {Note = Note.C, Octave = Octave.Zeroth}, token.Tone);

            return 16.35*Math.Pow(Math.Pow(2, (double) 1/12), gap);
        }

        public int SemiTonesBetween(Tone lower, Tone upper)
        {
            return OverAllIndex(upper) - OverAllIndex(lower);
        }

        public int OverAllIndex(Tone tone)
        {
            var noteSequence = new[] {Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A, Note.ASharp, Note.B};

            var noteIndex = Array.IndexOf(noteSequence, tone.Note);

            return noteIndex + (OctiveNumeric(tone.Octave) )*12;
        }

        public int OctiveNumeric(Octave octave)
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

    internal class Tone
    {
        public Note Note { get; set; }
        public Octave Octave { get; set; }
    }

    internal class Token
    {
        public Length Length { get; set; }
        public Tone Tone { get; set; }
    }

    internal class Length
    {
        public Fraction Fraction { get; set; }
        public bool Extended { get; set; }
    }

    public enum Fraction
    {
        Full,
        Half,
        Quater,
        Eighth,
        Sixteenth,
        Thirtyseconth
    }

    public enum Note
    {
        A,
        ASharp,
        B,
        C,
        CSharp,
        D,
        DSharp,
        E,
        F,
        FSharp,
        G,
        GSharp
    }

    public enum Octave
    {
        Zeroth,
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eighth
    }
}