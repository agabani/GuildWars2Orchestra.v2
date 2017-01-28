using System.Collections.Generic;
using System.IO;
using System.Linq;
using midi.Convertor;
using midi.MetaData;
using music;
using NAudio.Midi;
using Newtonsoft.Json;

namespace midi.Reader
{
    public class JsonReader
    {
        public Sheet Read(string path)
        {
            var profile = new Profile(ReadJsonProfile(path));

            var midiFile = new MidiFile(profile.Path);
            var midiInfo = new MidiInfo(profile.Path);

            return new Sheet
            {
                Tempo = midiInfo.Tempo * profile.Speed,
                Tokens = Notes(midiFile.Events, midiInfo.Tempo * profile.Speed, midiFile.DeltaTicksPerQuarterNote, profile)
            };
        }

        private static JsonProfile ReadJsonProfile(string path)
        {
            return JsonConvert.DeserializeObject<JsonProfile>(File.ReadAllText(path));
        }

        private static Dictionary<int, Token[]> Notes(MidiEventCollection midiEventCollection, double tempo, int deltaTicksPerQuarterNote, Profile profile)
        {
            var tokens = new Dictionary<int, Token[]>();

            for (var track = 0; track < midiEventCollection.Tracks; track++)
            {
                var array = midiEventCollection[track]
                    .OfType<NoteOnEvent>()
                    .Where(@event => @event.Velocity > 0)
                    .Select(@event => TokenConvertor.Convert(@event, tempo, deltaTicksPerQuarterNote))
                    .Where(token => CanPlay(token, profile.TrackFilters[track]))
                    .ToArray();

                tokens.Add(track, array);
            }

            return tokens;
        }

        private static bool CanPlay(Token token, TrackFilter trackFilter)
        {
            return !trackFilter.Ignore && trackFilter.ToneFilter.IsAllowed(token.Tone);
        }
    }
}