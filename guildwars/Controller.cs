using System;
using music;

namespace guildwars
{
    public class Controller
    {
        private readonly Keyboard _keyboard;

        private Octave _octave = Octave.Fourth;

        public Controller(Keyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public void Play(Tone tone)
        {
            while (_octave != tone.Octave)
            {
                if (OctaveId(_octave) < OctaveId(tone.Octave))
                {
                    Press(0);
                    IncreaseOctave();
                }
                else if (OctaveId(_octave) > OctaveId(tone.Octave))
                {
                    Press(9);
                    DecreaseOctave();
                }
            }

            Press(Key(tone));
        }

        private void IncreaseOctave()
        {
            if (_octave == Octave.Third)
            {
                _octave = Octave.Fourth;
            }
            else if (_octave == Octave.Fourth)
            {
                _octave = Octave.Fifth;
            }
        }

        private void DecreaseOctave()
        {
            if (_octave == Octave.Fifth)
            {
                _octave = Octave.Fourth;
            }
            else if (_octave == Octave.Fourth)
            {
                _octave = Octave.Third;
            }
        }

        private int OctaveId(Octave octave)
        {
            switch (octave)
            {
                case Octave.Third:
                    return 3;
                case Octave.Fourth:
                    return 4;
                case Octave.Fifth:
                    return 5;
                case Octave.Sixth:
                    return 6;
                default:
                    throw new ArgumentOutOfRangeException(nameof(octave), octave, null);
            }
        }

        private int Key(Tone tone)
        {
            if (tone.Note == Note.C && tone.Octave == Octave.Sixth)
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