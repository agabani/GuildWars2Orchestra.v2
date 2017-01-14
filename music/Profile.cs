using System.Collections.Generic;

namespace music
{
    public class Profile
    {
        public string Path { get; set; }
        public double? Speed { get; set; }
        public Dictionary<int, Track> Tracks { get; set; }
    }

    public class Track
    {
        public bool Ignore { get; set; }
    }
}