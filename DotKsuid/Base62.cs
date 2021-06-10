using System;

namespace DotKsuid
{
    static class Base62
    {
        public static readonly char[] Base62Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        public const uint BaseValue = 62;
        public const ulong MaxUIntCount = 4294967296;
        public const int OffsetUppercase = 10;
        public const int OffsetLowercase = 36;


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

        public static byte[] FromBase62(this string src)
        {
            return FastDecodeBase62(src);
        }

        private static byte[] FastEncodeBase62(byte[] src)
        {
            var dest = new byte[27];
            _ = src[19];
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

        private static byte[] FastDecodeBase62(ReadOnlySpan<char> src)
        {
            var dest = new byte[20];
            var parts = new uint[27]
            {
                ConvertToBase62Value(src[0]),
                ConvertToBase62Value(src[1]),
                ConvertToBase62Value(src[2]),
                ConvertToBase62Value(src[3]),
                ConvertToBase62Value(src[4]),
                ConvertToBase62Value(src[5]),
                ConvertToBase62Value(src[6]),
                ConvertToBase62Value(src[7]),
                ConvertToBase62Value(src[8]),
                ConvertToBase62Value(src[9]),

                ConvertToBase62Value(src[10]),
                ConvertToBase62Value(src[11]),
                ConvertToBase62Value(src[12]),
                ConvertToBase62Value(src[13]),
                ConvertToBase62Value(src[14]),
                ConvertToBase62Value(src[15]),
                ConvertToBase62Value(src[16]),
                ConvertToBase62Value(src[17]),
                ConvertToBase62Value(src[18]),
                ConvertToBase62Value(src[19]),

                ConvertToBase62Value(src[20]),
                ConvertToBase62Value(src[21]),
                ConvertToBase62Value(src[22]),
                ConvertToBase62Value(src[23]),
                ConvertToBase62Value(src[24]),
                ConvertToBase62Value(src[25]),
                ConvertToBase62Value(src[26]),
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
                    ulong accumulator = part + remainder * BaseValue;
                    var digit = accumulator / MaxUIntCount;
                    remainder = accumulator % MaxUIntCount;
                    if (counter != 0 || digit != 0)
                    {
                        quotient[counter] = (uint)digit;
                        counter++;
                    }
                }

                dest[destLength - 4] = (byte)(remainder >> 24);
                dest[destLength - 3] = (byte)(remainder >> 16);
                dest[destLength - 2] = (byte)(remainder >> 8);
                dest[destLength - 1] = (byte)remainder;
                destLength -= 4;

                parts = quotient.Slice(0, counter).ToArray();
            }
            return dest;
        }

        private static byte ConvertToBase62Value(char digit)
        {
            if (digit >= '0' && digit <= '9')
            {
                return (byte)(digit - '0');
            }
            else if (digit >= 'A' && digit <= 'Z')
            {
                return (byte)(OffsetUppercase + (digit - 'A'));
            }
            else
            {
                return (byte)(OffsetLowercase + (digit - 'a'));
            }
        }

    }
}
