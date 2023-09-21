namespace EasyTests;

interface ICreate<T>
{
    T Create(IOptions<T>? options);
}