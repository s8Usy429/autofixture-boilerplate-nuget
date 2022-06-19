﻿using AutoFixture.Boilerplate.Tests.DummyServices;
using FluentAssertions;
using Xunit;

namespace AutoFixture.Boilerplate.Tests.BaseTestClasses.WithSut
{
    public class ServiceWithDependenciesTest : AutoMoqTest<ServiceWithDependencies>
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

            // Act
            var response = Sut.Concat(prefix);

            // Assert
            response.Should().Contain(demo2);
        }
    }
}
