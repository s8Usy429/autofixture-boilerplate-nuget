using AutoFixture.Boilerplate.Attributes;
using AutoFixture.Boilerplate.Tests.DummyServices;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.Attributes
{
    public class ServiceWithAmbiguousContructorTest
    {
        [Theory, AutoMoqData]
        public void AutoMoqDataAttribute_ServiceWithAmbiguousContructor(
            string prefix,
            string demo2,
            [Frozen] Mock<IDependency2> mockDependency2,
            [Greedy] ServiceWithAmbiguousContructor sut)
        {
            mockDependency2
                .Setup(s => s.GetString())
                .Returns(demo2);

            var response = sut.Concat(prefix);

            response.Should().Contain(demo2);
        }

        [Theory]
        [InlineAutoMoqData("fixed")]
        public void InlineAutoMoqDataAttribute_ServiceWithAmbiguousContructor(
            string demo2,
            string prefix,
            [Frozen] Mock<IDependency2> mockDependency2,
            [Greedy] ServiceWithAmbiguousContructor sut)
        {
            mockDependency2
                .Setup(s => s.GetString())
                .Returns(demo2);

            var response = sut.Concat(prefix);

            response.Should().Contain("fixed");
        }
    }
}
