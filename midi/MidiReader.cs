﻿using System.Linq;
using midi.Convertor;
using midi.Info;
using music;
using NAudio.Midi;

namespace midi
{
    public class MidiReader
    {
        public Sheet Read(string path)
        {
            var midiFile = new MidiFile(path);
            var midiInfo = new MidiInfo(path);

            return new Sheet
            {
                Tempo = midiInfo.Tempo,
                Tokens = Notes(midiFile.Events, midiInfo.Tempo, midiFile.DeltaTicksPerQuarterNote)
            };
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