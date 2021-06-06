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
    }
}
