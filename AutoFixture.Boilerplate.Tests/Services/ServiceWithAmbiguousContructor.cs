namespace AutoFixture.Boilerplate.Tests.Services
{
    public class ServiceWithAmbiguousContructor
    {
        private readonly IDependency1 _dependency1;
        private readonly IDependency2 _dependency2;
        private readonly IDependency3 _dependency3;

        public ServiceWithAmbiguousContructor()
        {
            _dependency1 = new Dependency1();
            _dependency2 = new Dependency2();
            _dependency3 = new Dependency3();
        }

        public ServiceWithAmbiguousContructor(IDependency1 dependency1, IDependency2 dependency2, IDependency3 dependency3)
        {
            _dependency1 = dependency1;
            _dependency2 = dependency2;
            _dependency3 = dependency3;
        }

        public int Sum(int offset)
        {
            return offset + _dependency1.GetSum1() + _dependency2.GetSum2() + _dependency3.GetSum3();
        }
    }
}
