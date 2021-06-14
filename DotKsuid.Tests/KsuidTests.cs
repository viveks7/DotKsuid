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
            Assert.Equal(expected.ToString(), actual.ToString());
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
            Assert.Equal(expectedKsuid.ToString(), actualKsuid.ToString());
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
    }
}
