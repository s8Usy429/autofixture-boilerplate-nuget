using Moq;
using System;

namespace AutoFixture.Boilerplate
{
    public abstract class AutoFixtureTest
    {
        private readonly Lazy<IFixture> _lazyFixture;

        protected IFixture Fixture => _lazyFixture.Value;

        protected Mock<T> Mock<T>() where T : class
        {
            return Fixture.Freeze<Mock<T>>();
        }

        protected AutoFixtureTest() : this(_ => { })
        {
        }

        protected AutoFixtureTest(Action<IFixture> mandatoryFixtureCustomization)
        {
            _lazyFixture = new Lazy<IFixture>(() =>
            {
                var fixture = new Fixture();
                mandatoryFixtureCustomization(fixture);
                CustomizeFixture(fixture);
                return fixture;
            });
        }

        protected virtual void CustomizeFixture(IFixture fixture) { }
    }
}