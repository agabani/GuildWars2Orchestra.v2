using System.Diagnostics;
using System.Linq;
using System.Threading;
using music;

namespace guildwars
{
    public class Jukebox
    {
        private readonly EventQueue _eventQueue;

        public Jukebox(EventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public void Play(Sheet sheet)
        {
            var enumerable = sheet.Tokens
                .SelectMany(pair => pair.Value)
                //.Last()
                .Where(token => IsToneInRange(token.Tone))
                .OrderBy(token => token.AbsoluteTime)
                .ToArray();


            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int index = 0; index < enumerable.Length;)
            {
                if (stopwatch.ElapsedMilliseconds > enumerable[index].AbsoluteTime.TotalMilliseconds)
                {
                    _eventQueue.Queue(enumerable[index]);
                    index++;
                }

                Thread.Sleep(1);
            }
        }


        private bool IsToneInRange(Tone tone)
        {
            if (tone.Note == Note.C && tone.Octave == Octave.Seventh)
            {
                return true;
            }

            return tone.Octave == Octave.Fourth
                   || tone.Octave == Octave.Fifth
                   || tone.Octave == Octave.Sixth;
        }
    }
}