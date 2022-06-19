using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using Moq;
using System;
using System.Collections.Generic;

namespace AutoFixture.Boilerplate
{
    public abstract class AutoMoqTest : Fixture
    {
        protected AutoMoqTest()
        {
            Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        protected Mock<T> Mock<T>() where T : class => Freeze<Mock<T>>();

        #region SpecimenFactory

        public T Create<T>() => SpecimenFactory.Create<T>(this);

        public IEnumerable<T> CreateMany<T>() => SpecimenFactory.CreateMany<T>(this);

        public IEnumerable<T> CreateMany<T>(int count) => SpecimenFactory.CreateMany<T>(this, count);

        #endregion

        #region FixtureFreezer

        public T Freeze<T>() => FixtureFreezer.Freeze<T>(this);

        public T Freeze<T>(Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation) => FixtureFreezer.Freeze<T>(this, composerTransformation);

        #endregion
    }

    public abstract class AutoMoqTest<TSut> : AutoMoqTest
    {
        protected TSut Sut => Freeze<TSut>();
    }
}