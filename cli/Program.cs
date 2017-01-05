using System;
using System.IO;
using System.Text;

namespace cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var signalGenerator = new SignalGenerator();
            var wavePacker = new WavePacker();

            var generateSamples = signalGenerator.GenerateSamples(1500, 400);
            var memoryStream = wavePacker.Pack(generateSamples);
            wavePacker.Write("text.wav", memoryStream);
        }
    }

    internal class SignalGenerator
    {
        private const double SampleRate = 44100;
        private readonly double _sixteenBitSampleLimit = Math.Pow(2, 16)/2 - 1;

        public short[] GenerateSamples(long milliseconds, double frequency)
        {
            var numberOfSamples = (long) (milliseconds/1000*SampleRate);

            var samples = new short[numberOfSamples];

            for (var i = 0; i < numberOfSamples; i++)
            {
                samples[i] = ToAmplitute(i, frequency);
            }

            return samples;
        }

        private short ToAmplitute(long sample, double frequency)
        {
            return (short) (Math.Sin(sample*2*Math.PI*frequency/SampleRate)*_sixteenBitSampleLimit*0.8);
        }
    }

    internal class WavePacker
    {
        public MemoryStream Pack(short[] d)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream, Encoding.ASCII);

            var dataLength = d.Length*2;

            // RIFF
            writer.Write(Encoding.ASCII.GetBytes("RIFF"));
            writer.Write(d.Length);
            writer.Write(Encoding.ASCII.GetBytes("WAVE"));

            writer.Write(Encoding.ASCII.GetBytes("fmt "));
            writer.Write(16);
            writer.Write(ToShortArray(1)); // PCM
            writer.Write(ToShortArray(1)); // mono
            writer.Write(44100); // sample rate
            writer.Write(44100*16/8); // byte rate
            writer.Write(ToShortArray(2)); // bytes per sample
            writer.Write(ToShortArray(16)); // bites per sample

            // data
            writer.Write(Encoding.ASCII.GetBytes("data"));
            writer.Write(dataLength);
            var data = new byte[dataLength];
            Buffer.BlockCopy(d, 0, data, 0, dataLength);
            writer.Write(data);

            return stream;
        }

        private static byte[] ToShortArray(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return new[] {bytes[0], bytes[1]};
        }

        public void Write(string fileName, MemoryStream memoryStream)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                memoryStream.WriteTo(fileStream);
            }
        }
    }
}