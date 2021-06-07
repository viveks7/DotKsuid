﻿using System;

namespace DotKsuid
{
    static class Base62
    {
        public static readonly char[] Base62Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        public const uint BaseValue = 62;
        public const ulong MaxUIntCount = 4294967296;


        public static string ToBase62(this byte[] src)
        {
            var converted = FastEncodeBase62(src);
            return string.Create(converted.Length, converted,
                (buffer, base62Array) =>
            {
                var encode62Chars = Base62Characters;
                for (int i=base62Array.Length-1;  i>=0; i--)
                {
                    buffer[i] = encode62Chars[base62Array[i]];
                }
            });
        }

        private static byte[] FastEncodeBase62(byte[] src)
        {
            var dest = new byte[27];
            var parts = new uint[5]
            {
                ((uint)src[0]) << 24 | ((uint)src[1]) << 16 | ((uint)src[2]) << 8 | src[3],
                ((uint)src[4]) << 24 | ((uint)src[5]) << 16 | ((uint)src[6]) << 8 | src[7],
                ((uint)src[8]) << 24 | ((uint)src[9]) << 16 | ((uint)src[10]) << 8 | src[11],
                ((uint)src[12]) << 24 | ((uint)src[13]) << 16 | ((uint)src[14]) << 8 | src[15],
                ((uint)src[16]) << 24 | ((uint)src[17]) << 16 | ((uint)src[18]) << 8 | src[19],
            };
            var destLength = dest.Length;
            Span<uint> quotient = stackalloc uint[5];
            while (parts.Length > 0)
            {
                quotient.Clear();
                ulong remainder = 0;
                int counter = 0;
                foreach (var part in parts)
                {
                    ulong accumulator = part + remainder * MaxUIntCount;
                    var digit = accumulator / BaseValue;
                    remainder = accumulator % BaseValue;
                    if (counter != 0 || digit != 0)
                    {
                        quotient[counter] = (uint)digit;
                        counter++;
                    }
                }
                destLength--;
                dest[destLength] = (byte)remainder;
                parts = quotient.Slice(0, counter).ToArray();
            }
            return dest;
        }

    }
}
