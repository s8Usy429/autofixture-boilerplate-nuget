# autofixture-boilerplate-nuget
A set of convenient base classes and attributes to work with AutoMoq.

## Content
| Class                      | Description                                                     |
|:--------------------------:|-----------------------------------------------------------------|
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

	// Arrange
	// Use the Fixture property exposed from the base class
	var prefix = Fixture.Create<string>();
	var demo2 = Fixture.Create<string>();

	// Use the FreezeMock extension method (short for Fixture.Freeze<Mock<T>>())
	Fixture.FreezeMock<IDependency2>()
		.Setup(s => s.GetString())
		.Returns(demo2);

	// Act
	// Use the Sut property exposed from the base class
	var response = Sut.Concat(prefix);

	// Assert
	response.Should().Contain(demo2);

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
		string prefix,
		string demo2,
		[Frozen] Mock<IDependency2> mockDependency2,
		TheTypeOfTheSut sut)
	{
		// Arrange
		mockDependency2
			.Setup(s => s.GetString())
			.Returns(demo2);

		// Act
		var response = sut.Concat(prefix);

		// Assert
		response.Should().Contain(demo2);
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
	[InlineAutoMoq("fixed")]
	public void Test1(
		string demo2,
		string prefix,
		[Frozen] Mock<IDependency2> mockDependency2,
		TheTypeOfTheSut sut)
	{
		// Arrange
		mockDependency2
			.Setup(s => s.GetString())
			.Returns(demo2);

		// Act
		var response = sut.Concat(prefix);

		// Assert
		response.Should().Contain("fixed");
	}
}
```

### Customize the Fixture
#### Using AutoMoqTest
It is possible to customize the exposed Fixture by overriding the CustomizeFixture virtual method.
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
		public CustomizedAutoMoqDataAttribute()
			: base(fixture => fixture.Customize<TheTypeToCustomize>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery()))))
		{
		}
	}

	private class CustomizedInlineAutoMoqDataAttribute : InlineAutoMoqDataAttribute
	{
		public CustomizedInlineAutoMoqDataAttribute(params object[] values)
			: base(fixture => fixture.Customize<TheTypeToCustomize>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery()))), values)
		{
		}
	}
}
```