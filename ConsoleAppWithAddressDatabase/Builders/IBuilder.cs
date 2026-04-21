namespace ConsoleAppWithAddressDatabase.Builders;

public interface IBuilder<T>
{
    public T Reset();
    public T SetId(int id);
}