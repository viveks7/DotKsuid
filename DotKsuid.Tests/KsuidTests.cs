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

            var customString = ksuid.ToBytes().ToBase62Custom();

            // assert
            Assert.NotNull(actual);
            Assert.Equal(actual, customString);
        }
    }
}
