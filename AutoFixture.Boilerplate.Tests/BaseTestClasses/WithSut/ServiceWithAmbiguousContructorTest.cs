using AutoFixture.Boilerplate.Tests.DummyServices;
using AutoFixture.Kernel;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTestClasses.WithSut
{
    public class ServiceWithAmbiguousContructorTest : AutoMoqTest<ServiceWithAmbiguousContructor>
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

            // Act
            var response = Sut.Concat(prefix);

            // Assert
            response.Should().Contain(demo2);
        }
    }
}
