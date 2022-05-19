using AutoFixture.AutoMoq;
using System;

namespace AutoFixture.Boilerplate
{
    public abstract class AutoMoqTest<TSut> : AutoFixtureTest
    {
        private readonly Lazy<TSut> _lazySut;

        protected TSut Sut => _lazySut.Value;

        protected AutoMoqTest() : base(fixture => fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true }))
        {
            _lazySut = new Lazy<TSut>(() => Fixture.Create<TSut>());
        }
    }
}