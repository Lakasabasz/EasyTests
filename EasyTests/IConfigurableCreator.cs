namespace EasyTests;

interface IConfigurableCreator<T>
{
    static abstract T Create(IOptions<T>? options);
}