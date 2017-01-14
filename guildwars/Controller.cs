using System;
using System.Threading;
using music;

namespace guildwars
{
    public class Controller
    {
        private readonly Keyboard _keyboard;

        private Octave _octave = Octave.Fifth;

        public Controller(Keyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public void Play(Tone tone)
        {
            while (_octave != tone.Octave)
            {
                if (IsIncreaseOctaveRequired(tone))
                {
                    IncreaseOctave();
                }
                else if (IsDecreaseOctaveRequired(tone))
                {
                    DecreaseOctave();
                }
                Thread.Sleep(1);
            }

            Press(Key(tone));

            Thread.Sleep(100);
        }

        private bool IsIncreaseOctaveRequired(Tone tone)
        {
            return OctaveId(_octave) < OctaveId(tone.Octave);
        }

        private bool IsDecreaseOctaveRequired(Tone tone)
        {
            return OctaveId(_octave) > OctaveId(tone.Octave);
        }

        private void IncreaseOctave()
        {
            if (_octave == Octave.Fourth)
            {
                Press(0);
                _octave = Octave.Fifth;
            }
            else if (_octave == Octave.Fifth)
            {
                Press(0);
                _octave = Octave.Sixth;
            }
            else if (_octave == Octave.Sixth)
            {
                _octave = Octave.Seventh;
            }
        }

        private void DecreaseOctave()
        {
            if (_octave == Octave.Seventh)
            {
                _octave = Octave.Sixth;
            }
            else if(_octave == Octave.Sixth)
            {
                Press(9);
                _octave = Octave.Fifth;
            }
            else if (_octave == Octave.Fifth)
            {
                Press(9);
                _octave = Octave.Fourth;
            }
        }

        private int OctaveId(Octave octave)
        {
            switch (octave)
            {
                case Octave.Fourth:
                    return 4;
                case Octave.Fifth:
                    return 5;
                case Octave.Sixth:
                    return 6;
                case Octave.Seventh:
                    return 7;
                default:
                    throw new ArgumentOutOfRangeException(nameof(octave), octave, null);
            }
        }

        private int Key(Tone tone)
        {
            if (tone.Note == Note.C && tone.Octave == Octave.Seventh)
            {
                return 8;
            }

            switch (tone.Note)
            {
                case Note.C:
                    return 1;
                case Note.CSharp:
                    return 2;
                case Note.D:
                    return 2;
                case Note.DSharp:
                    return 3;
                case Note.E:
                    return 3;
                case Note.F:
                    return 4;
                case Note.FSharp:
                    return 5;
                case Note.G:
                    return 5;
                case Note.GSharp:
                    return 6;
                case Note.A:
                    return 6;
                case Note.ASharp:
                    return 7;
                case Note.B:
                    return 7;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Press(int key)
        {
            _keyboard.Press(key);
            _keyboard.Release(key);
        }
    }
}