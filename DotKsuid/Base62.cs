using System;
using System.Collections.Generic;
using System.Text;

namespace DotKsuid
{
    public static class Base62
    {
        public const string Base62Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public const string ZeroString = "000000000000000000000000000";

        public const int OffsetUppercase = 10;

        public const int OffsetLowercase = 36;

        public const uint base62 = 62;
        public const ulong maxUInt = 4294967296;

        public static byte[] FastEncodeBase62(byte[] src)
        {
            var dest = new byte[27];
            var parts = new uint[5]
            {
                ((uint)src[0]) << 24 | ((uint)src[1]) << 16 | ((uint)src[2]) << 8 | (uint)src[3],
                ((uint)src[4]) << 24 | ((uint)src[5]) << 16 | ((uint)src[6]) << 8 | (uint)src[7],
                ((uint)src[8]) << 24 | ((uint)src[9]) << 16 | ((uint)src[10]) << 8 | (uint)src[11],
                ((uint)src[12]) << 24 | ((uint)src[13]) << 16 | ((uint)src[14]) << 8 | (uint)src[15],
                ((uint)src[16]) << 24 | ((uint)src[17]) << 16 | ((uint)src[18]) << 8 | (uint)src[19],
            };
            var destLength = dest.Length;
            while(parts.Length > 0)
            {
                var quotient = new List<uint>();
                ulong remainder = 0;
                foreach (var part in parts)
                {
                    ulong accumulator = part + remainder * maxUInt;
                    var digit = accumulator / base62;
                    remainder = accumulator % base62;
                    if (quotient.Count > 0 || digit > 0)
                    {
                        quotient.Add((uint)digit);
                    }
                }
                destLength--;
                dest[destLength] = (byte)remainder;
                parts = quotient.ToArray();
            }
            return dest;
        }

        public static string ToBase62(this byte[] src)
        {
            var converted = FastEncodeBase62(src);
            var builder = new StringBuilder();
            foreach (var t in converted)
            {
                builder.Append(Base62Characters[t]);
            }
            return builder.ToString();
        }
    }
}
