# autofixture-boilerplate-nuget
A set of convenient base classes and attributes to work with AutoMoq.

## Content
| Class                      | Description                                                     |
|:--------------------------:|-----------------------------------------------------------------|
| AutoFixtureTest            | Base class (abstract) exposing a Fixture                        |
| AutoMoqTest                | Base class (abstract) exposing a Fixture and setting up AutoMoq |
| AutoMoqDataAttribute       | An attribute expending AutoData to use AutoMoq                  |
| InlineAutoMoqDataAttribute | An attribute expending InlineAutoData to use AutoMoq            |

## Usage
Look at real test samples here : <a href="https://github.com/s8Usy429/autofixture-boilerplate-nuget/tree/main/AutoFixture.Boilerplate.Tests">Samples</a>

### AutoMoqTest
You can inherit all your test classes from AutoMoqTest.
```cs
public class TheSutTests : AutoMoqTest<TheTypeOfTheSut>
{
	...
	// Use the Fixture property exposed from the base class
	var param1 = Fixture.Create<string>();

	// Use the Sut property exposed from the base class
	Sut.MethodUnderTest(param1);

	// Use the Mock<T> method exposed from the base class (short for Fixture.Freeze<Mock<T>>())
	Mock<TypeOfDependency>().Verify(s => s.DependentMethon());
	...
}
```

### AutoFixtureTest
You can inherit all your test classes from AutoFixtureTest.
```cs
public class TheSutTests : AutoFixtureTest
{
	...
	// Use the Fixture property exposed from the base class
	var param1 = Fixture.Create<string>();
	
	// Use the Mock<T> method exposed from the base class (short for Fixture.Freeze<Mock<T>>())
	var mockDependency1 = Mock<TypeOfDependency1>();
	...
	var mockDependencyN = Mock<TypeOfDependencyN>();
	
	// Create the SUT and pass all the mocks
	var sut = new TheTypeOfTheSut(mockDependency1.Object, ..., mockDependencyN.Object);

	sut.MethodUnderTest(param1);
	
	mockDependency.Verify(s => s.DependentMethon());
	...
}
```

### AutoMoqDataAttribute
AutoMoqData extends AutoData to automatically create mocks (AutoMoq).
You can then acces those mocks supplying them as parameters.
```cs
public class TheSutTests
{
        [Theory, AutoMoqData]
        public void Test1(
            int a,
            [Frozen] Mock<TypeOfDependency1> mockDependency1,
            [Frozen] Mock<TypeOfDependencyN> mockDependencyN,
            TheTypeOfTheSut sut)
	{
		...
	}
}
```

### InlineAutoMoqDataAttribute
InlineAutoMoqData extends InlineAutoData to automatically create mocks (AutoMoq).
You can then acces those mocks supplying them as parameters.
```cs
public class TheSutTests
{
        [Theory]
        [InlineAutoMoqData(1, 2)]
        [InlineAutoMoqData(3, 4)]
        public void Test1(
            int a,
            int b,
            [Frozen] Mock<TypeOfDependency1> mockDependency1,
            [Frozen] Mock<TypeOfDependencyN> mockDependencyN,
            TheTypeOfTheSut sut)
	{
		...
	}
}
```

### Customize the Fixture
#### Using AutoMoqTest or AutoFixtureTest
It is possible to customize the exposed Fixture by overriding the CustomizeFixture method.
```cs
public class TheSutTests : AutoMoqTest
{
	protected override void CustomizeFixture(IFixture fixture)
	{
		fixture.Customize<TheTypeToCustomize>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
	}
}
```
This will apply to all the tests in the class.
If you don't want this behavior, your only choice is to use the Fixture property directly inside your tests.
```cs
public class TheSutTests : AutoMoqTest
{
	[Fact]
	public void Test1()
	{
		...
		Fixture.Customize<TheTypeToCustomize>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
		...
	}
}
```

#### Using AutoMoqData or InlineAutoMoqData
You may be able to customize the fixture using existing xUnit attributes such as the GreedyAttribute.
```cs
public class TheSutTests
{
        [Theory, AutoMoqData]
        public void Test1(
            int a,
            [Greedy] TheTypeOfTheSut sut)
	{
		...
	}
}
```
Otherwise, you can inherit the attributes and customize the fixture.
```cs
public class TheSutTests
{
	private class CustomizedAutoMoqDataAttribute : AutoMoqDataAttribute
	{
		public CustomizedAutoMoqDataAttribute() : base(fixture => fixture.Customize<TheTypeToCustomize>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery()))))
		{
		}
	}

	private class CustomizedInlineAutoMoqDataAttribute : InlineAutoMoqDataAttribute
	{
		public CustomizedInlineAutoMoqDataAttribute(params object[] values) : base(new CustomizedAutoMoqDataAttribute(), values)
		{
		}
	}
}
```