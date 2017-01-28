using System;
using System.Collections.Generic;
using guildwars;
using midi;
using midi.Info;
using music;

namespace cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sheet = InputFromJson();
            Info("data/prelude.mid");
            OutputToWave(sheet);
            //OutputToGuildwars(sheet);
        }

        private static Sheet InputFromHardcode()
        {
            var length = new Length {Extended = false, Fraction = Fraction.Quater};

            var tokens = new List<Token>
            {
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.D, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.G, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.D, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.G, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Fifth)},
                new Token {Length = length, Tone = new Tone(Note.G, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.D, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.G, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.D, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.A, Octave.Second)},
                new Token {Length = length, Tone = new Tone(Note.B, Octave.Second)},
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.A, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.B, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.A, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Fourth)},
                new Token {Length = length, Tone = new Tone(Note.B, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.A, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.E, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.C, Octave.Third)},
                new Token {Length = length, Tone = new Tone(Note.B, Octave.Second)}
            };

            return new Sheet
            {
                Tempo = 200,
                Tokens = new Dictionary<int, Token[]> {{0, tokens.ToArray()}}
            };
        }

        private static Sheet InputFromMidi()
        {
            return new MidiReader().Read("data/prelude.mid");
        }

        private static Sheet InputFromJson()
        {
            return new JsonReader().Read("data/prelude.json");
        }

        private static void Info(string path)
        {
            var midiInfo = new MidiInfo(path);

            Console.WriteLine($"Path:           {path}");
            Console.WriteLine($"Tempo:          {midiInfo.Tempo}");
            Console.WriteLine($"Time Signature: {midiInfo.TimeSignature}");
            Console.WriteLine($"Tracks:         {midiInfo.TrackInfos.Count}");

            foreach (var keyValuePair in midiInfo.TrackInfos)
            {
                Console.WriteLine($"Track: {keyValuePair.Key}");
                Console.WriteLine($"    Number of Notes: {keyValuePair.Value.NumberOfNotes}");
                if (keyValuePair.Value.LowestTone != null)
                {
                    Console.WriteLine($"    Lowest Tone:     {keyValuePair.Value.LowestTone.Note}.{keyValuePair.Value.LowestTone.Octave}");
                }
                if (keyValuePair.Value.HighestTone != null)
                {
                    Console.WriteLine($"    Highest Tone:    {keyValuePair.Value.HighestTone.Note}.{keyValuePair.Value.HighestTone.Octave}");
                }
            }
        }

        private static void OutputToGuildwars(Sheet sheet)
        {
            var eventQueue = new EventQueue();

            var player = new Player(eventQueue, new Controller(new Keyboard()));

            player.Start();

            var jukebox = new Jukebox(eventQueue);

            jukebox.Play(sheet);
        }

        private static void OutputToWave(Sheet sheet)
        {
            var generator = new TimedSignalGenerator(new AudioTokenConvertor(sheet.Tempo));

            var audioSamples = generator.Generate(sheet);

            var wavePacker = new WavePacker();
            using (var memoryStream = wavePacker.Pack(audioSamples))
            {
                wavePacker.Write("output.wav", memoryStream);
            }
        }
    }
}