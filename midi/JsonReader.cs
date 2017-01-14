using System.IO;
using System.Linq;
using midi.Convertor;
using midi.Info;
using music;
using NAudio.Midi;
using Newtonsoft.Json;

namespace midi
{
    public class JsonReader
    {
        public Sheet Read(string path)
        {
            var profile = ReadProfile(path);

            var midiFile = new MidiFile(profile.Path);
            var midiInfo = new MidiInfo(profile.Path);

            return new Sheet
            {
                Profile = profile,
                Tempo = midiInfo.Tempo,
                Tokens = Notes(midiFile.Events, midiInfo.Tempo, midiFile.DeltaTicksPerQuarterNote)
            };
        }

        private static Profile ReadProfile(string path)
        {
            return JsonConvert.DeserializeObject<Profile>(File.ReadAllText(path));
        }

        private static Token[][] Notes(MidiEventCollection midiEventCollection, double tempo, int deltaTicksPerQuarterNote)
        {
            var tokens = new Token[midiEventCollection.Tracks][];

            for (var track = 0; track < midiEventCollection.Tracks; track++)
            {
                tokens[track] = midiEventCollection[track]
                    .OfType<NoteOnEvent>()
                    .Where(@event => @event.Velocity > 0)
                    .Select(@event => TokenConvertor.Convert(@event, tempo, deltaTicksPerQuarterNote))
                    .ToArray();
            }

            return tokens;
        }
    }
}