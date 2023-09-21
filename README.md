# EasyTests Framework
Implementation of container and custom attributes for easy testing

## Container
Class `container` provides methods `Require<T>` and `Cleanup`. It can create anything that implements `IOptions<T>` or
have parameterless constructor. Additionally, if your object implements `ICleanup` container will save reference to
object and execute `CleanUp()` method when you execute `Cleanup` on container.

Example:
```csharp
class TestFixture: TestTemplate
{
    private Product p = null!;

    [OneTimeSetUp]
    public void CreateProduct()
    {
        p = Container.Require<Product>();
    }
    
    [Test]
    public void ReviewTest()
    {
        Review review = Container.Require<Review>(new ReviewOptions(p));
        Assert.IsNotNull(review.Id, "Review wasn't created");
    }
    
    // Container will be cleaned because TestTemplate OneTimeTearDown will take care of it
```

## Custom attributes
Custom attributes implements functions that should change state of working environment.
They are prepared for different scopes like OneTimeSetUp or TearDown. You can configure
when it should be executed. Everything you have to do is inherit from class `AttributeBase`
rather than `System.Attribute`. Remember to set `System.AttributeUsage` on your class

## Other interfaces
- ICleanable - interface makes object of your class remembered as that is have to cleaned after work
- IOptions\<T> - interface for DTOs containing settings for create `T` object
- IConfigurableCreator\<T> - interface for object that can be created by `container` with outer IOptions\<T>
- ICreate\<T> - interface for post creation configuration, for example send request to API
