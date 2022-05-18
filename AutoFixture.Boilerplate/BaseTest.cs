using AutoFixture.AutoMoq;
using Moq;
using System;

namespace AutoFixture.Boilerplate
{
    public abstract class BaseTest
    {
        private readonly Lazy<IFixture> _lazyFixture;

        protected IFixture Fixture => _lazyFixture.Value;

        protected Mock<T> Mock<T>() where T : class
        {
            return Fixture.Freeze<Mock<T>>();
        }

        protected BaseTest() : this(_ => { })
        {
        }

        protected BaseTest(Action<IFixture> mandatoryFixtureCustomization)
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

    public abstract class BaseTest<TSut> : BaseTest
    {
        private readonly Lazy<TSut> _lazySut;

        protected TSut Sut => _lazySut.Value;

        protected BaseTest() : base(fixture => fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true }))
        {
            _lazySut = new Lazy<TSut>(() => Fixture.Create<TSut>());
        }
    }
}