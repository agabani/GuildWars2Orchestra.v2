using System.Collections.Generic;
using System.Linq;
using midi.Convertor;
using music;
using NAudio.Midi;

namespace midi.Info
{
    public class TrackInfo
    {
        public TrackInfo(IEnumerable<MidiEvent> events)
        {
            var noteOnEvents = GetNoteOnEvents(events);

            NumberOfNotes = GetNumberOfNotes(noteOnEvents);
            LowestTone = GetLowestTone(noteOnEvents);
            HighestTone = GetHighestTone(noteOnEvents);
        }

        public int NumberOfNotes { get; private set; }
        public Tone LowestTone { get; private set; }
        public Tone HighestTone { get; private set; }

        private static NoteOnEvent[] GetNoteOnEvents(IEnumerable<MidiEvent> events)
        {
            return events
                .OfType<NoteOnEvent>()
                .Where(@event => @event.Velocity > 0)
                .ToArray();
        }

        private static int GetNumberOfNotes(NoteOnEvent[] noteOnEvents)
        {
            return noteOnEvents.Length;
        }

        private static Tone GetLowestTone(NoteOnEvent[] noteOnEvents)
        {
            var noteOnEvent = noteOnEvents.OrderBy(@event => @event.NoteNumber).FirstOrDefault();
            return noteOnEvent != null ? ToneConvertor.Convert(noteOnEvent) : null;
        }

        private static Tone GetHighestTone(NoteOnEvent[] noteOnEvents)
        {
            var noteOnEvent = noteOnEvents.OrderByDescending(@event => @event.NoteNumber).FirstOrDefault();
            return noteOnEvent != null ? ToneConvertor.Convert(noteOnEvent) : null;
        }
    }
}