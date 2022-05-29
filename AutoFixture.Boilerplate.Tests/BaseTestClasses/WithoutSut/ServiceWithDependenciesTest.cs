using AutoFixture.Boilerplate.Tests.DummyServices;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTestClasses.WithoutSut
{
    public class ServiceWithDependenciesTest : AutoMoqTest
    {
        [Theory, AutoData]
        public void AutoMoqTest_ServiceWithDependencies(string prefix, string demo2)
        {
            Fixture.FreezeMock<IDependency2>()
                .Setup(s => s.GetString())
                .Returns(demo2);
            
            var response = Fixture.Create<ServiceWithDependencies>().Concat(prefix);

            response.Should().Contain(demo2);
        }
    }
}
