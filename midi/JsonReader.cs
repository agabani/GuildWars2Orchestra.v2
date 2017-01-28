using System.Collections.Generic;
using System.IO;
using System.Linq;
using midi.Convertor;
using midi.MetaData;
using music;
using NAudio.Midi;
using Newtonsoft.Json;

namespace midi
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
                Tempo = midiInfo.Tempo,
                Tokens = Notes(midiFile.Events, midiInfo.Tempo, midiFile.DeltaTicksPerQuarterNote, profile.TrackFilters)
            };
        }

        private static JsonProfile ReadJsonProfile(string path)
        {
            return JsonConvert.DeserializeObject<JsonProfile>(File.ReadAllText(path));
        }

        private static Dictionary<int, Token[]> Notes(MidiEventCollection midiEventCollection, double tempo, int deltaTicksPerQuarterNote, Dictionary<int, TrackFilter> tracks)
        {
            var tokens = new Dictionary<int, Token[]>();

            for (var track = 0; track < midiEventCollection.Tracks; track++)
            {
                var array = midiEventCollection[track]
                    .OfType<NoteOnEvent>()
                    .Where(@event => @event.Velocity > 0)
                    .Select(@event => TokenConvertor.Convert(@event, tempo, deltaTicksPerQuarterNote))
                    .Where(token => CanPlay(token, tracks[track]))
                    .ToArray();

                tokens.Add(track, array);
            }

            return tokens;
        }

        private static bool CanPlay(Token token, TrackFilter trackFilter)
        {
            if (trackFilter.Ignore)
            {
                return false;
            }
            if (trackFilter.ToneFilter.IsAllowed(token.Tone))
            {
                return true;
            }
            return false;
        }
    }
}