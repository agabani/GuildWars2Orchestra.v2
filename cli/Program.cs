namespace cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var signalGenerator = new SignalGenerator();
            var wavePacker = new WavePacker();
            var assembler = new Assembler();

            var token = new Token
            {
                Length = new Length
                {
                    Extended = false,
                    Fraction = Fraction.Full
                },
                Tone = new Tone
                {
                    Note = Note.C,
                    Octave = Octave.Third
                }
            };

            var duration = assembler.DurationFromToken(token);
            var frequency = assembler.FrequencyFromToken(token);

            var generateSamples = signalGenerator.GenerateSamples((long) duration.TotalMilliseconds, frequency);
            var memoryStream = wavePacker.Pack(generateSamples);
            wavePacker.Write("text.wav", memoryStream);
        }
    }
}