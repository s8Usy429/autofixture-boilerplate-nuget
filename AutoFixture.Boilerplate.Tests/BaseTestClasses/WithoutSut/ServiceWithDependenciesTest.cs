using AutoFixture.Boilerplate.Tests.DummyServices;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTestClasses.WithoutSut
{
    public class ServiceWithDependenciesTest : AutoMoqTest
    {
        [Fact]
        public void AutoMoqTest_ServiceWithDependencies()
        {
            // Arrange
            string prefix = Create<string>();
            string demo2 = Create<string>();
            Mock<IDependency2>()
                .Setup(s => s.GetString())
                .Returns(demo2);
            var sut = Create<ServiceWithDependencies>();

            // Act
            var response = sut.Concat(prefix);

            // Assert
            response.Should().Contain(demo2);
        }
    }
}
