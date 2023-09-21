namespace EasyTests;

class Container
{
    private readonly List<ICleanable> _cleanables = new();
    public T Request<T>(IOptions<T>? options = null) where T : IConfigurableCreator<T>, new()
    {
        T obj = typeof(T).GetInterface(nameof(IConfigurableCreator<T>)) is not null ? T.Create(options) : new T();
        if (obj is ICreate<T>) obj = ((ICreate<T>)obj).Create(options);
        if (obj is ICleanable) _cleanables.Add((ICleanable) obj);
        return obj;
    }

    public void CleanUp()
    {
        foreach (var cleanable in _cleanables)
        {
            try
            {
                cleanable.CleanUp();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}