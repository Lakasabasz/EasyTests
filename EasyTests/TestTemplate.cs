namespace EasyTests;

abstract class TestTemplate
{
    protected Container Container = new();

    [OneTimeSetUp]
    public void PrepareClass()
    {
        if (TestContext.CurrentContext.Test.ClassName is null) return;
        var attributes = Type.GetType(TestContext.CurrentContext.Test.ClassName)?
            .GetCustomAttributes(typeof(AttributeBase), true);
        foreach (var attribute in attributes ?? Array.Empty<object>())
        {
            var attr = (AttributeBase)attribute;
            if (!attr.Scopes.Contains(Scope.OneTimeSetUp)) return;
            attr.Execute(Container);
        }
    }

    [SetUp]
    public void PrepareMethod()
    {
        if (TestContext.CurrentContext.Test.ClassName is null) return;
        if (TestContext.CurrentContext.Test.MethodName is null) return;
        var attributes = Type.GetType(TestContext.CurrentContext.Test.ClassName)?
            .GetMethod(TestContext.CurrentContext.Test.MethodName)?
            .GetCustomAttributes(typeof(AttributeBase), true);
        foreach (var attribute in attributes ?? Array.Empty<object>())
        {
            var attr = (AttributeBase)attribute;
            if (!attr.Scopes.Contains(Scope.SetUp)) return;
            attr.Execute(Container);
        }
    }
    
    [OneTimeTearDown]
    public void CleanupClass()
    {
        if (TestContext.CurrentContext.Test.ClassName is null) return;
        var attributes = Type.GetType(TestContext.CurrentContext.Test.ClassName)?
            .GetCustomAttributes(typeof(AttributeBase), true);
        foreach (var attribute in attributes ?? Array.Empty<object>())
        {
            var attr = (AttributeBase)attribute;
            if (!attr.Scopes.Contains(Scope.OneTimeTearDown)) return;
            attr.Execute(Container);
        }

        Container.CleanUp();
    }

    [TearDown]
    public void CleanupMethod()
    {
        if (TestContext.CurrentContext.Test.ClassName is null) return;
        if (TestContext.CurrentContext.Test.MethodName is null) return;
        var attributes = Type.GetType(TestContext.CurrentContext.Test.ClassName)?
            .GetMethod(TestContext.CurrentContext.Test.MethodName)?
            .GetCustomAttributes(typeof(AttributeBase), true);
        foreach (var attribute in attributes ?? Array.Empty<object>())
        {
            var attr = (AttributeBase)attribute;
            if (!attr.Scopes.Contains(Scope.TearDown)) return;
            attr.Execute(Container);
        }
    }
}