using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotKsuid
{
    public sealed class Ksuid : IEquatable<Ksuid>
    {
        private const long EpochStamp = 1400000000;
        private const int TimestampLengthInBytes = 4;
        private const int PayloadLengthInBytes = 16;
        private const int KsuidBytesLength = TimestampLengthInBytes + PayloadLengthInBytes;
        private const int KsuidStringEncodedLength = 27;
        private const char ZeroPadding = '0';
        private readonly byte[] _payload;
        private readonly uint _timestamp;

        public static Ksuid NewKsuid() => new Ksuid();

        public static Ksuid MaxKsuid() => new Ksuid(Enumerable.Repeat<byte>(255, KsuidBytesLength).ToArray());

        public static Ksuid MinKsuid() => new Ksuid(Enumerable.Repeat<byte>(0, KsuidBytesLength).ToArray());

        public static Ksuid Parse(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            if (bytes.Length != KsuidBytesLength)
            {
                throw new ArgumentException("Bytes array is of invalid length!");
            }

            return new Ksuid(bytes);
        }

        public static Ksuid Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (value.Length != KsuidStringEncodedLength)
            {
                throw new ArgumentException("Value is not a valid KSUID string!");
            }
            var bytes = value.FromBase62();
            return new Ksuid(bytes);
        }

        public static bool TryParse(string value, out Ksuid ksuid)
        {
            ksuid = null;
            if (value == null || value.Length != KsuidStringEncodedLength)
            {
                return false;
            }

            var bytes = value.FromBase62();
            ksuid = new Ksuid(bytes);
            return true;
        }

        private Ksuid()
        {
            _payload = new byte[PayloadLengthInBytes];
            ThreadSafeRandom.NextBytes(_payload);
            _timestamp = Convert.ToUInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - EpochStamp);
        }

        private Ksuid(byte[] bytes)
        {
            Span<byte> input = bytes;
            _payload = input.Slice(TimestampLengthInBytes, PayloadLengthInBytes).ToArray();
            var timestamp = input.Slice(0, TimestampLengthInBytes).ToArray();
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestamp);
            }
            _timestamp = BitConverter.ToUInt32(timestamp, 0);
        }

        public byte[] ToByteArray()
        {
            var timestampBytes = BitConverter.GetBytes(_timestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var ksuid = new byte[KsuidBytesLength];
            Buffer.BlockCopy(timestampBytes, 0, ksuid, 0, TimestampLengthInBytes);
            Buffer.BlockCopy(_payload, 0, ksuid, timestampBytes.Length, PayloadLengthInBytes);
            return ksuid;
        }

        public override string ToString()
        {
            return ToByteArray().ToBase62()
                    .PadLeft(KsuidStringEncodedLength, ZeroPadding);
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
            var arrayHash = ((IStructuralEquatable)_payload)
                .GetHashCode(EqualityComparer<byte>.Default);
            return arrayHash * 17 + _timestamp.GetHashCode();
        }
    }
}
