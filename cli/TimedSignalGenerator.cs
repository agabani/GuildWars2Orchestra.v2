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
            var length = Sample(GetLength(sheet));

            var intBuffer = new int[length];

            foreach (var track in sheet.Tokens)
            {
                foreach (var token in track)
                {
                    Add(token, intBuffer);
                }
            }

            var shortBuffer = new short[length];

            for (var index = 0; index < length; index++)
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

        private void Add(Token token, int[] buffer)
        {
            var audioToken = _convertor.Convert(token);

            var startIndex = Sample(token.AbsoluteTimeMs);
            var deltaIndex = Sample((long) audioToken.Duration.TotalMilliseconds);

            for (var index = startIndex; index < startIndex + deltaIndex; index++)
            {
                buffer[index] = buffer[index] + ToAmplitute(index, audioToken.Frequency, 0.1);
            }
        }

        private static long Sample(long millisecond)
        {
            return (long) (millisecond/1000.0*SampleRate);
        }

        private long GetLength(Sheet sheet)
        {
            var lastToken = sheet.Tokens
                .SelectMany(token => token)
                .OrderBy(token => token.AbsoluteTimeMs)
                .ThenBy(token => (double) token.Length.Fraction)
                .Last();

            return (long) (_convertor.Convert(lastToken).Duration.TotalMilliseconds
                           + lastToken.AbsoluteTimeMs);
        }

        private short ToAmplitute(long sample, double frequency, double decay)
        {
            return (short) (Math.Sin(sample*2*Math.PI*frequency/SampleRate)*_sixteenBitSampleLimit*decay);
        }
    }
}