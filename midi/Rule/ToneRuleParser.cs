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

                var note = ToTone(match.Groups[2], match.Groups[3], match.Groups[4]);
                rules.Add(new SingleRule(note));
            }

            return new AnyRule(rules);
        }

        private static Tone ToTone(Group note, Group sharp, Group octave)
        {
            return new Tone(ToNote(note, sharp), ToOctave(octave));
        }

        private static Note ToNote(Group note, Group sharp)
        {
            switch (note.Value)
            {
                case "C":
                    switch (sharp.Value)
                    {
                        case "":
                            return Note.C;
                        case "#":
                            return Note.CSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp.Value), sharp.Value, null);
                    }
                case "D":
                    switch (sharp.Value)
                    {
                        case "":
                            return Note.D;
                        case "#":
                            return Note.DSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp.Value), sharp.Value, null);
                    }
                case "E":
                    switch (sharp.Value)
                    {
                        case "":
                            return Note.E;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp.Value), sharp.Value, null);
                    }
                case "F":
                    switch (sharp.Value)
                    {
                        case "":
                            return Note.F;
                        case "#":
                            return Note.FSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp.Value), sharp.Value, null);
                    }
                case "G":
                    switch (sharp.Value)
                    {
                        case "":
                            return Note.G;
                        case "#":
                            return Note.GSharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp.Value), sharp.Value, null);
                    }
                case "A":
                    switch (sharp.Value)
                    {
                        case "":
                            return Note.A;
                        case "#":
                            return Note.ASharp;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp.Value), sharp.Value, null);
                    }
                case "B":
                    switch (sharp.Value)
                    {
                        case "":
                            return Note.B;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(sharp.Value), sharp.Value, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(note.Value), note.Value, null);
            }
        }


        private static Octave ToOctave(Group octave)
        {
            switch (octave.Value)
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