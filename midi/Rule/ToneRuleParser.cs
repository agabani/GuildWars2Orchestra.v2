using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using music;

namespace midi.Rule
{
    public static class ToneRuleParser
    {
        private static readonly Regex DeserializeRegex = new Regex("([-,]?)([A-G])(#)?(\\d+)");

        public static IRule FromString(string @string)
        {
            var matchCollection = DeserializeRegex.Matches(@string);

            var rules = new List<IRule>();

            foreach (Match match in matchCollection)
            {
                var prefix = match.Groups[1];

                var note = ToTone(match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value);
                rules.Add(new SingleRule(note));
            }

            return new AnyRule(rules);
        }

        private static Tone ToTone(string note, string sharp, string octave)
        {
            return new Tone(ToNote(note, sharp), ToOctave(octave));
        }

        private static Note ToNote(string note, string sharp)
        {
            switch (note)
            {
                case "C":
                    switch (sharp)
                    {
                        case "":
                            return Note.C;
                        case "#":
                            return Note.CSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp), sharp, null);
                    }
                case "D":
                    switch (sharp)
                    {
                        case "":
                            return Note.D;
                        case "#":
                            return Note.DSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp), sharp, null);
                    }
                case "E":
                    switch (sharp)
                    {
                        case "":
                            return Note.E;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp), sharp, null);
                    }
                case "F":
                    switch (sharp)
                    {
                        case "":
                            return Note.F;
                        case "#":
                            return Note.FSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp), sharp, null);
                    }
                case "G":
                    switch (sharp)
                    {
                        case "":
                            return Note.G;
                        case "#":
                            return Note.GSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp), sharp, null);
                    }
                case "A":
                    switch (sharp)
                    {
                        case "":
                            return Note.A;
                        case "#":
                            return Note.ASharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp), sharp, null);
                    }
                case "B":
                    switch (sharp)
                    {
                        case "":
                            return Note.B;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp), sharp, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(note), note, null);
            }
        }


        private static Octave ToOctave(string octave)
        {
            switch (octave)
            {
                case "0":
                    return Octave.Zeroth;
                case "1":
                    return Octave.First;
                case "2":
                    return Octave.Second;
                case "3":
                    return Octave.Third;
                case "4":
                    return Octave.Fourth;
                case "5":
                    return Octave.Fifth;
                case "6":
                    return Octave.Sixth;
                case "7":
                    return Octave.Seventh;
                case "8":
                    return Octave.Eighth;
                case "9":
                    return Octave.Ninth;
                case "10":
                    return Octave.Tenth;
                default:
                    throw new ArgumentOutOfRangeException(nameof(octave), octave, null);
            }
        }
    }
}