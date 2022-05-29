using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;

namespace AutoFixture.Boilerplate.Attributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : this(_ => { })
        {
        }

        public AutoMoqDataAttribute(Action<IFixture> fixtureCustomizations) : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            fixtureCustomizations(fixture);
            return fixture;
        })
        {
        }
    }
}
