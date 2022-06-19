# autofixture-boilerplate-nuget
A set of convenient base classes and attributes to work with AutoMoq.

## Content
| Class                      | Description                                              |
|:--------------------------:|----------------------------------------------------------|
| AutoMoqTest                | Abstract class inheriting Fixture and setting up AutoMoq |
| AutoMoqDataAttribute       | An attribute expending AutoData to use AutoMoq           |
| InlineAutoMoqDataAttribute | An attribute expending InlineAutoData to use AutoMoq     |

## Usage
Look at real test samples here : <a href="https://github.com/s8Usy429/autofixture-boilerplate-nuget/tree/main/AutoFixture.Boilerplate.Tests">Samples</a>

### AutoMoqTest
You can inherit all your test classes from AutoMoqTest.
```cs
public class TheSutTests : AutoMoqTest<TheTypeOfTheSut>
{
	...

	// Arrange
	var prefix = Create<string>();
	var demo2 = Create<string>();
	Mock<IDependency2>()
		.Setup(s => s.GetString())
		.Returns(demo2);

	// Act
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
It is possible to customize the Fixture in your constructor.
```cs
public class TheSutTests : AutoMoqTest
{
    public class TheSutTests()
    {
        Customize<TheTypeToCustomize>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
    }
}
```
This will apply to all the tests in the class.
If you don't want this behavior, call the Customize method directly inside your tests.
```cs
public class TheSutTests : AutoMoqTest
{
	[Fact]
	public void Test1()
	{
		...
		Customize<TheTypeToCustomize>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
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