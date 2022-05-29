using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using System;

namespace AutoFixture.Boilerplate
{
    public abstract class AutoMoqTest
    {
        private readonly Lazy<IFixture> _lazyFixture;

        protected IFixture Fixture => _lazyFixture.Value;

        protected AutoMoqTest() : this(_ => { })
        {
        }

        protected AutoMoqTest(Action<IFixture> fixtureCustomizations)
        {
            _lazyFixture = new Lazy<IFixture>(() =>
            {
                var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
                fixtureCustomizations(fixture);
                return fixture;
            });
        }
    }

    public abstract class AutoMoqTest<TSut> : AutoMoqTest
    {
        private readonly Lazy<TSut> _lazySut;

        protected TSut Sut => _lazySut.Value;

        protected AutoMoqTest() : this(_ => { })
        {
        }

        protected AutoMoqTest(Action<IFixture> fixtureCustomizations) : base(fixtureCustomizations)
        {
            _lazySut = new Lazy<TSut>(() => Fixture.Create<TSut>());
        }
    }
}