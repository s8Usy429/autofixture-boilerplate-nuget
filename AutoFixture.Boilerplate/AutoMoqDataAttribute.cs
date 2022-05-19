using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;

namespace AutoFixture.Boilerplate
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : this(_ => { })
        {

        }

        protected AutoMoqDataAttribute(Action<IFixture> customization) : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            customization(fixture);
            return fixture;
        })
        {

        }
    }
}
