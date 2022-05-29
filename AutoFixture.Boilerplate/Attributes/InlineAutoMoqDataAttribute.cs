using AutoFixture.Xunit2;
using System;

namespace AutoFixture.Boilerplate.Attributes
{
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values) : base(new AutoMoqDataAttribute(), values)
        {
        }

        public InlineAutoMoqDataAttribute(Action<IFixture> fixtureCustomizations, params object[] values) : base(new AutoMoqDataAttribute(fixtureCustomizations), values)
        {
        }
    }
}
