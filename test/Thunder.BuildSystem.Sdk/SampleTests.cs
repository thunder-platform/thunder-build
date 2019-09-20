using FluentAssertions;
using Xunit;

namespace Thunder.BuildSystem.Sdk.Tests
{
    public class SampleTests
    {
        [Fact]
        public void SampleTests_ShouldBeOk()
        {
            "ok".Should().Be("ok");
        }
    }
}
