﻿using System;

namespace DotKsuid
{
    public class Ksuid
    {
        private const long EpochStamp = 1400000000;
        private const int timestampLengthInBytes = 4;
        private const int payloadLengthInBytes = 16;
        private const int byteLength = timestampLengthInBytes + payloadLengthInBytes;
        private const int stringEncodedLength = 27;
        private const string minStringEncoded = "000000000000000000000000000";
        private const string maxStringEncoded = "aWgEPTl1tmebfsQzFP4bxwgy80V";
        private const char padding = '0';
        private readonly byte[] _ksuid;

        public Ksuid()
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
            Buffer.BlockCopy(timestampBytes, 0, _ksuid, 0, timestampBytes.Length);
            Buffer.BlockCopy(payload, 0, _ksuid, timestampBytes.Length, payloadLengthInBytes);
        }

        public static Ksuid NewKsuid()
        {
            return new Ksuid();
        }

        public override string ToString()
        {
            return _ksuid.ToBase62(stringEncodedLength)
                    .PadLeft(stringEncodedLength, padding);
        }

        public byte[] ToBytes()
        {
            return _ksuid;
        }
    }
}
