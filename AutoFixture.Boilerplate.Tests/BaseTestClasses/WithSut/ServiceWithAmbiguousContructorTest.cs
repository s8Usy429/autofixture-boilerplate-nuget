using AutoFixture.Boilerplate.Tests.DummyServices;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTestClasses.WithSut
{
    public class ServiceWithAmbiguousContructorTest : AutoMoqTest<ServiceWithAmbiguousContructor>
    {
        protected override void CustomizeFixture(IFixture fixture)
        {
            fixture.Customize<ServiceWithAmbiguousContructor>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
        }

        [Theory, AutoData]
        public void AutoMoqTest_ServiceWithAmbiguousContructor(string prefix, string demo2)
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
