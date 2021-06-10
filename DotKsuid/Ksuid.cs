using System;
using System.Collections.Generic;
using System.Linq;

namespace DotKsuid
{
    public class Ksuid : IEquatable<Ksuid>
    {
        private const long EpochStamp = 1400000000;
        private const int timestampLengthInBytes = 4;
        private const int payloadLengthInBytes = 16;
        private const int byteLength = timestampLengthInBytes + payloadLengthInBytes;
        private const int stringEncodedLength = 27;
        private const char ZeroPadding = '0';
        private readonly byte[] _payload;
        private readonly uint _timestamp;

        public static Ksuid NewKsuid() => new Ksuid();

        public static Ksuid MaxKsuid => new Ksuid(Enumerable.Repeat<byte>(255, byteLength).ToArray());

        public static Ksuid MinKsuid => new Ksuid(Enumerable.Repeat<byte>(0, byteLength).ToArray());

        public static Ksuid FromBytes(byte[] bytes) => new Ksuid(bytes);

        private Ksuid()
        {
            _payload = new byte[payloadLengthInBytes];
            ThreadSafeRandom.NextBytes(_payload);
            _timestamp = Convert.ToUInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - EpochStamp);
        }

        private Ksuid(byte[] bytes)
        {
            Span<byte> input = bytes;
            _payload = input.Slice(timestampLengthInBytes, payloadLengthInBytes).ToArray();
            var timestamp = input.Slice(0, timestampLengthInBytes).ToArray();
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestamp);
            }
            _timestamp = BitConverter.ToUInt32(timestamp, 0);
        }

        public byte[] ToBytes()
        {
            var timestampBytes = BitConverter.GetBytes(_timestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var ksuid = new byte[byteLength];
            Buffer.BlockCopy(timestampBytes, 0, ksuid, 0, timestampLengthInBytes);
            Buffer.BlockCopy(_payload, 0, ksuid, timestampBytes.Length, payloadLengthInBytes);
            return ksuid;
        }

        public override string ToString()
        {
            return ToBytes().ToBase62()
                    .PadLeft(stringEncodedLength, ZeroPadding);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Ksuid);
        }

        public bool Equals(Ksuid other)
        {
            return other != null &&
                   EqualityComparer<byte[]>.Default.Equals(_payload, other._payload) &&
                   _timestamp == other._timestamp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_payload, _timestamp);
        }
    }
}
