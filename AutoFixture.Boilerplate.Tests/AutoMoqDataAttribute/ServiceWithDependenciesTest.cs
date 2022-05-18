using AutoFixture.Boilerplate;
using AutoFixture.Boilerplate.Tests.DummyServices;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AutoMoqDataAttributeTests
{
    public class ServiceWithDependenciesTest
    {
        [Theory, AutoMoqData]
        public void Test1(
            int offset,
            int sum1,
            int sum2,
            int sum3,
            [Frozen] Mock<IDependency1> mockDependency1,
            [Frozen] Mock<IDependency2> mockDependency2,
            [Frozen] Mock<IDependency3> mockDependency3,
            ServiceWithDependencies sut)
        {
            // Arrange
            mockDependency1.Setup(s => s.GetSum1()).Returns(sum1);
            mockDependency2.Setup(s => s.GetSum2()).Returns(sum2);
            mockDependency3.Setup(s => s.GetSum3()).Returns(sum3);

            // Act
            int sum = sut.Sum(offset);

            // Assert
            sum.Should().Be(offset + sum1 + sum2 + sum3);
        }
    }
}