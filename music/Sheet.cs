using System.Collections.Generic;

namespace music
{
    public class Sheet
    {
        public double Tempo { get; set; }
        public Dictionary<int, Token[]> Tokens { get; set; }
    }
}