using System;
using System.Linq;
using music;
using NAudio.Midi;

namespace midi
{
    public class MidiReader
    {
        public Sheet Read(string path)
        {
            var midiFile = new MidiFile(path);

            var tempo = Tempo(midiFile.Events);

            var timeSignature = TimeSignature(midiFile.Events);

            return new Sheet
            {
                Tempo = tempo,
                Tokens = Notes(midiFile.Events, tempo, midiFile.DeltaTicksPerQuarterNote)
            };
        }

        private static double Tempo(MidiEventCollection midiEventCollection)
        {
            return midiEventCollection
                .SelectMany(@event => @event)
                .OfType<TempoEvent>()
                .Where(@event => @event.MetaEventType == MetaEventType.SetTempo)
                .OrderBy(@event => @event.AbsoluteTime)
                .Last().Tempo;
        }

        private static string TimeSignature(MidiEventCollection midiEventCollection)
        {
            return midiEventCollection
                .SelectMany(x => x)
                .OfType<TimeSignatureEvent>()
                .Where(@event => @event.MetaEventType == MetaEventType.TimeSignature)
                .OrderBy(@event => @event.AbsoluteTime)
                .Last().TimeSignature;
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