﻿using AutoFixture.Xunit2;

namespace AutoFixture.Boilerplate
{
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values) : base(new AutoMoqDataAttribute(), values)
        {

        }
    }
}
