using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;

namespace midi
{
    public class Class
    {
        public void Run()
        {
            var midiFile = new MidiFile("prelude.mid");

            var tempo = Tempo(midiFile.Events);

            var timeSignature = TimeSignature(midiFile.Events);

            var notes = Notes(midiFile.Events);
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

        private static IEnumerable<NoteOnEvent> Notes(MidiEventCollection midiEventCollection)
        {
            return midiEventCollection
                .SelectMany(@event => @event)
                .OfType<NoteOnEvent>()
                .Where(@event => @event.Velocity > 0);
        }
    }
}