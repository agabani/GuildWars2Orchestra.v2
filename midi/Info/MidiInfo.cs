using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NAudio.Midi;

namespace midi.Info
{
    public class MidiInfo
    {
        public MidiInfo(string path)
        {
            var file = new MidiFile(path);
            Tempo = GetTempo(file);
            TimeSignature = GetTimeSignature(file);
            TrackInfos = GetTrackInfos(file);
        }

        public double Tempo { get; private set; }
        public string TimeSignature { get; private set; }
        public IReadOnlyDictionary<int, TrackInfo> TrackInfos { get; private set; }

        private static double GetTempo(MidiFile file)
        {
            return file.Events
                .SelectMany(@event => @event)
                .OfType<TempoEvent>()
                .Where(@event => @event.MetaEventType == MetaEventType.SetTempo)
                .OrderBy(@event => @event.AbsoluteTime)
                .Last().Tempo;
        }

        private static string GetTimeSignature(MidiFile file)
        {
            return file.Events
                .SelectMany(x => x)
                .OfType<TimeSignatureEvent>()
                .Where(@event => @event.MetaEventType == MetaEventType.TimeSignature)
                .OrderBy(@event => @event.AbsoluteTime)
                .Last().TimeSignature;
        }

        private static IReadOnlyDictionary<int, TrackInfo> GetTrackInfos(MidiFile file)
        {
            var trackInfos = new Dictionary<int, TrackInfo>();

            for (var i = 0; i < file.Tracks; i++)
            {
                trackInfos.Add(i, new TrackInfo(file.Events[i]));
            }

            return new ReadOnlyDictionary<int, TrackInfo>(trackInfos);
        }
    }
}