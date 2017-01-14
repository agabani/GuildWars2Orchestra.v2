using System.Collections.Generic;
using System.Linq;
using guildwars;
using midi;
using music;

namespace cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Guildwars();
            //Midi();
            //Generate();
        }

        private static void Guildwars()
        {
            var sheet = new MidiReader().Read("Pokemon Red Version  Pokemon Blue Version - Pokemon Center.mid");

            var eventQueue = new EventQueue();

            var player = new Player(eventQueue, new Controller(new Keyboard()));

            player.Start();

            var jukebox = new Jukebox(eventQueue);

            jukebox.Play(sheet);

        }

        private static void Midi()
        {
            var sheet = new MidiReader().Read("Final Fantasy.mid");

            var generator = new TimedSignalGenerator(new AudioTokenConvertor(sheet.Tempo));

            var audioSamples = generator.Generate(sheet);

            var wavePacker = new WavePacker();
            using (var memoryStream = wavePacker.Pack(audioSamples))
            {
                wavePacker.Write("midi.wav", memoryStream);
            }
        }

        private static void Generate()
        {
            var length = new Length {Extended = false, Fraction = Fraction.Quater};

            var tokens = new List<Token>
            {
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.D, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.G, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.D, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.G, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Fifth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.G, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.D, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.G, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.D, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.A, Octave = Octave.Second}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.B, Octave = Octave.Second}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.A, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.B, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.A, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Fourth}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.B, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.A, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.E, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.C, Octave = Octave.Third}},
                new Token {Length = length, Tone = new Tone {IsRest = false, Note = Note.B, Octave = Octave.Second}}
            };

            var convertor = new AudioTokenConvertor(200);
            var signalGenerator = new SignalGenerator();
            var wavePacker = new WavePacker();

            var audioSamples = tokens
                .Select(token => convertor.Convert(token))
                .Select(audioToken => signalGenerator.GenerateSamples(audioToken))
                .SelectMany(audioTokenSamples => audioTokenSamples)
                .ToArray();

            using (var memoryStream = wavePacker.Pack(audioSamples))
            {
                wavePacker.Write("gen.wav", memoryStream);
            }
        }
    }
}