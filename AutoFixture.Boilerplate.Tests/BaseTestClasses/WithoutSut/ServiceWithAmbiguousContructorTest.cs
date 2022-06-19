using AutoFixture.Boilerplate.Tests.DummyServices;
using AutoFixture.Kernel;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTestClasses.WithoutSut
{
    public class ServiceWithAmbiguousContructorTest : AutoMoqTest
    {
        public ServiceWithAmbiguousContructorTest()
        {
            Customize<ServiceWithAmbiguousContructor>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
        }

        [Fact]
        public void AutoMoqTest_ServiceWithAmbiguousContructor()
        {
            // Arrange
            string prefix = Create<string>();
            string demo2 = Create<string>();
            Mock<IDependency2>()
                .Setup(s => s.GetString())
                .Returns(demo2);
            var sut = Create<ServiceWithAmbiguousContructor>();

            // Act
            var response = sut.Concat(prefix);

            // Assert
            response.Should().Contain(demo2);
        }
    }
}
