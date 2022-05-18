using AutoFixture.Boilerplate.Tests.Services;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTest
{
    public class ServiceWithDependenciesTest : BaseTest<ServiceWithDependencies>
    {
        [Theory, AutoData]
        public void Test1(int offset, int sum1, int sum2, int sum3)
        {
            // Arrange
            Mock<IDependency1>().Setup(s => s.GetSum1()).Returns(sum1);
            Mock<IDependency2>().Setup(s => s.GetSum2()).Returns(sum2);
            Mock<IDependency3>().Setup(s => s.GetSum3()).Returns(sum3);

            // Act
            int sum = Sut.Sum(offset);

            // Assert
            sum.Should().Be(offset + sum1 + sum2 + sum3);
        }
    }
}