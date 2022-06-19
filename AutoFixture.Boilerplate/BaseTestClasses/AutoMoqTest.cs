using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using System;

namespace AutoFixture.Boilerplate
{
    public abstract class AutoMoqTest
    {
        private readonly Lazy<IFixture> _lazyFixture;

        protected IFixture Fixture => _lazyFixture.Value;

        protected AutoMoqTest()
        {
            _lazyFixture = new Lazy<IFixture>(() =>
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
                CustomizeFixture(fixture);
                return fixture;
            });
        }

        protected virtual void CustomizeFixture(IFixture fixture) { }
    }

    public abstract class AutoMoqTest<TSut> : AutoMoqTest
    {
        private readonly Lazy<TSut> _lazySut;

        protected TSut Sut => _lazySut.Value;

        protected AutoMoqTest()
        {
            _lazySut = new Lazy<TSut>(() => Fixture.Create<TSut>());
        }
    }
}