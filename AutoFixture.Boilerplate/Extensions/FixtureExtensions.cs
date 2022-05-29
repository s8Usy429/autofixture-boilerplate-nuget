using Moq;

namespace AutoFixture.Boilerplate
{
    public static class FixtureExtensions
    {
        public static Mock<T> FreezeMock<T>(this IFixture fixture) where T : class
        {
            return fixture.Freeze<Mock<T>>();
        }
    }
}