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
        public Note Note { get; private set; }
        public Octave Octave { get; private set; }
    }
}