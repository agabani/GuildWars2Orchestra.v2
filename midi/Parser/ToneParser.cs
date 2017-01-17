using System;
using System.Text.RegularExpressions;
using music;

namespace midi.Parser
{
    internal static class ToneParser
    {
        private static readonly Regex DeserializeRegex = new Regex("([-,]?)([A-G])(#)?(\\d+)");

        public static Tone FromString(string @string)
        {
            throw new NotImplementedException();
        }
    }
}