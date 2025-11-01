public interface IEvent<T>
{
    void Execute(T parameters);
}