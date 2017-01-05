using System;
using System.IO;
using System.Text;

namespace cli
{
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