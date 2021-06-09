using System;
using System.Linq;

namespace DotKsuid
{
    public class Ksuid
    {
        private const long EpochStamp = 1400000000;
        private const int timestampLengthInBytes = 4;
        private const int payloadLengthInBytes = 16;
        private const int byteLength = timestampLengthInBytes + payloadLengthInBytes;
        private const int stringEncodedLength = 27;
        private const char Padding = '0';
        private readonly byte[] _ksuid;

        private Ksuid()
        {
            var payload = new byte[payloadLengthInBytes];
            ThreadSafeRandom.NextBytes(payload);

            var timestamp = Convert.ToUInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - EpochStamp);
            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            _ksuid = new byte[byteLength];
            Buffer.BlockCopy(timestampBytes, 0, _ksuid, 0, timestampLengthInBytes);
            Buffer.BlockCopy(payload, 0, _ksuid, timestampBytes.Length, payloadLengthInBytes);
        }

        private Ksuid(byte[] bytes)
        {
            _ksuid = new byte[byteLength];
            Buffer.BlockCopy(bytes, 0, _ksuid, 0, bytes.Length);
        }

        public static Ksuid MaxKsuid => new Ksuid(Enumerable.Repeat<byte>(255, byteLength).ToArray());

        public static Ksuid MinKsuid => new Ksuid(Enumerable.Repeat<byte>(0, byteLength).ToArray());

        public static Ksuid NewKsuid()
        {
            return new Ksuid();
        }

        public override string ToString()
        {
            return _ksuid.ToBase62()
                    .PadLeft(stringEncodedLength, Padding);
        }

        public byte[] ToBytes()
        {
            return _ksuid;
        }
    }
}
