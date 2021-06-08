using Xunit;

namespace DotKsuid.Tests
{
    public class KsuidTests
    {

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
        public void ToBytes_GetBytesOfKsuid()
        {
            // arrange
            var ksuid = Ksuid.NewKsuid();

            // actual
            var actual = ksuid.ToBytes();

            // assert
            Assert.NotEmpty(actual);
            Assert.Equal(20, actual.Length);
        }

        [Fact]
        public void MaxKsuid_GetMaximumValueKsuid()
        {
            // arrange
            var expected = "aWgEPTl1tmebfsQzFP4bxwgy80V";

            // actual
            var actual = Ksuid.MaxKsuid.ToString();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MinKsuid_GetMaximumValueKsuid()
        {
            // arrange
            var expected = "000000000000000000000000000";

            // actual
            var actual = Ksuid.MinKsuid.ToString();

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
