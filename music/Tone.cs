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

        public override bool Equals(object obj)
        {
            var tone = obj as Tone;
            return tone != null && tone.Equals(this);
        }

        protected bool Equals(Tone other)
        {
            return IsRest == other.IsRest && Note == other.Note && Octave == other.Octave;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsRest.GetHashCode();
                hashCode = (hashCode*397) ^ (int) Note;
                hashCode = (hashCode*397) ^ (int) Octave;
                return hashCode;
            }
        }
    }
}