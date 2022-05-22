using AutoFixture.AutoMoq;
using System;

namespace AutoFixture.Boilerplate
{
    public abstract class AutoMoqTest : AutoFixtureTest
    {
        protected AutoMoqTest() : base(fixture => fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true }))
        {
        }
    }

    public abstract class AutoMoqTest<TSut> : AutoFixtureTest<TSut>
    {
        protected AutoMoqTest() : base(fixture => fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true }))
        {
        }
    }
}