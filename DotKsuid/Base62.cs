using System;

namespace DotKsuid
{
    public static class Base62
    {
        public const string Base62Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public const string ZeroString = "000000000000000000000000000";

        public const int OffsetUppercase = 10;

        public const int OffsetLowercase = 36;

        public const int base62 = 62;
        public const long maxUInt = 4294967296;

        public static void FastEncodeBase62(byte[] src, byte[] dest)
        {
            uint[] parts = new uint[]
            {
                ((uint)src[0]) << 24 | ((uint)src[1]) << 16 | ((uint)src[2]) << 8 | (uint)src[3],
                ((uint)src[4]) << 24 | ((uint)src[5]) << 16 | ((uint)src[6]) << 8 | (uint)src[7],
                ((uint)src[8]) << 24 | ((uint)src[9]) << 16 | ((uint)src[10]) << 8 | (uint)src[11],
                ((uint)src[12]) << 24 | ((uint)src[13]) << 16 | ((uint)src[14]) << 8 | (uint)src[15],
                ((uint)src[16]) << 24 | ((uint)src[17]) << 16 | ((uint)src[18]) << 8 | (uint)src[19],
            };
        }
    }
}
