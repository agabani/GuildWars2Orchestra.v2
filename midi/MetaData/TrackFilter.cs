using midi.Filter;

namespace midi.MetaData
{
    public class TrackFilter
    {
        public bool Ignore { get; set; }
        public IToneFilter ToneFilter { get; set; }
    }
}