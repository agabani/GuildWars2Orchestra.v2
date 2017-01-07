using System;
using System.Linq;
using music;

namespace cli
{
    public class TimedSignalGenerator
    {
        private const double SampleRate = 44100;

        private readonly AudioTokenConvertor _convertor;
        private readonly double _sixteenBitSampleLimit = Math.Pow(2, 16)/2 - 1;

        public TimedSignalGenerator(AudioTokenConvertor convertor)
        {
            _convertor = convertor;
        }

        public short[] Generate(Sheet sheet)
        {
            var lengthMs = GetLength(sheet);
            var numberOfSamples = Sample(lengthMs);

            var intBuffer = new int[numberOfSamples];

            foreach (var track in sheet.Tokens)
            {
                foreach (var token in track)
                {
                    Add(token, intBuffer);
                }
            }

            return ToShort(numberOfSamples, intBuffer);
        }

        private static long Sample(long millisecond)
        {
            return (long) (millisecond/1000.0*SampleRate);
        }

        private void Add(Token token, int[] buffer)
        {
            var audioToken = _convertor.Convert(token);

            var startIndex = Sample((long) token.AbsoluteTime.TotalMilliseconds);
            var deltaIndex = Sample((long) audioToken.Duration.TotalMilliseconds);

            for (var index = startIndex; index < startIndex + deltaIndex && index < buffer.Length; index++)
            {
                buffer[index] = buffer[index] + ToAmplitute(index, audioToken.Frequency, 0.05);
            }
        }

        private long GetLength(Sheet sheet)
        {
            var lastToken = sheet.Tokens
                .SelectMany(token => token)
                .OrderBy(token => token.AbsoluteTime)
                .ThenBy(token => (double) token.Length.Fraction)
                .Last();

            return (long) (_convertor.Convert(lastToken).Duration.TotalMilliseconds
                           + lastToken.AbsoluteTime.TotalMilliseconds);
        }

        private short ToAmplitute(long sample, double frequency, double decay)
        {
            return (short) (Math.Sin(sample*2*Math.PI*frequency/SampleRate)*_sixteenBitSampleLimit*decay);
        }

        private short[] ToShort(long numberOfSamples, int[] intBuffer)
        {
            var shortBuffer = new short[numberOfSamples];

            for (var index = 0; index < numberOfSamples; index++)
            {
                shortBuffer[index] = (short)
                    (intBuffer[index] > _sixteenBitSampleLimit
                        ? _sixteenBitSampleLimit
                        : intBuffer[index] < -_sixteenBitSampleLimit
                            ? -_sixteenBitSampleLimit
                            : intBuffer[index]);
            }

            return shortBuffer;
        }
    }
}