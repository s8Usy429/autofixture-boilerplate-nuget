using AutoFixture.Boilerplate.Tests.DummyServices;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTestClasses.WithSut
{
    public class ServiceWithDependenciesTest : AutoMoqTest<ServiceWithDependencies>
    {
        [Theory, AutoData]
        public void AutoMoqTest_ServiceWithDependencies(string prefix, string demo2)
        {
            // Arrange
            Fixture.FreezeMock<IDependency2>()
                .Setup(s => s.GetString())
                .Returns(demo2);

            // Act
            var response = Sut.Concat(prefix);

            // Assert
            response.Should().Contain(demo2);
        }
    }
}
