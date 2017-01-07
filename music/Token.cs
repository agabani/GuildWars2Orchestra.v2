using System;

namespace music
{
    public class Token
    {
        public Length Length { get; set; }
        public Tone Tone { get; set; }
        public TimeSpan AbsoluteTime { get; set; }
    }
}