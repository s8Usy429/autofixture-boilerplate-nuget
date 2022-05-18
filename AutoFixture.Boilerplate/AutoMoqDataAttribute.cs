using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace AutoFixture.Boilerplate
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true }))
        {

        }
    }
}
