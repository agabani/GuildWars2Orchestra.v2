using System;

namespace cli
{
    internal class SignalGenerator
    {
        private const double SampleRate = 44100;
        private readonly double _sixteenBitSampleLimit = Math.Pow(2, 16)/2 - 1;

        public short[] GenerateSamples(long milliseconds, double frequency)
        {
            var numberOfSamples = (long) (milliseconds/1000.0*SampleRate);

            var samples = new short[numberOfSamples];

            for (var i = 0; i < numberOfSamples; i++)
            {
                samples[i] = ToAmplitute(i, frequency, 0.8*Math.Pow(Math.E, -1*i/SampleRate));
            }

            return samples;
        }

        private short ToAmplitute(long sample, double frequency, double decay)
        {
            return (short) (Math.Sin(sample*2*Math.PI*frequency/SampleRate)*_sixteenBitSampleLimit*decay);
        }
    }
}