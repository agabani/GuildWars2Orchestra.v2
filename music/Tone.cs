namespace music
{
    public class Tone
    {
        public Tone(Note note, Octave octave)
        {
            Note = note;
            Octave = octave;
        }

        public bool IsRest { get; set; }
        public Note Note { get; }
        public Octave Octave { get; }

        public static bool operator >(Tone a, Tone b) => Value(a) > Value(b);
        public static bool operator <(Tone a, Tone b) => Value(a) < Value(b);
        public static bool operator <=(Tone a, Tone b) => Value(a) <= Value(b);
        public static bool operator >=(Tone a, Tone b) => Value(a) >= Value(b);

        private static int Value(Tone tone) => (int) tone.Octave*12 + (int) tone.Note;
    }
}