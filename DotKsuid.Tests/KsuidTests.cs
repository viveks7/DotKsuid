using System;
using Xunit;

namespace DotKsuid.Tests
{
    public class KsuidTests
    {
        [Fact]
        public void NewKsuid_ShouldReturnKsuid()
        {
            // arrange & actual
            var actual = Ksuid.NewKsuid();

            // assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void MaxKsuid_GetMaximumValueKsuid()
        {
            // arrange
            var expected = "aWgEPTl1tmebfsQzFP4bxwgy80V";

            // actual
            var actual = Ksuid.MaxKsuid().ToString();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MinKsuid_GetMinimumValueKsuid()
        {
            // arrange
            var expected = "000000000000000000000000000";

            // actual
            var actual = Ksuid.MinKsuid().ToString();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Parse_ShouldParseValidByteArrayToKsuid()
        {
            // arrange
            var expected = Ksuid.NewKsuid();
            var bytes = expected.ToByteArray();

            // actual
            var actual = Ksuid.Parse(bytes);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Parse_ShouldThrowExceptionForInvalidByteArray()
        {
            // arrange
            var bytes = new byte[40];

            // actual & assert
            Assert.Throws<ArgumentException>(() => Ksuid.Parse(bytes));
        }

        [Fact]
        public void Parse_ShouldThrowExceptionForNullByteArray()
        {
            // arrange
            byte[] bytes = null;

            // actual & assert
            Assert.Throws<ArgumentNullException>(() => Ksuid.Parse(bytes));
        }

        [Fact]
        public void Parse_ShouldParseStringToKsuid()
        {
            // arrange
            var ksuid = Ksuid.NewKsuid();
            var expected = ksuid.ToString();

            // actual
            var actual = Ksuid.Parse(expected).ToString();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Parse_ShouldThrowExceptionForInvalidString()
        {
            // arrange
            string value = "dsfdfdsfwe";

            // actual & assert
            Assert.Throws<ArgumentException>(() => Ksuid.Parse(value));
        }

        [Fact]
        public void Parse_ShouldThrowExceptionForNullString()
        {
            // arrange
            string value = null;

            // actual & assert
            Assert.Throws<ArgumentNullException>(() => Ksuid.Parse(value));
        }

        [Fact]
        public void TryParse_ShouldReturnTrueForValidString()
        {
            // arrange
            var expectedKsuid = Ksuid.NewKsuid();
            string value = expectedKsuid.ToString();

            // actual
            var actual = Ksuid.TryParse(value, out Ksuid actualKsuid);

            // assert
            Assert.True(actual);
            Assert.NotNull(actualKsuid);
            Assert.Equal(expectedKsuid, actualKsuid);
        }

        [Fact]
        public void TryParse_ShouldReturnFalseForNullString()
        {
            // arrange
            string value = null;

            // actual
            var actual = Ksuid.TryParse(value, out var ksuid);

            // assert
            Assert.False(actual);
            Assert.Null(ksuid);
        }


        [Fact]
        public void TryParse_ShouldReturnFalseForInvalidString()
        {
            // arrange
            string value = "dsfdfdsfwe";

            // actual
            var actual = Ksuid.TryParse(value, out var ksuid);

            // assert
            Assert.False(actual);
            Assert.Null(ksuid);
        }

        [Fact]
        public void ToString_GetStringValueOfKsuid()
        {
            // arrange
            var ksuid = Ksuid.NewKsuid();

            // actual
            var actual = ksuid.ToString();

            // assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void ToByteArray_GetBytesOfKsuid()
        {
            // arrange
            var ksuid = Ksuid.NewKsuid();

            // actual
            var actual = ksuid.ToByteArray();

            // assert
            Assert.NotEmpty(actual);
            Assert.Equal(20, actual.Length);
        }

        [Fact]
        public void Equals_ShouldReturnTrueIfEqual()
        {
            // arrange
            var ksuid1 = Ksuid.NewKsuid();
            var ksuid2 = Ksuid.Parse(ksuid1.ToByteArray());

            // actual
            var actual = ksuid1.Equals(ksuid2);

            // assert
            Assert.True(actual);
        }

        [Fact]
        public void Equals_ShouldReturnFalseIfNotEqual()
        {
            // arrange
            var ksuid1 = Ksuid.NewKsuid();
            var ksuid2 = Ksuid.NewKsuid();

            // actual
            var actual = ksuid1.Equals(ksuid2);

            // assert
            Assert.False(actual);
        }

        [Fact]
        public void EqualsOverride_ShouldReturnTrueIfEqual()
        {
            // arrange
            object ksuid1 = Ksuid.NewKsuid();
            object ksuid2 = Ksuid.Parse((ksuid1 as Ksuid).ToByteArray());

            // actual
            var actual = ksuid1.Equals(ksuid2);

            // assert
            Assert.True(actual);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashcodeIfEqual()
        {
            // arrange
            var ksuid1 = Ksuid.NewKsuid();
            var ksuid2 = Ksuid.Parse(ksuid1.ToByteArray());

            // actual
            var ksuid1hashCode = ksuid1.GetHashCode();
            var ksuid2hashCode = ksuid2.GetHashCode();

            // assert
            Assert.Equal(ksuid1hashCode, ksuid2hashCode);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashcodeIfNotEqual()
        {
            // arrange
            var ksuid1 = Ksuid.NewKsuid();
            var ksuid2 = Ksuid.NewKsuid();

            // actual
            var ksuid1hashCode = ksuid1.GetHashCode();
            var ksuid2hashCode = ksuid2.GetHashCode();

            // assert
            Assert.NotEqual(ksuid1hashCode, ksuid2hashCode);
        }
    }
}
