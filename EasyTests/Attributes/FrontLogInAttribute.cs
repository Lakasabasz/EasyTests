namespace EasyTests.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
class FrontLogInAttribute: AttributeBase
{
    public FrontLogInAttribute()
    {
        Scopes = new List<Scope>()
        {
            Scope.SetUp, Scope.OneTimeSetUp
        };
    }
    internal override void Execute(Container c)
    {
        throw new NotImplementedException();
    }
}