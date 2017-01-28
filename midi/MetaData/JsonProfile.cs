using System.Collections.Generic;
using midi.Filter;

namespace midi.MetaData
{
    public class JsonProfile
    {
        public string Path { get; set; }
        public double? Speed { get; set; }
        public Dictionary<int, JsonTrackFilter> TrackFilters { get; set; }
    }

    public class Profile
    {
        public string Path { get; set; }
        public double Speed { get; set; }
        public Dictionary<int, TrackFilter> TrackFilters { get; set; }

        public Profile(JsonProfile jsonProfile)
        {
            Path = jsonProfile.Path;
            Speed = jsonProfile.Speed ?? 1;

            TrackFilters = new Dictionary<int, TrackFilter>();
            foreach (var jsonTrackFilter in jsonProfile.TrackFilters)
            {
                var toneFilter = jsonTrackFilter.Value.Tone != null ? ToneFilterParser.FromString(string.Join(",", jsonTrackFilter.Value.Tone)) : new TrueToneFilter();

                TrackFilters.Add(jsonTrackFilter.Key, new TrackFilter
                {
                    Ignore = jsonTrackFilter.Value.Ignore,
                    ToneFilter = toneFilter
                });
            }
        }
    }
}