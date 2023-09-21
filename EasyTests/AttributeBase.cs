namespace EasyTests;

enum Scope
{
    SetUp,
    OneTimeSetUp,
    TearDown,
    OneTimeTearDown
}

abstract class AttributeBase: Attribute
{
    public List<Scope> Scopes = new() { Scope.OneTimeSetUp };
    internal abstract void Execute(Container c);
}